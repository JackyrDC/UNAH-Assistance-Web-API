using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    [RoutePrefix("api/campus")]
    public class CampusController : ApiController
    {
        private readonly MyAppDbContext _context = new MyAppDbContext();

        // GET:
        // api/campus
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var campuses = _context.Campus.Where(c => !c.IsDeleted).ToList();
            return Ok(campuses);
        }

        // GET:
        // api/campus/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var campus = _context.Campus.FirstOrDefault(c => c.IdCampus == id && !c.IsDeleted);
            if (campus == null)
                return NotFound();

            return Ok(campus);
        }

        // POST:
        // api/campus
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] Campus campus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Campus.Add(campus);
            _context.SaveChanges();
            return Created($"api/campus/{campus.IdCampus}", campus);
        }

        // PUT:
        // api/campus/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Campus updatedCampus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campus = _context.Campus.Find(id);
            if (campus == null || campus.IsDeleted)
                return NotFound();

            campus.Name = updatedCampus.Name;
            campus.Address = updatedCampus.Address;
            campus.City = updatedCampus.City;

            _context.SaveChanges();
            return Ok(campus);
        }

        // DELETE (borrado lógico):
        // api/campus/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var campus = _context.Campus.Find(id);
            if (campus == null || campus.IsDeleted)
                return NotFound();

            campus.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // Restaurar un campus eliminado:
        // api/campus/{id}/restore
        [HttpPost]
        [Route("{id:int}/restore")]
        public async Task<IHttpActionResult> Restore(int id)
        {
            var campus = _context.Campus.Find(id);
            if (campus == null || !campus.IsDeleted)
                return NotFound();

            campus.IsDeleted = false;
            _context.SaveChanges();
            return Ok(campus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}