using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;

namespace configcat.assistant.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthGoogleController : ControllerBase
    {
        private static readonly JsonParser jsonParser =
            new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        private static HttpClient httpClient = new HttpClient();

        [HttpPost]
        public async Task<ContentResult> Post()
        {
            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            {
                request = jsonParser.Parse<WebhookRequest>(reader);
            }

            var response = new WebhookResponse();

            switch (request.QueryResult.Intent.DisplayName)
            {
                case "Health":
                    response.FulfillmentText = await IsWebsiteOkAsync() ? "All systems are green!" : "There is something wrong with the website.";

                    break;
                default:
                    response.FulfillmentText = "I don't understand your intent.";
                    break;
            }

            string responseJson = response.ToString();
            return Content(responseJson, "application/json");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkResult();
        }

        private async Task<bool> IsWebsiteOkAsync()
        {
            var getResponse = await httpClient.GetAsync("https://configcat.com");
            return getResponse.IsSuccessStatusCode;
        }
    }
}