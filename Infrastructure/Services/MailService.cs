using Application.Models;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly SMTPConfig _config;

        public MailService(SMTPConfig config)
        {
            _config = config;
        }

        public void SendConfirmationMessage(string email, string url)
        {
            using var smtp = new SmtpClient()
            {
                Host = _config.Host,
                Port = _config.Port,
                EnableSsl = _config.EnableSsl,
                Credentials = new NetworkCredential(_config.Username, _config.Password)
            };

            using var message = new MailMessage()
            {

                IsBodyHtml = true,
                Subject = "IUsta Email confirmation",
                Body = @$"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Email Confirmation</title>
</head>
<body style=""font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #1a1a1a; color: #ffffff;"">

    <table style=""max-width: 600px; margin: 0 auto; background-color: #1a1a1a; border-collapse: collapse;"">
        <tr>
            <td style=""padding: 20px; text-align: center; background-color: #b90000;"">
                <h1 style=""color: #ffffff;"">Email Confirmation</h1>
            </td>
        </tr>
        <tr>
            <td style=""padding: 20px;"">
                <p>Hello {email},</p>
                <p style=""color: #cccccc;"">Thank you for registering on our website. Please click the button below to confirm your email address:</p>
                <p style=""text-align: center;"">
                    <a href=""{url}"" style=""display: inline-block; padding: 10px 20px; background-color: #b90000; color: #ffffff; text-decoration: none; border-radius: 5px;"">Confirm Email</a>
                </p>
                <p style=""color: #cccccc;"">If you did not request this confirmation, you can ignore this email.</p>
                <p style=""color: #cccccc;"">Best regards,<br> The IUsta Team</p>
            </td>
        </tr>
        <tr>
            <td style=""padding: 20px; text-align: center; background-color: #1a1a1a;"">
                <p style=""margin: 0; color: #cccccc;"">© {DateTime.Now.Year} IUsta. All rights reserved.</p>
            </td>
        </tr>
    </table>

</body>
</html>"
            };
        }
    }
}
