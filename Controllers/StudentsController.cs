using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UNAH_Assistance_Web_API.Models;

namespace UNAH_Assistance_Web_API.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly MyAppDbContext _context = new MyAppDbContext();

        // GET:
        // api/students
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var students = _context.Estudiantes.Where(s => !s.IsDeleted).ToList();
            return Ok(students);
        }

        // GET:
        // api/students/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var student = _context.Estudiantes.FirstOrDefault(s => s.IdStudent == id && !s.IsDeleted);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST:
        // api/students
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] Students newStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Estudiantes.Add(newStudent);
                _context.SaveChanges();
                return Created($"api/students/{newStudent.IdStudent}", newStudent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST:
        // api/students/many
        [HttpPost]
        [Route("many")]
        public async Task<IHttpActionResult> CreateMany([FromBody] Students[] students)
        {
            if (students == null || students.Length == 0)
                return BadRequest("Lista de estudiantes vacía.");

            try
            {
                _context.Estudiantes.AddRange(students);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT:
        // api/students/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Students updatedStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _context.Estudiantes.Find(id);
            if (student == null || student.IsDeleted)
                return NotFound();

            try
            {
                student.StudentName = updatedStudent.StudentName;
                student.StudentLastName = updatedStudent.StudentLastName;
                student.StudentEmail = updatedStudent.StudentEmail;
                student.StudentPhone = updatedStudent.StudentPhone;
                student.StudentAddress = updatedStudent.StudentAddress;
                student.StudentGender = updatedStudent.StudentGender;
                student.StudentBirthDate = updatedStudent.StudentBirthDate;
                student.StudentPhoto = updatedStudent.StudentPhoto;
                student.StudentActive = updatedStudent.StudentActive;
                student.IdCampus = updatedStudent.IdCampus;
                student.IdUserType = updatedStudent.IdUserType;
                student.IdUserState = updatedStudent.IdUserState;

                _context.SaveChanges();
                return Ok(student);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE (borrado lógico):
        // api/students/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> SoftDelete(int id)
        {
            var student = _context.Estudiantes.Find(id);
            if (student == null || student.IsDeleted)
                return NotFound();

            try
            {
                student.IsDeleted = true;
                _context.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Restaurar un estudiante eliminado:
        // api/students/{id}/restore
        [HttpPost]
        [Route("{id:int}/restore")]
        public async Task<IHttpActionResult> Restore(int id)
        {
            var student = _context.Estudiantes.Find(id);
            if (student == null || !student.IsDeleted)
                return NotFound();

            try
            {
                student.IsDeleted = false;
                _context.SaveChanges();
                return Ok(student);
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