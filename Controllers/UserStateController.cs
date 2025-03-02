using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
namespace UNAH_Assistance_Web_API.Controllers
{
    public class UserStateController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpGet]
        [Route("/api/userstate/get")]
        public IEnumerable<Models.UserState> Get()
        {
            return db.UserStates.ToList();
        }

        [HttpGet]
        [Route("/api/userstate/get/{id}")]
        public Models.UserState Get(int id)
        {
            return db.UserStates.Find(id);
        }

        [HttpPost]
        [Route("/api/userstate/post")]
        public IHttpActionResult Post([FromBody]Models.UserState userState)
        {
            db.UserStates.Add(userState);
            db.SaveChanges();
            return Ok();
        }


        [HttpPut]
        [Route("/api/userstate/put/{id}")]
        public IHttpActionResult Edit(int id, [FromBody]Models.UserState userState)
        {
            try
            {
                db.Entry(userState).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                db.UserStates.Remove(db.UserStates.Find(id));
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}
