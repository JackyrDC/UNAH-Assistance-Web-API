using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UNAH_Assistance_Web_API.Controllers
{
    public class CampusController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [HttpGet]
        public IEnumerable<Models.Campus> Get()
        {
            return db.Campus.ToList();
        }
        [HttpGet]
        public Models.Campus Get(int id)
        {
            return db.Campus.Find(id);
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]Models.Campus campus)
        {
            db.Campus.Add(campus);
            db.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody]Models.Campus campus)
        {
            db.Entry(campus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            db.Campus.Remove(db.Campus.Find(id));
            db.SaveChanges();
            return Ok();
        }

    }
}
