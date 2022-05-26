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
    public class valoracionsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/valoracions
        public IQueryable<valoracions> Getvaloracions()
        {

            db.Configuration.LazyLoadingEnabled = false;
            return db.valoracions;
        }

        // GET: api/valoracions/5
        [ResponseType(typeof(valoracions))]
        public async Task<IHttpActionResult> Getvaloracions(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            valoracions valoracions = await db.valoracions.FindAsync(id);
            if (valoracions == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(valoracions);
            }
            return result;
        }

        // PUT: api/valoracions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvaloracions(int id, valoracions valoracions)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != valoracions.kpis_id)
            {
                return BadRequest();
            }

            db.Entry(valoracions).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!valoracionsExists(id))
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

        // POST: api/valoracions

        //[ResponseType(typeof(valoracions))]
        //public async Task<IHttpActionResult> Postvaloracions(valoracions valoracions)
        //{

        //    IHttpActionResult result;

        //    if (!ModelState.IsValid)

        //    {
        //        result = BadRequest(ModelState);
        //    }
        //    else
        //    {

        //        db.valoracions.Add(valoracions);
        //        String missatge = "";


        //    try
        //    {
        //        await db.SaveChangesAsync();
        //        result = CreatedAtRoute("DefaultApi", new { id = valoracions }, valoracions);
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        SqlException sqlException = (SqlException)ex.InnerException.InnerException;
        //            result = BadRequest(missatge);
        //        }
        //    }

        //    return result;
        //}



        // DELETE: api/valoracions/5
        [ResponseType(typeof(valoracions))]
        public async Task<IHttpActionResult> Deletevaloracions(int id)
        {
            valoracions valoracions = await db.valoracions.FindAsync(id);
            if (valoracions == null)
            {
                return NotFound();
            }

            db.valoracions.Remove(valoracions);
            await db.SaveChangesAsync();

            return Ok(valoracions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool valoracionsExists(int id)
        {
            return db.valoracions.Count(e => e.kpis_id == id) > 0;
        }

        /*Buscar por id*/
        [HttpGet]
        [Route("api/valoracions/nom/{nom}")]
        public async Task<IHttpActionResult> BuscaPerNom(usuaris usuarioid, kpis kpis1)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<valoracions> _valoracio = usuarioid.valoracions.Where(x => x.kpis == kpis1).ToList();
            return Ok(_valoracio);
        }




        [ResponseType(typeof(valoracions))]
        public async Task<IHttpActionResult> Postvaloracions(valoracions valoracions)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.valoracions.Add(valoracions);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = valoracions.kpis_id }, valoracions);
            }

        }
    }
}