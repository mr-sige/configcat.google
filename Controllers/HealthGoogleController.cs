using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Dialogflow.v2.Data;
using Microsoft.AspNetCore.Mvc;

namespace configcat.assistant.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthGoogleController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] GoogleCloudDialogflowV2WebhookRequest request)
        {
            var response = new GoogleCloudDialogflowV2WebhookResponse();

            response.FulfillmentText = "ConfigCat rules!";
            return new OkObjectResult(response);
            
        }
    }
}
