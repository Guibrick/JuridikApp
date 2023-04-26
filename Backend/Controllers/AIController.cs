using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace JuridikApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        [HttpPost]
        public IActionResult GenerateJudgement([FromBody] string prompt)
        {
            string apiKey = APIKeys.OpenAIKey;
            string answer = string.Empty;
            var openAI = new OpenAIAPI(apiKey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = prompt;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 100;
            var result = openAI.Completions.CreateCompletionsAsync(completionRequest);

            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
                return Ok(answer);
            }
            else
            {
                return BadRequest("Not found an answer for that.");
            }
        }
    }
}