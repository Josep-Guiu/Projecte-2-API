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
    public class grups_has_llistes_skillsController : ApiController
    {
        private frase_aluEntities db = new frase_aluEntities();

        // GET: api/grups_has_llistes_skills
        public IQueryable<grups_has_llistes_skills> Getgrups_has_llistes_skills()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.grups_has_llistes_skills;
        }

        // GET: api/grups_has_llistes_skills/5
        [ResponseType(typeof(grups_has_llistes_skills))]
        public async Task<IHttpActionResult> Getgrups_has_llistes_skills(int id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            grups_has_llistes_skills grups_has_llistes_skills = await db.grups_has_llistes_skills.FindAsync(id);
            if (grups_has_llistes_skills == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(grups_has_llistes_skills);
            }

            return result;
        }

        // PUT: api/grups_has_llistes_skills/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgrups_has_llistes_skills(int id, grups_has_llistes_skills grups_has_llistes_skills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grups_has_llistes_skills.grups_id)
            {
                return BadRequest();
            }

            db.Entry(grups_has_llistes_skills).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!grups_has_llistes_skillsExists(id))
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

        // POST: api/grups_has_llistes_skills
        [ResponseType(typeof(grups_has_llistes_skills))]
        public async Task<IHttpActionResult> Postgrups_has_llistes_skills(grups_has_llistes_skills grups_has_llistes_skills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.grups_has_llistes_skills.Add(grups_has_llistes_skills);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (grups_has_llistes_skillsExists(grups_has_llistes_skills.grups_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grups_has_llistes_skills.grups_id }, grups_has_llistes_skills);
        }

        // DELETE: api/grups_has_llistes_skills/5
        [ResponseType(typeof(grups_has_llistes_skills))]
        public async Task<IHttpActionResult> Deletegrups_has_llistes_skills(int id)
        {
            grups_has_llistes_skills grups_has_llistes_skills = await db.grups_has_llistes_skills.FindAsync(id);
            if (grups_has_llistes_skills == null)
            {
                return NotFound();
            }

            db.grups_has_llistes_skills.Remove(grups_has_llistes_skills);
            await db.SaveChangesAsync();

            return Ok(grups_has_llistes_skills);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool grups_has_llistes_skillsExists(int id)
        {
            return db.grups_has_llistes_skills.Count(e => e.grups_id == id) > 0;
        }


        [HttpGet]
        [Route("api/grups_has_llistes_skills/grups_id/{grups_id}")]
        public async Task<IHttpActionResult> BuscaPerId(int grups_id)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;
            List<grups_has_llistes_skills> _grups = db.grups_has_llistes_skills
                .Where(x => x.grups_id == grups_id)
                                 .ToList();
            return Ok(_grups);
        }
    }
}