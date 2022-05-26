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
    public class grups_has_docentsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/grups_has_docents
        public IQueryable<grups_has_docents> Getgrups_has_docents()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.grups_has_docents;
        }

        // GET: api/grups_has_docents/5
        [ResponseType(typeof(grups_has_docents))]
        public async Task<IHttpActionResult> Getgrups_has_docents(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            grups_has_docents grups_has_docents = await db.grups_has_docents.FindAsync(id);
            if (grups_has_docents == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(grups_has_docents);
            }

            return result;
        }

        // PUT: api/grups_has_docents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgrups_has_docents(int id, grups_has_docents grups_has_docents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grups_has_docents.grups_id)
            {
                return BadRequest();
            }

            db.Entry(grups_has_docents).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!grups_has_docentsExists(id))
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

        // POST: api/grups_has_docents
        [ResponseType(typeof(grups_has_docents))]
        public async Task<IHttpActionResult> Postgrups_has_docents(grups_has_docents grups_has_docents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.grups_has_docents.Add(grups_has_docents);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (grups_has_docentsExists(grups_has_docents.grups_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grups_has_docents.grups_id }, grups_has_docents);
        }

        // DELETE: api/grups_has_docents/5
        [ResponseType(typeof(grups_has_docents))]
        public async Task<IHttpActionResult> Deletegrups_has_docents(int id)
        {
            grups_has_docents grups_has_docents = await db.grups_has_docents.FindAsync(id);
            if (grups_has_docents == null)
            {
                return NotFound();
            }

            db.grups_has_docents.Remove(grups_has_docents);
            await db.SaveChangesAsync();

            return Ok(grups_has_docents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool grups_has_docentsExists(int id)
        {
            return db.grups_has_docents.Count(e => e.grups_id == id) > 0;
        }

        [HttpGet]
        [Route("api/grups_has_docents/grups_id/{grups_id}")]
        public async Task<IHttpActionResult> BuscaPerId(int grups_id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<grups_has_docents> _grupsdocents = db.grups_has_docents
                .Where(x => x.grups_id == grups_id)
                                 .ToList();
            return Ok(_grupsdocents);
        }
    }
}