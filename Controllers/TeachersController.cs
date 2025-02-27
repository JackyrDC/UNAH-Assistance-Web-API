using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeachersController : ApiController
    {
        private readonly MyAppDbContext _context = new MyAppDbContext();

        // GET:
        // api/teachers
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var teachers = _context.Teachers.Where(t => !t.IsDeleted).ToList();
            return Ok(teachers);
        }

        // GET:
        // api/teachers/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.IdTeacher == id && !t.IsDeleted);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        // POST:
        // api/teachers
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] Teachers newTeacher)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Teachers.Add(newTeacher);
                _context.SaveChanges();
                return Created($"api/teachers/{newTeacher.IdTeacher}", newTeacher);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST:
        // api/teachers/many
        [HttpPost]
        [Route("many")]
        public async Task<IHttpActionResult> CreateMany([FromBody] Teachers[] teachers)
        {
            if (teachers == null || teachers.Length == 0)
                return BadRequest("Lista de profesores vacía.");

            try
            {
                _context.Teachers.AddRange(teachers);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT:
        // api/teachers/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Teachers updatedTeacher)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = _context.Teachers.Find(id);
            if (teacher == null || teacher.IsDeleted)
                return NotFound();

            try
            {
                teacher.EmployeeNumber = updatedTeacher.EmployeeNumber;
                teacher.IdCampus = updatedTeacher.IdCampus;
                teacher.IdUserState = updatedTeacher.IdUserState;
                teacher.IdUserType = updatedTeacher.IdUserType;

                _context.SaveChanges();
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE (borrado lógico):
        // api/teachers/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null || teacher.IsDeleted)
                return NotFound();

            try
            {
                teacher.IsDeleted = true;
                _context.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Restaurar un profesor eliminado:
        // api/teachers/{id}/restore
        [HttpPost]
        [Route("{id:int}/restore")]
        public async Task<IHttpActionResult> Restore(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null || !teacher.IsDeleted)
                return NotFound();

            try
            {
                teacher.IsDeleted = false;
                _context.SaveChanges();
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
