using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationApi3.Models;

namespace WebApplicationApi3.Controllers
{
    public class usuarisController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/usuaris

        public IHttpActionResult Getusuaris()
        {

            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<usuaris> _usuaris = db.usuaris
                .Include("grups_has_alumnes")
                .Include("grups_has_docents")
                .ToList();
            return Ok(_usuaris);
        }

        // GET: api/usuaris/5
        [ResponseType(typeof(usuaris))]
        public async Task<IHttpActionResult> Getusuaris(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            //usuaris usuaris = await db.usuaris.FindAsync(id);

            usuaris _usuari = await db.usuaris
                .Include("grups_has_alumnes")
                 .Include("grups_has_docents")
                .Where(x => x.id == id)
                .FirstOrDefaultAsync();
            if (_usuari == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(_usuari);
            }

            return result;
        }

        /*Buscar por Nombre*/
        [HttpGet]
        [Route("api/usuaris/nom/{nom}")]
        public IHttpActionResult BuscaPerNom(String nom)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<usuaris> _usuaris = db.usuaris
                .Where(x => x.nom.Contains(nom))
                .ToList();
            return Ok(_usuaris);
        }

        /*Buscar por Correo*/
        [HttpGet]
        [Route("api/usuaris/correu/{correu}")]
        public IHttpActionResult BuscaPerCorreu(String correu)
        {

            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<usuaris> _usuaris = db.usuaris
                .Include("grups_has_alumnes")
                .Include("grups_has_docents")
                .Where(x => x.correu.Equals(correu))
                .ToList();
            return Ok(_usuaris);

        }

        // PUT: api/usuaris/5
        [HttpPost]
        [Route("api/usuaris/update/{id}")]

        public async Task<IHttpActionResult> UpdateUsuaris(int id, usuaris _usuari)
        {
            IHttpActionResult result;
            String missatge = "";
            if (!ModelState.IsValid)
            {
                result = BadRequest(ModelState);
            }
            else
            {
                if (id != _usuari.id)
                {
                    result = BadRequest();
                }
                else
                {

                    db.Entry(_usuari).State = EntityState.Modified;

                    try
                    {

                        await db.SaveChangesAsync();
                        result = StatusCode(HttpStatusCode.NoContent);
                    }

                    catch (DbUpdateConcurrencyException)
                    {
                        if (!usuarisExists(id))
                        {
                            result = NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        SqlException sqlException = (SqlException)ex.InnerException.InnerException;
                        missatge = WebApplicationApi3.Clase.Utilitat.Errores(sqlException);
                        result = BadRequest(missatge);
                    }
                }

            }

            return result;
        }

        // POST: api/usuaris
        [ResponseType(typeof(usuaris))]
        public async Task<IHttpActionResult> Postusuaris(usuaris usuaris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usuaris.Add(usuaris);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usuaris.id }, usuaris);
        }


        // DELETE: api/usuaris/5
        [ResponseType(typeof(usuaris))]
        public async Task<IHttpActionResult> Deleteusuaris(int id)
        {
            usuaris usuaris = await db.usuaris.FindAsync(id);
            if (usuaris == null)
            {
                return NotFound();
            }

            db.usuaris.Remove(usuaris);
            await db.SaveChangesAsync();

            return Ok(usuaris);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarisExists(int id)
        {
            return db.usuaris.Count(e => e.id == id) > 0;
        }
    }
}