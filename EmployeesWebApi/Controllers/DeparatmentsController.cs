using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeesDataAccess;

namespace EmployeesWebApi.Controllers
{
    public class DeparatmentsController : ApiController
    {
        private EmpDbContext db = new EmpDbContext();

        // GET: api/Deparatments
        public IQueryable<Deparatments> GetDeparatments()
        {
            return db.Deparatments;
        }

        // GET: api/Deparatments/5
        [ResponseType(typeof(Deparatments))]
        public IHttpActionResult GetDeparatments(long id)
        {
            Deparatments deparatments = db.Deparatments.Find(id);
            if (deparatments == null)
            {
                return NotFound();
            }

            return Ok(deparatments);
        }

        // PUT: api/Deparatments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeparatments(long id, Deparatments deparatments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deparatments.DepartmentID)
            {
                return BadRequest();
            }

            db.Entry(deparatments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeparatmentsExists(id))
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

        // POST: api/Deparatments
        [ResponseType(typeof(Deparatments))]
        public IHttpActionResult PostDeparatments(Deparatments deparatments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Deparatments.Add(deparatments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deparatments.DepartmentID }, deparatments);
        }

        // DELETE: api/Deparatments/5
        [ResponseType(typeof(Deparatments))]
        public IHttpActionResult DeleteDeparatments(long id)
        {
            Deparatments deparatments = db.Deparatments.Find(id);
            if (deparatments == null)
            {
                return NotFound();
            }

            db.Deparatments.Remove(deparatments);
            db.SaveChanges();

            return Ok(deparatments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeparatmentsExists(long id)
        {
            return db.Deparatments.Count(e => e.DepartmentID == id) > 0;
        }
    }
}