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
using API.Entities;

namespace API.Controllers
{
    public class SetUserAccessesController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/SetUserAccesses
        public IQueryable<SetUserAccess> GetSetUserAccesses()
        {
            return db.SetUserAccesses;
        }

        // GET: api/SetUserAccesses/5
        [ResponseType(typeof(SetUserAccess))]
        public async Task<IHttpActionResult> GetSetUserAccess(int id)
        {
            SetUserAccess setUserAccess = await db.SetUserAccesses.FindAsync(id);
            if (setUserAccess == null)
            {
                return NotFound();
            }

            return Ok(setUserAccess);
        }

        // PUT: api/SetUserAccesses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSetUserAccess(int id, SetUserAccess setUserAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setUserAccess.user_grp_id)
            {
                return BadRequest();
            }

            db.Entry(setUserAccess).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetUserAccessExists(id))
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

        // POST: api/SetUserAccesses
        [ResponseType(typeof(SetUserAccess))]
        public async Task<IHttpActionResult> PostSetUserAccess(SetUserAccess setUserAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SetUserAccesses.Add(setUserAccess);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = setUserAccess.user_grp_id }, setUserAccess);
        }

        // DELETE: api/SetUserAccesses/5
        [ResponseType(typeof(SetUserAccess))]
        public async Task<IHttpActionResult> DeleteSetUserAccess(int id)
        {
            SetUserAccess setUserAccess = await db.SetUserAccesses.FindAsync(id);
            if (setUserAccess == null)
            {
                return NotFound();
            }

            db.SetUserAccesses.Remove(setUserAccess);
            await db.SaveChangesAsync();

            return Ok(setUserAccess);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetUserAccessExists(int id)
        {
            return db.SetUserAccesses.Count(e => e.user_grp_id == id) > 0;
        }
    }
}