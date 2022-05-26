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
    public class botchatsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/botchats
        public IQueryable<botchat> Getbotchat()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.botchat;
        }

        // GET: api/botchats/5
        [ResponseType(typeof(botchat))]
        public async Task<IHttpActionResult> Getbotchat(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            botchat botchat = await db.botchat.FindAsync(id);
            if (botchat == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(botchat);
            }
            return result;


        }
        // PUT: api/botchats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putbotchat(int id, botchat botchat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != botchat.id)
            {
                return BadRequest();
            }

            db.Entry(botchat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!botchatExists(id))
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

        // POST: api/botchats
        [ResponseType(typeof(botchat))]
        public async Task<IHttpActionResult> Postbotchat(botchat botchat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.botchat.Add(botchat);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = botchat.id }, botchat);
        }

        // DELETE: api/botchats/5
        [ResponseType(typeof(botchat))]
        public async Task<IHttpActionResult> Deletebotchat(int id)
        {
            botchat botchat = await db.botchat.FindAsync(id);
            if (botchat == null)
            {
                return NotFound();
            }

            db.botchat.Remove(botchat);
            await db.SaveChangesAsync();

            return Ok(botchat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool botchatExists(int id)
        {
            return db.botchat.Count(e => e.id == id) > 0;
        }
    }
}