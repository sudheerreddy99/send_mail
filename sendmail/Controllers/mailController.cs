using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace sendmail.Controllers
{
    public class mailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Home(string recievermail,string Title,string subject)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse("wilhelmine89@ethereal.email");
                email.To.Add(MailboxAddress.Parse($"{recievermail}"));
                email.Subject = Title;
                email.Body = new TextPart(TextFormat.Html) { Text = $"<h2>{subject}</h1>" };

                
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("wilhelmine89@ethereal.email", "S2GcXu5SYg6g2226N5");
                smtp.Send(email);
                smtp.Disconnect(true);
                ViewData["send"] = "Mail sent succesfully";
            }
            catch (Exception ex)
            {
                ViewData["send"] = "error occured";
            }
            return View();
        }
    }
}
