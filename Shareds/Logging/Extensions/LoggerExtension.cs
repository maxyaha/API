using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Shareds.Logging.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal class LoggerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="log"></param>
        public static void Write(object elements, NLog.Logger log)
        {
            Write(elements, 0, log);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="depth"></param>
        /// <param name="log"></param>
        public static void Write(object elements, int depth, NLog.Logger log)
        {
            if (!log.IsTraceEnabled)
                // Return void, If trace is not enabled.
                return;

            LoggerExtension dumper = new LoggerExtension(depth);
            dumper.WriteObject(null, elements);

            log.Trace(dumper.build);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static string Dump(object elements)
        {
            return Dump(elements, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static string Dump(object elements, int depth)
        {
            LoggerExtension dumper = new LoggerExtension(depth);
            dumper.WriteObject(null, elements);

            return dumper.build.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        private StringBuilder build;
        /// <summary>
        /// 
        /// </summary>
        private int pos;
        /// <summary>
        /// 
        /// </summary>
        private int level;
        /// <summary>
        /// 
        /// </summary>
        private int depth;


        private LoggerExtension(int depth)
        {
            this.build = new StringBuilder();
            this.depth = depth;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void Write(string message)
        {
            if (String.IsNullOrEmpty(message))
                // Return void, If message is null or empty.
                return;

            this.build.Append(message);
            this.pos += message.Length;
        }
        /// <summary>
        /// 
        /// </summary>
        private void WriteIndent()
        {
            for (int level = 0; level < this.level; level++)
            {
                this.build.Append("  ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void WriteTab()
        {
            Write("  ");

            int pos = this.pos % 8;
            while (!pos.Equals(0))
            {
                Write(" ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void WriteLine()
        {
            this.build.AppendLine();
            this.pos = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        private void WriteValue(object elements)
        {
            var message = String.Empty
                as String;
            switch (elements)
            {
                case DateTime element:
                    message = element.ToShortDateString();
                    break;
                case String element:
                    message = element.ToString();
                    break;
                case ValueType element:
                    message = element.ToString();
                    break;
                case IEnumerable element:
                    message = "...";
                    break;
                case null:
                    message = "null";
                    break;
                default:
                    message = "{ }";
                    break;
            }

            Write(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="elements"></param>
        private void WriteObject(string prefix, object elements)
        {
            switch (elements)
            {
                case null:
                    WriteIndent();
                    Write(prefix);
                    WriteValue(null);
                    WriteLine();
                    break;
                case String element:
                    WriteIndent();
                    Write(prefix);
                    WriteValue(element);
                    WriteLine();
                    break;
                case ValueType element:
                    WriteIndent();
                    Write(prefix);
                    WriteValue(element);
                    WriteLine();
                    break;
                case IEnumerable element:
                    element_IEnumerable(prefix, element);
                 
                    break;
                default:
                    WriteIndent();
                    Write(prefix);

                    MemberInfo[] members = elements.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    MemberInfo_element(members, elements);
            
                    WriteLine();

                    if (this.level < this.depth)
                    {
                        foreach (MemberInfo member in members)
                        {
                            object element;

                            switch (member)
                            {
                                case FieldInfo field:
                                    element = field.GetValue(elements);
                                    break;
                                case PropertyInfo property:
                                    element = property.GetValue(elements, null);
                                    break;
                                default:
                                    continue;
                            }
                            this.level++;
                            WriteObject(member.Name + ": ", element);
                            this.level--;
                        }
                    }
                    break;
            }
        }

        private void MemberInfo_element(MemberInfo[] members, object elements)
        {
            foreach (MemberInfo member in members)
            {
                object element;

                try
                {
                    switch (member)
                    {
                        case FieldInfo field:
                            element = field.GetValue(elements);
                            break;
                        case PropertyInfo property:
                            element = property.GetValue(elements, null);
                            break;
                        default:
                            continue;
                    }
                    Write(member.Name);
                    Write("=");
                    WriteValue(element);
                    WriteTab();
                }
                catch (Exception ex)
                {
                    Write("Exception :" + ex.ToString());
                }
            }
        }
        private void element_IEnumerable(string prefix, IEnumerable element)
        {
            foreach (object elementary in element)
            {
                switch (elementary)
                {
                    case IEnumerable enumerable when !(elementary is String):
                        WriteIndent();
                        Write(prefix);
                        WriteValue(elementary);
                        WriteLine();

                        if (this.level < this.depth)
                        {
                            this.level++;
                            WriteObject(prefix, elementary);
                            this.level--;
                        }
                        break;
                    default:
                        WriteObject(prefix, elementary);
                        break;
                }
            }
        }
    }
}
