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
    public class AssociateDepartmentSkillsetsController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/AssociateDepartmentSkillsets
        public IQueryable<AssociateDepartmentSkillset> GetAssociateDepartmentSkillsets()
        {
            return db.AssociateDepartmentSkillsets;
        }

        // GET: api/AssociateDepartmentSkillsets/5
        [ResponseType(typeof(AssociateDepartmentSkillset))]
        public async Task<IHttpActionResult> GetAssociateDepartmentSkillset(int id)
        {
            AssociateDepartmentSkillset associateDepartmentSkillset = await db.AssociateDepartmentSkillsets.FindAsync(id);
            if (associateDepartmentSkillset == null)
            {
                return NotFound();
            }

            return Ok(associateDepartmentSkillset);
        }

        // PUT: api/AssociateDepartmentSkillsets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAssociateDepartmentSkillset(int id, AssociateDepartmentSkillset associateDepartmentSkillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != associateDepartmentSkillset.AssociateDepartmentSkillsetID)
            {
                return BadRequest();
            }

            db.Entry(associateDepartmentSkillset).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssociateDepartmentSkillsetExists(id))
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

        // POST: api/AssociateDepartmentSkillsets
        [ResponseType(typeof(AssociateDepartmentSkillset))]
        public async Task<IHttpActionResult> PostAssociateDepartmentSkillset(AssociateDepartmentSkillset associateDepartmentSkillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssociateDepartmentSkillsets.Add(associateDepartmentSkillset);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = associateDepartmentSkillset.AssociateDepartmentSkillsetID }, associateDepartmentSkillset);
        }

        // DELETE: api/AssociateDepartmentSkillsets/5
        [ResponseType(typeof(AssociateDepartmentSkillset))]
        public async Task<IHttpActionResult> DeleteAssociateDepartmentSkillset(int id)
        {
            AssociateDepartmentSkillset associateDepartmentSkillset = await db.AssociateDepartmentSkillsets.FindAsync(id);
            if (associateDepartmentSkillset == null)
            {
                return NotFound();
            }

            db.AssociateDepartmentSkillsets.Remove(associateDepartmentSkillset);
            await db.SaveChangesAsync();

            return Ok(associateDepartmentSkillset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssociateDepartmentSkillsetExists(int id)
        {
            return db.AssociateDepartmentSkillsets.Count(e => e.AssociateDepartmentSkillsetID == id) > 0;
        }
    }
}