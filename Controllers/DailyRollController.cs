using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    [RoutePrefix("api/dailyrolls")]
    public class DailyRollController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpGet]
        [Route("get")]
        public IEnumerable<DailyRoll> Get()
        {
            return db.DailyRolls.Where(d => !d.IsDeleted).ToList();
        }

        [HttpGet]
        [Route("get/{id}")]
        public IHttpActionResult Get(int id)
        {
            var dailyRoll = db.DailyRolls.FirstOrDefault(d => d.idDailyRoll == id && !d.IsDeleted);
            if (dailyRoll == null)
                return NotFound();

            return Ok(dailyRoll);
        }

        [HttpPost]
        [Route("post")]
        public IHttpActionResult Post([FromBody] DailyRoll dailyRoll)
        {
            if (dailyRoll == null)
                return BadRequest("Datos inválidos.");

            dailyRoll.creationDate = DateTime.Now;
            db.DailyRolls.Add(dailyRoll);
            db.SaveChanges();
            return Ok("DailyRoll creado correctamente.");
        }

        [HttpPut]
        [Route("put/{id}")]
        public IHttpActionResult Put(int id, [FromBody] DailyRoll dailyRoll)
        {
            var existingDailyRoll = db.DailyRolls.Find(id);
            if (existingDailyRoll == null || existingDailyRoll.IsDeleted)
                return NotFound();

            existingDailyRoll.idRoll = dailyRoll.idRoll;
            existingDailyRoll.creationDate = dailyRoll.creationDate;
            db.SaveChanges();
            return Ok("DailyRoll actualizado correctamente.");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var dailyRoll = db.DailyRolls.Find(id);
            if (dailyRoll == null)
                return NotFound();

            dailyRoll.IsDeleted = true;
            db.SaveChanges();
            return Ok("DailyRoll eliminado lógicamente.");
        }

        [HttpPut]
        [Route("restore/{id}")]
        public IHttpActionResult Restore(int id)
        {
            var dailyRoll = db.DailyRolls.Find(id);
            if (dailyRoll == null)
                return NotFound();

            if (!dailyRoll.IsDeleted)
                return BadRequest("El DailyRoll ya está activo.");

            dailyRoll.IsDeleted = false;
            db.SaveChanges();
            return Ok("DailyRoll restaurado correctamente.");
        }
    }
}
