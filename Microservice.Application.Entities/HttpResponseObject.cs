using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Microservice.Application.Entities
{
    public class HttpResponseObject
    {
        public HttpResponseObject(HttpStatusCode status)
        {
            StatusCode = status;
        }

        public HttpResponseObject(HttpStatusCode status, object content)
        {
            StatusCode = status;
            Content = content;
        }


        public string Version { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get => StatusCode.ToString(); }

        public string ReasonPhrase { get; set; }

        public object Content { get; set; }
    }
}
