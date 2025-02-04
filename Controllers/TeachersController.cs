using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace UNAH_Assistance_Web_API.Controllers
{
    public class TeachersController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [HttpGet]
        [Route("api/teachers/")]
        public IEnumerable<Models.Teachers> Get()
        {
            return db.Teachers.ToList();
        }

        [HttpGet]
        [Route("api/teachers/{id}")]
        public Models.Teachers Get(int id)
        {
            return db.Teachers.Find(id);
        }

        [HttpPost]
        [Route("api/teachers/post")]
        public IHttpActionResult Post([FromBody]Models.Teachers teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("api/teachers/createmultiple")]
        public IHttpActionResult CreateMultiple([FromBody]IEnumerable<Models.Teachers> teachers)
        {
            try{
                db.Teachers.AddRange(teachers);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("api/teachers/put/{id}")]
        public IHttpActionResult Put(int id)
        {
            var teacher = db.Teachers.Find(id);
            db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
            return Ok();
        }

        [HttpPut]
        [Route("api/teachers/put")]
        public IHttpActionResult Edit(int id, [FromBody]Models.Teachers teacher)
        {
            try
            {
                db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("api/teachers/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                db.Teachers.Remove(db.Teachers.Find(id));
                db.SaveChanges();
                return Ok();
            }
            catch {
                return BadRequest();
            }
        }

 
    }
}
