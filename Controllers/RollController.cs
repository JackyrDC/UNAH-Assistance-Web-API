using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    public class RollController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET:
        // api/rolls
        [HttpGet]
        [Route("api/rolls")]
        public IEnumerable<Roll> Get()
        {
            return db.Rolls.Where(r => !r.IsDeleted).ToList();
        }

        // GET:
        // api/rolls/{id}
        [HttpGet]
        [Route("api/rolls/{id}")]
        public IHttpActionResult Get(int id)
        {
            var roll = db.Rolls.FirstOrDefault(r => r.IdRoll == id && !r.IsDeleted);
            if (roll == null)
                return NotFound();

            return Ok(roll);
        }

        // POST:
        // api/rolls
        [HttpPost]
        [Route("api/rolls")]
        public IHttpActionResult Post([FromBody] Roll roll)
        {
            if (roll == null)
                return BadRequest("Datos inválidos");

            db.Rolls.Add(roll);
            db.SaveChanges();
            return Ok(roll);
        }

        // PUT:
        // api/rolls/{id}
        [HttpPut]
        [Route("api/rolls/{id}")]
        public IHttpActionResult Put(int id, [FromBody] Roll updatedRoll)
        {
            var roll = db.Rolls.Find(id);
            if (roll == null || roll.IsDeleted)
                return NotFound();

            roll.RollDate = updatedRoll.RollDate;
            roll.IdTeacher = updatedRoll.IdTeacher;
            roll.IdClass = updatedRoll.IdClass;

            db.SaveChanges();
            return Ok(roll);
        }

        // DELETE (borrado lógico):
        // api/rolls/{id}
        [HttpDelete]
        [Route("api/rolls/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var roll = db.Rolls.Find(id);
            if (roll == null || roll.IsDeleted)
                return NotFound();

            roll.IsDeleted = true;
            db.SaveChanges();
            return Ok("Roll eliminado lógicamente.");
        }


    }
}
