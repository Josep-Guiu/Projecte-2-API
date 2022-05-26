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
    public class grups_has_horarisController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/grups_has_horaris
        public IQueryable<grups_has_horaris> Getgrups_has_horaris()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.grups_has_horaris;
        }

        // GET: api/grups_has_horaris/5
        [ResponseType(typeof(grups_has_horaris))]
        public async Task<IHttpActionResult> Getgrups_has_horaris(int id)
        {

            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;


            grups_has_horaris grups_has_horaris = await db.grups_has_horaris.FindAsync(id);
            if (grups_has_horaris == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(grups_has_horaris);
            }

            return result;
        }

        // PUT: api/grups_has_horaris/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgrups_has_horaris(int id, grups_has_horaris grups_has_horaris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grups_has_horaris.id_grups)
            {
                return BadRequest();
            }

            db.Entry(grups_has_horaris).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!grups_has_horarisExists(id))
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

        // POST: api/grups_has_horaris
        [ResponseType(typeof(grups_has_horaris))]
        public async Task<IHttpActionResult> Postgrups_has_horaris(grups_has_horaris grups_has_horaris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.grups_has_horaris.Add(grups_has_horaris);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (grups_has_horarisExists(grups_has_horaris.id_grups))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grups_has_horaris.id_grups }, grups_has_horaris);
        }

        // DELETE: api/grups_has_horaris/5
        [ResponseType(typeof(grups_has_horaris))]
        public async Task<IHttpActionResult> Deletegrups_has_horaris(int id)
        {
            grups_has_horaris grups_has_horaris = await db.grups_has_horaris.FindAsync(id);
            if (grups_has_horaris == null)
            {
                return NotFound();
            }

            db.grups_has_horaris.Remove(grups_has_horaris);
            await db.SaveChangesAsync();

            return Ok(grups_has_horaris);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool grups_has_horarisExists(int id)
        {
            return db.grups_has_horaris.Count(e => e.id_grups == id) > 0;
        }
    }
}