using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UNAH_Assistance_Web_API.Controllers
{
    public class ClassesController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpGet]
        [Route("api/Classes/Get")]
        public IEnumerable<Models.Classes> Get()
        {
            return db.Classes.ToList();
        }

        [HttpGet]
        [Route("api/Classes/Get/{id}")]
        public Models.Classes GetbyId(int id)
        {
            return db.Classes.Find(id);
        }

        [HttpGet]
        [Route("api/Classes/GetByAlumn/{idAlumno}")]
        public IEnumerable<Models.Classes> GetClassesByAlumn(int idAlumno)
        {
            return db.Classes.Where(c => c.StudentsList.Any(s => s.IdStudent == idAlumno)).ToList();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Models.Classes classes)
        {
            db.Classes.Add(classes);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Models.Classes classes)
        {
            db.Entry(classes).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            db.Classes.Remove(db.Classes.Find(id));
            db.SaveChanges();
            return Ok();
        }
    }
}
