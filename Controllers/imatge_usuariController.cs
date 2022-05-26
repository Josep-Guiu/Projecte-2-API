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
    public class imatge_usuariController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/imatge_usuari
        public IQueryable<imatge_usuari> Getimatge_usuari()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.imatge_usuari;
        }

        // GET: api/imatge_usuari/5
        [ResponseType(typeof(imatge_usuari))]
        public async Task<IHttpActionResult> Getimatge_usuari(int id)
        {
            IHttpActionResult result;

            db.Configuration.LazyLoadingEnabled = false;

            imatge_usuari imatge_usuari = await db.imatge_usuari.FindAsync(id);
            if (imatge_usuari == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(imatge_usuari);
            }

            return result;
        }

        // PUT: api/imatge_usuari/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putimatge_usuari(int id, imatge_usuari imatge_usuari)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imatge_usuari.id)
            {
                return BadRequest();
            }

            db.Entry(imatge_usuari).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!imatge_usuariExists(id))
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

        // POST: api/imatge_usuari
        [ResponseType(typeof(imatge_usuari))]
        public async Task<IHttpActionResult> Postimatge_usuari(imatge_usuari imatge_usuari)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.imatge_usuari.Add(imatge_usuari);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = imatge_usuari.id }, imatge_usuari);
        }

        // DELETE: api/imatge_usuari/5
        [ResponseType(typeof(imatge_usuari))]
        public async Task<IHttpActionResult> Deleteimatge_usuari(int id)
        {
            imatge_usuari imatge_usuari = await db.imatge_usuari.FindAsync(id);
            if (imatge_usuari == null)
            {
                return NotFound();
            }

            db.imatge_usuari.Remove(imatge_usuari);
            await db.SaveChangesAsync();

            return Ok(imatge_usuari);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool imatge_usuariExists(int id)
        {
            return db.imatge_usuari.Count(e => e.id == id) > 0;
        }
    }
}