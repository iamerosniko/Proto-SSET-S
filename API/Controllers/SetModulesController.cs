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
    public class SetModulesController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/SetModules
        public IQueryable<SetModule> GetSetModules()
        {
            return db.SetModules;
        }

        // GET: api/SetModules/5
        [ResponseType(typeof(SetModule))]
        public async Task<IHttpActionResult> GetSetModule(string id)
        {
            SetModule setModule = await db.SetModules.FindAsync(id);
            if (setModule == null)
            {
                return NotFound();
            }

            return Ok(setModule);
        }

        // PUT: api/SetModules/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSetModule(string id, SetModule setModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setModule.mod_id)
            {
                return BadRequest();
            }

            db.Entry(setModule).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetModuleExists(id))
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

        // POST: api/SetModules
        [ResponseType(typeof(SetModule))]
        public async Task<IHttpActionResult> PostSetModule(SetModule setModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SetModules.Add(setModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SetModuleExists(setModule.mod_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = setModule.mod_id }, setModule);
        }

        // DELETE: api/SetModules/5
        [ResponseType(typeof(SetModule))]
        public async Task<IHttpActionResult> DeleteSetModule(string id)
        {
            SetModule setModule = await db.SetModules.FindAsync(id);
            if (setModule == null)
            {
                return NotFound();
            }

            db.SetModules.Remove(setModule);
            await db.SaveChangesAsync();

            return Ok(setModule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetModuleExists(string id)
        {
            return db.SetModules.Count(e => e.mod_id == id) > 0;
        }
    }
}