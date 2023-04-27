using JuridikApp.Data;
using JuridikApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using OpenAI_API.Completions;

namespace JuridikApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly JuridikContext _context;

        public QueriesController(JuridikContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Query>>> GetQueries()
        {
            if (_context.Queries == null)
            {
                return NotFound();
            }
            return await _context.Queries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Query>> GetQuery(string id)
        {
            if (_context.Queries == null)
            {
                return NotFound();
            }
            var query = await _context.Queries.FindAsync(id);

            if (query == null)
            {
                return NotFound();
            }

            return query;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuery(string id, Query query)
        {
            if (id != query.QueryId)
            {
                return BadRequest();
            }

            _context.Entry(query).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Query>> PostQueryandAIAnswer([FromForm] QueryRequest request)
        {
            string apiKey = APIKeys.OpenAIKey;
            string answer = string.Empty;
            var openAI = new OpenAIAPI(apiKey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = "I want a case judgement under the following circumnstances: " + request.Description + "which took place on " + request.CaseDate +
                "in " + request.CasePlace + " being the following person the claimant: " + request.CaseClaimant + " and this other person the defendant: " + request.CaseDefendant +
                ". I want that the judgement is done according to the following legislation: " + request.ApplicableLaw + " and this jurispudence: " + request.ApplicableJurisprudence
                + "and this doctrine: " + request.ApplicableDoctrine;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 500;
            var result = openAI.Completions.CreateCompletionsAsync(completionRequest);

            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }

                _context.Queries.Add(new Query()
                {
                    QueryId = Guid.NewGuid().ToString(),
                    CaseDate = request.CaseDate,
                    CasePlace = request.CasePlace,
                    CaseClaimant = request.CaseClaimant,
                    CaseDefendant = request.CaseDefendant,
                    Description = request.Description,
                    ApplicableLaw = request.ApplicableLaw,
                    ApplicableJurisprudence = request.ApplicableJurisprudence,
                    ApplicableDoctrine = request.ApplicableDoctrine,
                    Judgement = answer,
                }
                );
                await _context.SaveChangesAsync();
            }
            return Ok(answer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuery(string id)
        {
            if (_context.Queries == null)
            {
                return NotFound();
            }
            var query = await _context.Queries.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }

            _context.Queries.Remove(query);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QueryExists(string id)
        {
            return (_context.Queries?.Any(e => e.QueryId == id)).GetValueOrDefault();
        }
    }
}