using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationApi3.Models;

namespace WebApplicationApi3.Controllers
{
    public class missatgesController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/missatges
        public IQueryable<missatges> Getmissatges()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.missatges;
        }

        // GET: api/missatges/5
        [ResponseType(typeof(missatges))]
        public async Task<IHttpActionResult> Getmissatges(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            missatges missatges = await db.missatges.FindAsync(id);
            if (missatges == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(missatges);
            }
            return result;
        }

        // PUT: api/missatges/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmissatges(int id, missatges missatges)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != missatges.id)
            {
                return BadRequest();
            }

            db.Entry(missatges).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!missatgesExists(id))
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

        // POST: api/missatges
        [ResponseType(typeof(missatges))]
        public async Task<IHttpActionResult> Postmissatges(missatges missatges)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.missatges.Add(missatges);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = missatges.id }, missatges);
        }

        // DELETE: api/missatges/5
        [ResponseType(typeof(missatges))]
        public async Task<IHttpActionResult> Deletemissatges(int id)
        {
            missatges missatges = await db.missatges.FindAsync(id);
            if (missatges == null)
            {
                return NotFound();
            }

            db.missatges.Remove(missatges);
            await db.SaveChangesAsync();

            return Ok(missatges);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool missatgesExists(int id)
        {
            return db.missatges.Count(e => e.id == id) > 0;
        }
    }
}