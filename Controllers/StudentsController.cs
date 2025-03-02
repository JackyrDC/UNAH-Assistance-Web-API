using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UNAH_Assistance_Web_API.Controllers
{
    public class StudentsController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpGet]
        [Route("/api/Students/GET")]
        public IEnumerable<Models.Students> Get()
        {
            return db.Estudiantes.ToList();
        }

        [HttpGet]
        [Route("/api/Students/GET/{id}")]
        public Models.Students Get(int id)
        {
            return db.Estudiantes.Find(id);
        }

        [HttpPost]
        [Route("/api/Students/POST")]
        public void Post([FromBody] Models.Students Student)
        {
            try
            {
                db.Estudiantes.Add(Student);
                db.SaveChanges();

            }
            catch
            {
                Console.WriteLine("Error en la creación del nuevo estudiante");
            }
        }

        [HttpPost]
        [Route("/api/Students/PostMany")]
        public IHttpActionResult PostMany([FromBody] Models.Students[] students)
        {
            try
            {
                db.Estudiantes.AddRange(students);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/api/Students/PUT/{id}")]
        public IHttpActionResult Put(int id, [FromBody] Models.Students student)
        {
            db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("/api/Students/DELETE/{id}")]
        public IHttpActionResult Delete(int id)
        {
            db.Estudiantes.Remove(db.Estudiantes.Find(id));
            db.SaveChanges();
            return Ok();
        }
    }
}