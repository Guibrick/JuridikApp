using JuridikApp.Data;
using JuridikApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<Query>> PostQuery([FromForm] QueryRequest request)
        {
            if (_context.Queries == null)
            {
                return Problem("Entity set 'JuridikContext.Queries'  is null.");
            }
            _context.Queries.Add(new Query()
            {
                QueryId = Guid.NewGuid().ToString(),
                CaseDate = request.CaseDate,
                CasePlace = request.CasePlace,
                CaseClaimant = request.CaseClaimant,
                CaseDefendant = request.CaseDefendant,
                ApplicableLaw = request.ApplicableLaw,
                ApplicableJurisprudence = request.ApplicableJurisprudence,
                ApplicableDoctrine = request.ApplicableDoctrine,
                Judgement = "Pending",
            }
            );

            await _context.SaveChangesAsync();

            return Ok();
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