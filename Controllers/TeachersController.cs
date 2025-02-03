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
            teachers.ToList().ForEach(t => db.Teachers.Add(t));
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/teachers/put/{id}")]
        public IHttpActionResult Put(int id)
        {
            var teacher = db.Teachers.Find(id);
            db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
            return Ok();
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Teachers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
