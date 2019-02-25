using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace CalcApp.Controllers
{
    public class HomeController : Controller
    {
        private const string URL = "http://azurefunctionwithsendgrid20190225101730.azurewebsites.net/api/";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public async Task<JsonResult> SendGridMail(string YourEmail, string Subject)
        {


            EmailModel emailModel = new EmailModel();
            emailModel.From = "parvez.natrix@gmail.com";
            emailModel.Tos.Add(YourEmail);
            emailModel.PlainTextContent = "Hello, SendGrid Email!";
            emailModel.Subject = Subject;
            emailModel.HtmlContent = "<strong>Hello, Email!, Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            var customerData = JsonConvert.SerializeObject(emailModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(customerData);
            var byteData = new ByteArrayContent(buffer);
            byteData.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL); //http://localhost:7071/api/
            var response = await client.PostAsync("SendMail", byteData);
            client.Dispose();


            //var client = new SendGridClient("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");
            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress("parvez.natrix@gmail.com", "DX Team"),
            //    Subject = "Hello World from the SendGrid CSharp SDK!",
            //    PlainTextContent = "Hello, Email!",
            //    HtmlContent = "<strong>Hello, Email!</strong>"
            //};
            //msg.AddTo(new EmailAddress("parvez.natrix@gmail.com", "Test User"));
            //var response = await client.SendEmailAsync(msg);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public class EmailModel
        {
            public string From { get; set; }
            public List<string> Tos { get; set; } = new List<string>();
            public string Subject { get; set; }
            public string PlainTextContent { get; set; }
            public string HtmlContent { get; set; }
        }
    }
}