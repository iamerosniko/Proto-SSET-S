using API.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    [Authorize]
    public class SetGroupsController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/SetGroups
        public IQueryable<SetGroup> GetSetGroups()
        {
            return db.SetGroups;
        }

        // GET: api/SetGroups/5
        [ResponseType(typeof(SetGroup))]
        public async Task<IHttpActionResult> GetSetGroup(string id)
        {
            SetGroup setGroup = await db.SetGroups.FindAsync(id);
            if (setGroup == null)
            {
                return NotFound();
            }

            return Ok(setGroup);
        }

        // PUT: api/SetGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSetGroup(string id, SetGroup setGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setGroup.grp_id)
            {
                return BadRequest();
            }

            db.Entry(setGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetGroupExists(id))
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

        // POST: api/SetGroups
        [ResponseType(typeof(SetGroup))]
        public async Task<IHttpActionResult> PostSetGroup(SetGroup setGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SetGroups.Add(setGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SetGroupExists(setGroup.grp_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = setGroup.grp_id }, setGroup);
        }

        // DELETE: api/SetGroups/5
        [ResponseType(typeof(SetGroup))]
        public async Task<IHttpActionResult> DeleteSetGroup(string id)
        {
            SetGroup setGroup = await db.SetGroups.FindAsync(id);
            if (setGroup == null)
            {
                return NotFound();
            }

            db.SetGroups.Remove(setGroup);
            await db.SaveChangesAsync();

            return Ok(setGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetGroupExists(string id)
        {
            return db.SetGroups.Count(e => e.grp_id == id) > 0;
        }
    }
}