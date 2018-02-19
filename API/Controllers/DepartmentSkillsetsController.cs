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
    public class DepartmentSkillsetsController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/DepartmentSkillsets
        public IQueryable<DepartmentSkillset> GetDepartmentSkillsets()
        {
            return db.DepartmentSkillsets;
        }

        // GET: api/DepartmentSkillsets/5
        [ResponseType(typeof(DepartmentSkillset))]
        public async Task<IHttpActionResult> GetDepartmentSkillset(int id)
        {
            DepartmentSkillset departmentSkillset = await db.DepartmentSkillsets.FindAsync(id);
            if (departmentSkillset == null)
            {
                return NotFound();
            }

            return Ok(departmentSkillset);
        }

        // PUT: api/DepartmentSkillsets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDepartmentSkillset(int id, DepartmentSkillset departmentSkillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departmentSkillset.DepartmentSkillsetID)
            {
                return BadRequest();
            }

            db.Entry(departmentSkillset).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentSkillsetExists(id))
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

        // POST: api/DepartmentSkillsets
        [ResponseType(typeof(DepartmentSkillset))]
        public async Task<IHttpActionResult> PostDepartmentSkillset(DepartmentSkillset departmentSkillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DepartmentSkillsets.Add(departmentSkillset);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = departmentSkillset.DepartmentSkillsetID }, departmentSkillset);
        }

        // DELETE: api/DepartmentSkillsets/5
        [ResponseType(typeof(DepartmentSkillset))]
        public async Task<IHttpActionResult> DeleteDepartmentSkillset(int id)
        {
            DepartmentSkillset departmentSkillset = await db.DepartmentSkillsets.FindAsync(id);
            if (departmentSkillset == null)
            {
                return NotFound();
            }

            db.DepartmentSkillsets.Remove(departmentSkillset);
            await db.SaveChangesAsync();

            return Ok(departmentSkillset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentSkillsetExists(int id)
        {
            return db.DepartmentSkillsets.Count(e => e.DepartmentSkillsetID == id) > 0;
        }
    }
}