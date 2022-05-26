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
    public class horarisController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/horaris
        public IQueryable<horari> Gethorari()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.horari;
        }

        // GET: api/horaris/5
        [ResponseType(typeof(horari))]
        public async Task<IHttpActionResult> Gethorari(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            horari horari = await db.horari.FindAsync(id);
            if (horari == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(horari);
            }

            return result;
        }

        // PUT: api/horaris/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puthorari(int id, horari horari)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horari.id)
            {
                return BadRequest();
            }

            db.Entry(horari).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!horariExists(id))
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

        // POST: api/horaris
        [ResponseType(typeof(horari))]
        public async Task<IHttpActionResult> Posthorari(horari horari)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.horari.Add(horari);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = horari.id }, horari);
        }

        // DELETE: api/horaris/5
        [ResponseType(typeof(horari))]
        public async Task<IHttpActionResult> Deletehorari(int id)
        {
            horari horari = await db.horari.FindAsync(id);
            if (horari == null)
            {
                return NotFound();
            }

            db.horari.Remove(horari);
            await db.SaveChangesAsync();

            return Ok(horari);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool horariExists(int id)
        {
            return db.horari.Count(e => e.id == id) > 0;
        }
    }
}