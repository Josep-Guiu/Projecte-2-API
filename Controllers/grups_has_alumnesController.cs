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
    public class grups_has_alumnesController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/grups_has_alumnes
        public IQueryable<grups_has_alumnes> Getgrups_has_alumnes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.grups_has_alumnes;
        }

        // GET: api/grups_has_alumnes/5
        [ResponseType(typeof(grups_has_alumnes))]
        public async Task<IHttpActionResult> Getgrups_has_alumnes(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            grups_has_alumnes grups_has_alumnes = await db.grups_has_alumnes.FindAsync(id);
            if (grups_has_alumnes == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(grups_has_alumnes);
            }

            return result;
        }

        // PUT: api/grups_has_alumnes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgrups_has_alumnes(int id, grups_has_alumnes grups_has_alumnes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grups_has_alumnes.grups_id)
            {
                return BadRequest();
            }

            db.Entry(grups_has_alumnes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!grups_has_alumnesExists(id))
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

        // POST: api/grups_has_alumnes
        [ResponseType(typeof(grups_has_alumnes))]
        public async Task<IHttpActionResult> Postgrups_has_alumnes(grups_has_alumnes grups_has_alumnes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.grups_has_alumnes.Add(grups_has_alumnes);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (grups_has_alumnesExists(grups_has_alumnes.grups_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grups_has_alumnes.grups_id }, grups_has_alumnes);
        }

        // DELETE: api/grups_has_alumnes/5
        [ResponseType(typeof(grups_has_alumnes))]
        public async Task<IHttpActionResult> Deletegrups_has_alumnes(int id)
        {
            grups_has_alumnes grups_has_alumnes = await db.grups_has_alumnes.FindAsync(id);
            if (grups_has_alumnes == null)
            {
                return NotFound();
            }

            db.grups_has_alumnes.Remove(grups_has_alumnes);
            await db.SaveChangesAsync();

            return Ok(grups_has_alumnes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool grups_has_alumnesExists(int id)
        {
            return db.grups_has_alumnes.Count(e => e.grups_id == id) > 0;
        }

        [HttpGet]
        [Route("api/grups_has_alumnes/grups_id/{grups_id}")]
        public async Task<IHttpActionResult> BuscaPerId(int grups_id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<grups_has_alumnes> _grupsalumnes = db.grups_has_alumnes
                .Where(x => x.grups_id == grups_id)
                                 .ToList();
            return Ok(_grupsalumnes);
        }
    }
}