using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    [RoutePrefix("api/classes")]
    public class ClassesController : ApiController
    {
        private readonly MyAppDbContext _context = new MyAppDbContext();

        // GET:
        // api/classes
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var classes = _context.Classes.Where(c => !c.IsDeleted).ToList();
            return Ok(classes);
        }

        // GET:
        // api/classes/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var classItem = _context.Classes.FirstOrDefault(c => c.IdClass == id && !c.IsDeleted);
            if (classItem == null)
                return NotFound();

            return Ok(classItem);
        }

        // GET:
        // api/classes/by-student/{idAlumno}
        [HttpGet]
        [Route("by-student/{idAlumno:int}")]
        public async Task<IHttpActionResult> GetClassesByStudent(int idAlumno)
        {
            var classes = _context.Classes
                .Where(c => !c.IsDeleted && c.StudentsList.Any(s => s.IdStudent == idAlumno))
                .ToList();

            return Ok(classes);
        }

        // POST:
        // api/classes
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] Classes newClass)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Classes.Add(newClass);
            _context.SaveChanges();
            return Created($"api/classes/{newClass.IdClass}", newClass);
        }

        // PUT:
        // api/classes/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Classes updatedClass)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var classItem = _context.Classes.Find(id);
            if (classItem == null || classItem.IsDeleted)
                return NotFound();

            classItem.ClassName = updatedClass.ClassName;
            classItem.IdTeacher = updatedClass.IdTeacher;
            classItem.IdCampus = updatedClass.IdCampus;
            classItem.Period = updatedClass.Period;
            classItem.Year = updatedClass.Year;
            classItem.Credits = updatedClass.Credits;

            _context.SaveChanges();
            return Ok(classItem);
        }

        // DELETE (borrado lógico):
        // api/classes/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var classItem = _context.Classes.Find(id);
            if (classItem == null || classItem.IsDeleted)
                return NotFound();

            classItem.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // Restaurar una clase eliminada:
        // api/classes/{id}/restore
        [HttpPost]
        [Route("{id:int}/restore")]
        public async Task<IHttpActionResult> Restore(int id)
        {
            var classItem = _context.Classes.Find(id);
            if (classItem == null || !classItem.IsDeleted)
                return NotFound();

            classItem.IsDeleted = false;
            _context.SaveChanges();
            return Ok(classItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}