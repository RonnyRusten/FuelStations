
using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using Microsoft.Identity.Client;

namespace WinFormsApp1
{
    public class EmailHelper
    {
        public async void SendMail(string recipient, string subject, string body)
        {
            //MailMessage mail = new MailMessage();
            //mail.Subject = subject;
            //mail.From = new MailAddress("ronny.rusten@brattvaag-electro.no");
            //mail.To.Add(recipient);
            //mail.Body = body;
            //mail.IsBodyHtml = true;

            var _sender = "itservice@brael.no";
            var _password = "Helpdesk24";  // The sender's Office 365 password. 

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_sender, _sender));
            message.ReplyTo.Add(new MailboxAddress("helpdesk@brael.no", "helpdesk@brael.no"));
            message.To.Add(new MailboxAddress("ronny.rusten@gmail.com", "ronny.rusten@gmail.com"));
            message.Subject = "Mail fra Bra Bilkontroll";

            var builder = new BodyBuilder
            {
                HtmlBody = @"<!DOCTYPE html>
                             <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
                                Mailbody...
                             "
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                var options = new PublicClientApplicationOptions
                {
                    ClientId = "b12b7c42-68db-4e13-b0fc-5efb60896748",  // Your client ID of the Azure App Registration. 
                    TenantId = "9144e070-53bd-49f9-99ba-2632c26354bf"   // Your Azure tenant ID. 
                };

                var pcab = PublicClientApplicationBuilder
                    .CreateWithApplicationOptions(options)
                    .Build();

                var scopes = new[] { "email", "https://outlook.office365.com/SMTP.Send" };

                var authToken = pcab.AcquireTokenByUsernamePassword(scopes, _sender,
                        new NetworkCredential("", _password).SecurePassword)
                    .ExecuteAsync();

                await client.ConnectAsync("smtp.office365.com", 587, 
                    SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(new SaslMechanismOAuth2(_sender, 
                    authToken.Result.AccessToken));
                var sendResult = await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }

            //SmtpClient smtp = new SmtpClient("10.40.1.51", 587);
            //SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);

            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = true;
            //NetworkCredential netCre = new NetworkCredential("ronny.rusten@brattvaag-electro.no", "RonRus1969@beas");
            //smtp.Credentials = netCre;
            //SmtpClient smtp = new SmtpClient("localhost", 25);

            //try
            //{
            //    smtp.Send(mail);
            //}
            //catch (Exception ex)
            //{
            //    var test = ex;
            //}
        }
    }
}
