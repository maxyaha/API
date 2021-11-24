using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Shareds.Common.SMTPClient
{
    public static class Clienter
    {
        public static void Send(this MailMessage message, string host, string port)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = host;
                smtp.Port = int.Parse(port);
                
                // send the email
                smtp.Send(message);
            }
        }
    }
}
    
