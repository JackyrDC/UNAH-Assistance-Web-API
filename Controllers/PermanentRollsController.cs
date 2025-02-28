using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    [[Route("api/[controller]")]
    [ApiController]
    public class PermanentRollController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PermanentRollController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermanentRoll>>> GetPermanentRolls()
        {
            return await _context.PermanentRolls.Where(p => !p.IsDeleted).ToListAsync();
        }

        [HttpGet("{idDailyRoll}/{idStudent}")]
        public async Task<ActionResult<PermanentRoll>> GetPermanentRoll(int idDailyRoll, int idStudent)
        {
            var permanentRoll = await _context.PermanentRolls.FindAsync(idDailyRoll, idStudent);

            if (permanentRoll == null || permanentRoll.IsDeleted)
            {
                return NotFound();
            }

            return permanentRoll;
        }

        [HttpPost]
        public async Task<ActionResult<PermanentRoll>> PostPermanentRoll(PermanentRoll permanentRoll)
        {
            _context.PermanentRolls.Add(permanentRoll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermanentRoll", new { idDailyRoll = permanentRoll.idDailyRoll, idStudent = permanentRoll.idStudent }, permanentRoll);
        }

        [HttpPut("{idDailyRoll}/{idStudent}")]
        public async Task<IActionResult> PutPermanentRoll(int idDailyRoll, int idStudent, PermanentRoll permanentRoll)
        {
            if (idDailyRoll != permanentRoll.idDailyRoll || idStudent != permanentRoll.idStudent)
            {
                return BadRequest();
            }

            _context.Entry(permanentRoll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PermanentRolls.Any(e => e.idDailyRoll == idDailyRoll && e.idStudent == idStudent))
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

        [HttpDelete("{idDailyRoll}/{idStudent}")]
        public async Task<IActionResult> DeletePermanentRoll(int idDailyRoll, int idStudent)
        {
            var permanentRoll = await _context.PermanentRolls.FindAsync(idDailyRoll, idStudent);
            if (permanentRoll == null || permanentRoll.IsDeleted)
            {
                return NotFound();
            }

            permanentRoll.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
