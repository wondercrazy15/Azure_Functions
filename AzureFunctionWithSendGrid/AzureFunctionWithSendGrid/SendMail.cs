// The 'From' and 'To' fields are automatically populated with the values specified by the binding settings.
//
// You can also optionally configure the default From/To addresses globally via host.config, e.g.:
//
// {
//   "sendGrid": {
//      "to": "user@host.com",
//      "from": "Azure Functions <samples@functions.com>"
//   }
// }
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using SendGrid;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Web.Http.Results;
using System.Threading.Tasks;

namespace AzureFunctionWithSendGrid
{
    public static class SendMail
    {
        [FunctionName("SendMail")]
        public static async Task<HttpResponseMessage> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]string data, HttpRequestMessage req)
        {
            try
            {
                var emailObject = JsonConvert.DeserializeObject<EmailModel>(data);
                var client = new SendGridClient("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");
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(emailObject.From, "DX Team"),
                    Subject = emailObject.Subject,
                    PlainTextContent = emailObject.PlainTextContent,
                    HtmlContent = emailObject.HtmlContent
                };
                foreach (var item in emailObject.Tos)
                {
                    msg.AddTo(new EmailAddress(item, "Test User"));
                }
                var response = client.SendEmailAsync(msg);
                return req.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (System.Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
    public class EmailModel
    {
        public string From { get; set; }
        public List<string> Tos { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
    }
}
