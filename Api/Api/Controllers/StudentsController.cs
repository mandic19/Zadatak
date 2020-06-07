using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Api.Models;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [Authorize]
    public class StudentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Students
        public HttpResponseMessage GetStudents()
        {
            try
            {
                var studentsList = db.Database.SqlQuery<Student>("exec SelectStudents").ToList<Student>();
                foreach(Student student in studentsList)
                {
                    student.Status = db.Status.Find(student.Status_Id);
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JArray.FromObject(studentsList).ToString(), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
          
            if (student == null)
            {
                return NotFound();
            }
            student.Courses = db.Database.SqlQuery<Course>("Select courses.CourseID, courses.Name from courses join StudentCourses on courses.CourseID = studentcourses.Course_CourseID" +
               " WHERE Student_StudentID = " + student.StudentID).ToList<Course>();
            Status status = db.Status.Find(student.Status_Id);
            student.Status = status;

            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentID)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Students.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.StudentID }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.StudentID == id) > 0;
        }
    }
}