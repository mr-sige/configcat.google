using System.IO;
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

        [HttpPost]
        public ContentResult Post()
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
                    response.FulfillmentText = "All systems are green!";
                    break;
                default:
                    response.FulfillmentText = "I don't understand your intent.";
                    break;
            }

            response.FulfillmentText += $" Intent: {request.QueryResult.Intent.DisplayName}";
            string responseJson = response.ToString();
            return Content(responseJson, "application/json");
    }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkResult();
        }
    }
}
