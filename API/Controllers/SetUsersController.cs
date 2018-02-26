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
    public class SetUsersController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/SetUsers
        public IQueryable<SetUser> GetSetUsers()
        {
            return db.SetUsers;
        }

        // GET: api/SetUsers/5
        [ResponseType(typeof(SetUser))]
        public async Task<IHttpActionResult> GetSetUser(string id)
        {
            SetUser setUser = await db.SetUsers.FindAsync(id);
            if (setUser == null)
            {
                return NotFound();
            }

            return Ok(setUser);
        }

        // PUT: api/SetUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSetUser(string id, SetUser setUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setUser.user_id)
            {
                return BadRequest();
            }

            db.Entry(setUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetUserExists(id))
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

        // POST: api/SetUsers
        [ResponseType(typeof(SetUser))]
        public async Task<IHttpActionResult> PostSetUser(SetUser setUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SetUsers.Add(setUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SetUserExists(setUser.user_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = setUser.user_id }, setUser);
        }

        // DELETE: api/SetUsers/5
        [ResponseType(typeof(SetUser))]
        public async Task<IHttpActionResult> DeleteSetUser(string id)
        {
            SetUser setUser = await db.SetUsers.FindAsync(id);
            if (setUser == null)
            {
                return NotFound();
            }

            db.SetUsers.Remove(setUser);
            await db.SaveChangesAsync();

            return Ok(setUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetUserExists(string id)
        {
            return db.SetUsers.Count(e => e.user_id == id) > 0;
        }
    }
}