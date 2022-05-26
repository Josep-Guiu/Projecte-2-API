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
    public class grupsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/grups
        public IQueryable<grups> Getgrups()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.grups;
        }

        // GET: api/grups/5
        [ResponseType(typeof(grups))]
        public async Task<IHttpActionResult> Getgrups(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            grups grups = await db.grups.FindAsync(id);
            if (grups == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(grups);
            }

            return result;
        }

        // PUT: api/grups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgrups(int id, grups grups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grups.id)
            {
                return BadRequest();
            }

            db.Entry(grups).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!grupsExists(id))
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

        // POST: api/grups
        [ResponseType(typeof(grups))]
        public async Task<IHttpActionResult> Postgrups(grups grups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.grups.Add(grups);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = grups.id }, grups);
        }

        // DELETE: api/grups/5
        [ResponseType(typeof(grups))]
        public async Task<IHttpActionResult> Deletegrups(int id)
        {
            grups grups = await db.grups.FindAsync(id);
            if (grups == null)
            {
                return NotFound();
            }

            db.grups.Remove(grups);
            await db.SaveChangesAsync();

            return Ok(grups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool grupsExists(int id)
        {
            return db.grups.Count(e => e.id == id) > 0;
        }
    }
}