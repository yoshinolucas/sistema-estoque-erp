using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Sistema_ERP.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _config;
        public Email(IConfiguration config)
        {
            _config = config;
        }
        public bool Enviar(string email, string assunto, string msg)
        {
            try
            {
                string host = _config.GetValue<string>("SMTP:Host");
                string nome = _config.GetValue<string>("SMTP:Nome");
                string userName = _config.GetValue<string>("SMTP:UserName");
                string senha = _config.GetValue<string>("SMTP:Senha");
                int port = _config.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, nome)
                };
                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = msg;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(userName, senha);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
