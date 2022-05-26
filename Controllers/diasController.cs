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
    public class diasController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/dias
        public IQueryable<dias> Getdias()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.dias;
        }

        // GET: api/dias/5
        [ResponseType(typeof(dias))]
        public async Task<IHttpActionResult> Getdias(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            dias dias = await db.dias.FindAsync(id);
            if (dias == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(dias);
            }

            return result;
        }

        // PUT: api/dias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdias(int id, dias dias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dias.id)
            {
                return BadRequest();
            }

            db.Entry(dias).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!diasExists(id))
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

        // POST: api/dias
        [ResponseType(typeof(dias))]
        public async Task<IHttpActionResult> Postdias(dias dias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.dias.Add(dias);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dias.id }, dias);
        }

        // DELETE: api/dias/5
        [ResponseType(typeof(dias))]
        public async Task<IHttpActionResult> Deletedias(int id)
        {
            dias dias = await db.dias.FindAsync(id);
            if (dias == null)
            {
                return NotFound();
            }

            db.dias.Remove(dias);
            await db.SaveChangesAsync();

            return Ok(dias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool diasExists(int id)
        {
            return db.dias.Count(e => e.id == id) > 0;
        }
    }
}