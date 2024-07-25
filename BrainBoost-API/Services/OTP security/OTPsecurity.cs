using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using MimeKit;
using MimeKit.Text;

namespace BrainBoost_API.Services.OTP_security
{
    public class OTPsecurity
    {
        public static string sendmail(string EmailReceiver)
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("tomaswagdy549@gmail.com"));
            email.To.Add(MailboxAddress.Parse(EmailReceiver));
            email.Subject = "this is test mail";
            email.Body = new TextPart(TextFormat.Html) { Text = $"your activation code is {code.ToString("D4")} , it will be expired in 3 minutes"  };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465,true);
            smtp.Authenticate("tomaswagdy549@gmail.com", "abqf vroq ttxs koqk");
            smtp.Send(email);
            smtp.Disconnect(true);
            return code.ToString("D4");
        }
    }
}
