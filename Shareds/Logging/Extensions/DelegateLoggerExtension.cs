using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shareds.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Shareds.Logging.Extensions
{
    public abstract class LoggerDelegateExtensions : DelegatingHandler
    {
        private readonly RequestDelegate nexter;
        private readonly RecyclableMemoryStreamManager streamer;

        private const int ReadChunkBufferLength = 4096;


        protected LoggerDelegateExtensions()
        {
        }

        protected LoggerDelegateExtensions(RequestDelegate next)
        {
            this.nexter = next;
            this.streamer = new RecyclableMemoryStreamManager();
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var information = string.Format("{0} {1}", request.Method, request.RequestUri);

            #region request
            var requestmessage = request.Content is null ? new byte[] { } : await request.Content.ReadAsByteArrayAsync(); 
            #endregion request
            // Logging
            await InnerMessageAsync(information, requestmessage);


            #region response
            var response = await base.SendAsync(request, cancellationToken);

            byte[] responsemessage;

            if (response.IsSuccessStatusCode)
                responsemessage = response.Content is null ? new byte[] { } : await response.Content.ReadAsByteArrayAsync();
            else
                responsemessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);
            #endregion response
            // Logging
            await OuterMessageAsync(information, responsemessage);

            return response;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var information = string.Format("{0} {1}", context.Request.Method, context.Request.GetEncodedUrl());

            // #region request

            #region request
            byte[] requestmessage = null;
            context.Request.EnableBuffering();
            // Leave the body open so the next middleware can read it.
            if (context.Request.ContentLength != null && context.Request.ContentLength > 0)
            {
                using (var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: Convert.ToInt32(context.Request.ContentLength),
                    leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    // Do some processing with body…
                    requestmessage = Encoding.ASCII.GetBytes(body);

                    // Reset the request body stream position so the next middleware can read it
                    context.Request.Body.Position = 0;
                }
            }
            #endregion request
            await InnerMessageAsync(information, requestmessage);

            #region response
            byte[] responsemessage;

            var original = context.Response.Body;
            using (var stream = this.streamer.GetStream())
            {
                context.Response.Body = stream;

                await this.nexter(context).ConfigureAwait(false);

                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(original);
                stream.Seek(0, SeekOrigin.Begin);

                responsemessage = await ReadAsByteArrayAsync(context.Response);


            }
            #endregion response
            // Logging
            await OuterMessageAsync(information, responsemessage);
        }
        private async Task<byte[]> ReadAsByteArrayAsync(HttpResponse response)
        {
            using (var stream = this.streamer.GetStream())
            {
                await response.Body.CopyToAsync(stream);
                response.Body.Seek(0, SeekOrigin.Begin);
                return ReadStreamInChunks(stream);
            }
        }
        private static byte[] ReadStreamInChunks(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer;
            using (var writer = new StringWriter())
            {
                using (var reader = new StreamReader(stream))
                {
                    var read = new char[ReadChunkBufferLength];
                    int length;
                    //DO WHILE: IS USEFUL FOR THE LAST ITERATION IN CASE READCHUNKLENGTH < CHUNKLENGTH
                    do
                    {
                        length = reader.ReadBlock(read, 0, ReadChunkBufferLength);
                        writer.Write(read, 0, length);
                    } while (length > 0);

                    buffer = Encoding.ASCII.GetBytes(writer.ToString());
                }
            }
            return buffer;
        }



       

        protected abstract Task InnerMessageAsync(string information, byte[] message);
        protected abstract Task OuterMessageAsync(string information, byte[] message);
    }

    public class LoggingDelegate : LoggerDelegateExtensions
    {
        private readonly ILogger logger;

        public LoggingDelegate(ILogger log) : base()
        {
            this.logger = log;
        }

        public LoggingDelegate(ILogger log, RequestDelegate next) : base(next)
        {
            this.logger = log;
        }

        protected override async Task InnerMessageAsync(string information, byte[] message)
        {
            try
            {
                JsonConvert.DeserializeObject<IDictionary<string, object>>(Encoding.UTF8.GetString(message), new IgnoreBase64String());
                await Task.Run(() => this.logger.Info(string.Format("Request: {0}\r\n{1}", information, Encoding.UTF8.GetString(message))));
            }
            catch (Exception ex)
            {
                await Task.Run(() => this.logger.Info(string.Format("Request: {0}\r\n{1}", information, ex.ToString())));
            }
        }

        protected override async Task OuterMessageAsync(string information, byte[] message)
        {
            try
            {
                JsonConvert.DeserializeObject<IDictionary<string, object>>(Encoding.UTF8.GetString(message), new IgnoreBase64String());
                await Task.Run(() => this.logger.Info(string.Format("Response: {0}\r\n{1}", information, Encoding.UTF8.GetString(message))));
            }
            catch (Exception ex)
            {
                await Task.Run(() => this.logger.Info(string.Format("Response: {0}\r\n{1}", information, ex.ToString())));
            }
        }
    }
    public class IgnoreBase64String : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { this.WriteValue(writer, value); }

        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        private void WriteValue(JsonWriter writer, object value)
        {
            var t = JToken.FromObject(value);
            switch (t.Type)
            {
                case JTokenType.Object:
                    this.WriteObject(writer, value);
                    break;
                case JTokenType.Array:
                    this.WriteArray(writer, value);
                    break;
                default:
                    writer.WriteValue(value);
                    break;
            }
        }

        private void WriteObject(JsonWriter writer, object value)
        {
            writer.WriteStartObject();
            var obj = value as IDictionary<string, object>;
            foreach (var kvp in obj)
            {
                writer.WritePropertyName(kvp.Key);
                this.WriteValue(writer, kvp.Value);
            }
            writer.WriteEndObject();
        }

        private void WriteArray(JsonWriter writer, object value)
        {
            writer.WriteStartArray();
            var array = value as IEnumerable<object>;
            foreach (var o in array)
            {
                this.WriteValue(writer, o);
            }
            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ReadValue(reader);
        }

        public bool ChkIsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0 && s.Length > 16) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        private object ReadValue(JsonReader reader)
        {
            while (reader.TokenType == JsonToken.Comment)
            {
                if (!reader.Read()) throw new JsonSerializationException("Unexpected Token when converting IDictionary<string, object>");
            }

            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return ReadObject(reader);
                case JsonToken.StartArray:
                    return this.ReadArray(reader);
                case JsonToken.String when Convert.TryFromBase64String((string)reader.Value, new Span<byte>(new byte[((string)reader.Value).Length]), out int bytes) && ChkIsBase64String(((string)reader.Value)):
                    return "ignore base64";
                case JsonToken.String when ((string)reader.Value).Contains("application/pdf;base64,"):
                    return "ignore base64";
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.String:
                case JsonToken.Boolean:
                case JsonToken.Undefined:
                case JsonToken.Null:
                case JsonToken.Date:
                case JsonToken.Bytes:
                    return reader.Value;
                default:
                    throw new JsonSerializationException
                        (string.Format("Unexpected token when converting IDictionary<string, object>: {0}", reader.TokenType));
            }
        }

        private object ReadArray(JsonReader reader)
        {
            IList<object> list = new List<object>();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Comment:
                        break;
                    case JsonToken.EndArray:
                        return list;
                    default:
                        var v = ReadValue(reader);

                        list.Add(v);
                        break;
                }
            }

            throw new JsonSerializationException("Unexpected end when reading IDictionary<string, object>");
        }

        private object ReadObject(JsonReader reader)
        {
            var obj = new Dictionary<string, object>();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        var propertyName = reader.Value.ToString();

                        if (!reader.Read())
                        {
                            throw new JsonSerializationException("Unexpected end when reading IDictionary<string, object>");
                        }

                        var v = ReadValue(reader);

                        obj[propertyName] = v;
                        break;
                    case JsonToken.Comment:
                        break;
                    case JsonToken.EndObject:
                        return obj;
                }
            }

            throw new JsonSerializationException("Unexpected end when reading IDictionary<string, object>");
        }

        public override bool CanConvert(Type objectType) { return typeof(IDictionary<string, object>).IsAssignableFrom(objectType); }
    }


    public class DictionaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { this.WriteValue(writer, value); }

        private void WriteValue(JsonWriter writer, object value)
        {
            var t = JToken.FromObject(value);
            switch (t.Type)
            {
                case JTokenType.Object:
                    this.WriteObject(writer, value);
                    break;
                case JTokenType.Array:
                    this.WriteArray(writer, value);
                    break;
                default:
                    writer.WriteValue(value);
                    break;
            }
        }

        private void WriteObject(JsonWriter writer, object value)
        {
            writer.WriteStartObject();
            var obj = value as IDictionary<string, object>;
            foreach (var kvp in obj)
            {
                writer.WritePropertyName(kvp.Key);
                this.WriteValue(writer, kvp.Value);
            }
            writer.WriteEndObject();
        }

        private void WriteArray(JsonWriter writer, object value)
        {
            writer.WriteStartArray();
            var array = value as IEnumerable<object>;
            foreach (var o in array)
            {
                this.WriteValue(writer, o);
            }
            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ReadValue(reader);
        }

        private object ReadValue(JsonReader reader)
        {
            while (reader.TokenType == JsonToken.Comment)
            {
                if (!reader.Read()) throw new JsonSerializationException("Unexpected Token when converting IDictionary<string, object>");
            }

            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return ReadObject(reader);
                case JsonToken.StartArray:
                    return this.ReadArray(reader);
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.String:
                case JsonToken.Boolean:
                case JsonToken.Undefined:
                case JsonToken.Null:
                case JsonToken.Date:
                case JsonToken.Bytes:
                    return reader.Value;
                default:
                    throw new JsonSerializationException
                        (string.Format("Unexpected token when converting IDictionary<string, object>: {0}", reader.TokenType));
            }
        }

        private object ReadArray(JsonReader reader)
        {
            IList<object> list = new List<object>();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Comment:
                        break;
                    case JsonToken.EndArray:
                        return list;
                    default:
                        var v = ReadValue(reader);
                        list.Add(v);
                        break;
                }
            }

            throw new JsonSerializationException("Unexpected end when reading IDictionary<string, object>");
        }

        private object ReadObject(JsonReader reader)
        {
            var obj = new Dictionary<string, object>();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        var propertyName = reader.Value.ToString();

                        if (!reader.Read())
                        {
                            throw new JsonSerializationException("Unexpected end when reading IDictionary<string, object>");
                        }

                        var v = ReadValue(reader);

                        obj[propertyName] = v;
                        break;
                    case JsonToken.Comment:
                        break;
                    case JsonToken.EndObject:
                        return obj;
                }
            }

            throw new JsonSerializationException("Unexpected end when reading IDictionary<string, object>");
        }

        public override bool CanConvert(Type objectType) { return typeof(IDictionary<string, object>).IsAssignableFrom(objectType); }
    }
}
