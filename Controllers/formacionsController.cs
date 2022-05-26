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
    public class formacionsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/formacions
        public IQueryable<formacion> Getformacion()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.formacion;
        }

        // GET: api/formacions/5
        [ResponseType(typeof(formacion))]
        public async Task<IHttpActionResult> Getformacion(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            formacion formacion = await db.formacion.FindAsync(id);
            if (formacion == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(formacion);
            }

            return result;
        }

        // PUT: api/formacions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putformacion(int id, formacion formacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formacion.id)
            {
                return BadRequest();
            }

            db.Entry(formacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!formacionExists(id))
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

        // POST: api/formacions
        [ResponseType(typeof(formacion))]
        public async Task<IHttpActionResult> Postformacion(formacion formacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.formacion.Add(formacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = formacion.id }, formacion);
        }

        // DELETE: api/formacions/5
        [ResponseType(typeof(formacion))]
        public async Task<IHttpActionResult> Deleteformacion(int id)
        {
            formacion formacion = await db.formacion.FindAsync(id);
            if (formacion == null)
            {
                return NotFound();
            }

            db.formacion.Remove(formacion);
            await db.SaveChangesAsync();

            return Ok(formacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool formacionExists(int id)
        {
            return db.formacion.Count(e => e.id == id) > 0;
        }
    }
}