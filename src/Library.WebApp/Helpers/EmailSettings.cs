using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Helpers
{
    public enum EmailTypes : byte
    {
        Direct, Newsletter, Sales, NoReply
    }

    public class EmailSettings
    {
        public static MailAddress GetMailAddress(EmailTypes type)
        {
            if (type == EmailTypes.Direct)
                return new MailAddress("username1@example.com", "Display name");
            else if (type == EmailTypes.Newsletter)
                return new MailAddress("username2@example.com", "Display name");
            else if (type == EmailTypes.Sales)
                return new MailAddress("username3@example.com", "Display name");
            else //if (type == EmailTypes.NoReply || ...) 
                return new MailAddress("username4@example.com", "Display name");
        }

        public static SmtpClient GetSmtpClient(EmailTypes type)
        {
            if (type == EmailTypes.Direct)
                return new SmtpClient
                {
                    Host = "mail.example.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential() { UserName = "username1@rezaee.org", Password = "xxxxxxxx" }
                };
            else if (type == EmailTypes.Newsletter)
                return new SmtpClient
                {
                    Host = "mail.example.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential() { UserName = "username2@rezaee.org", Password = "xxxxxxxx" }
                };
            else if (type == EmailTypes.Sales)
                return new SmtpClient
                {
                    Host = "mail.example.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential() { UserName = "username3@rezaee.org", Password = "xxxxxxxx" }
                };
            else //if (type == EmailTypes.NoReply || ...) 
                return new SmtpClient
                {
                    Host = "mail.example.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential() { UserName = "username4@rezaee.org", Password = "xxxxxxxx" }
                };
        }
    }
}
