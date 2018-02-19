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
    public class SetGroupAccessesController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/SetGroupAccesses
        public IQueryable<SetGroupAccess> GetSetGroupAccesses()
        {
            return db.SetGroupAccesses;
        }

        // GET: api/SetGroupAccesses/5
        [ResponseType(typeof(SetGroupAccess))]
        public async Task<IHttpActionResult> GetSetGroupAccess(int id)
        {
            SetGroupAccess setGroupAccess = await db.SetGroupAccesses.FindAsync(id);
            if (setGroupAccess == null)
            {
                return NotFound();
            }

            return Ok(setGroupAccess);
        }

        // PUT: api/SetGroupAccesses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSetGroupAccess(int id, SetGroupAccess setGroupAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setGroupAccess.grp_mod_id)
            {
                return BadRequest();
            }

            db.Entry(setGroupAccess).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetGroupAccessExists(id))
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

        // POST: api/SetGroupAccesses
        [ResponseType(typeof(SetGroupAccess))]
        public async Task<IHttpActionResult> PostSetGroupAccess(SetGroupAccess setGroupAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SetGroupAccesses.Add(setGroupAccess);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = setGroupAccess.grp_mod_id }, setGroupAccess);
        }

        // DELETE: api/SetGroupAccesses/5
        [ResponseType(typeof(SetGroupAccess))]
        public async Task<IHttpActionResult> DeleteSetGroupAccess(int id)
        {
            SetGroupAccess setGroupAccess = await db.SetGroupAccesses.FindAsync(id);
            if (setGroupAccess == null)
            {
                return NotFound();
            }

            db.SetGroupAccesses.Remove(setGroupAccess);
            await db.SaveChangesAsync();

            return Ok(setGroupAccess);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetGroupAccessExists(int id)
        {
            return db.SetGroupAccesses.Count(e => e.grp_mod_id == id) > 0;
        }
    }
}