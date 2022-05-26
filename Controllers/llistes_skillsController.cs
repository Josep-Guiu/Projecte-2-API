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
    public class llistes_skillsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/llistes_skills
        public IQueryable<llistes_skills> Getllistes_skills()
        {

            db.Configuration.LazyLoadingEnabled = false;

            return db.llistes_skills;

        }

        // GET: api/llistes_skills/5
        [ResponseType(typeof(llistes_skills))]
        public async Task<IHttpActionResult> Getllistes_skills(int id)
        {
            IHttpActionResult result;

            db.Configuration.LazyLoadingEnabled = false;

            llistes_skills llistes_skills = await db.llistes_skills.FindAsync(id);
            if (llistes_skills == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(llistes_skills);

            }

            return result;
        }

        // PUT: api/llistes_skills/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putllistes_skills(int id, llistes_skills llistes_skills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != llistes_skills.id)
            {
                return BadRequest();
            }

            db.Entry(llistes_skills).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!llistes_skillsExists(id))
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

        // POST: api/llistes_skills
        [ResponseType(typeof(llistes_skills))]
        public async Task<IHttpActionResult> Postllistes_skills(llistes_skills llistes_skills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.llistes_skills.Add(llistes_skills);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = llistes_skills.id }, llistes_skills);
        }

        // DELETE: api/llistes_skills/5
        [ResponseType(typeof(llistes_skills))]
        public async Task<IHttpActionResult> Deletellistes_skills(int id)
        {
            llistes_skills llistes_skills = await db.llistes_skills.FindAsync(id);
            if (llistes_skills == null)
            {
                return NotFound();
            }

            db.llistes_skills.Remove(llistes_skills);
            await db.SaveChangesAsync();

            return Ok(llistes_skills);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool llistes_skillsExists(int id)
        {
            return db.llistes_skills.Count(e => e.id == id) > 0;
        }
    }
}