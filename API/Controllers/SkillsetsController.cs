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
    public class SkillsetsController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/Skillsets
        public IQueryable<Skillset> GetSkillsets()
        {
            return db.Skillsets;
        }

        // GET: api/Skillsets/5
        [ResponseType(typeof(Skillset))]
        public async Task<IHttpActionResult> GetSkillset(int id)
        {
            Skillset skillset = await db.Skillsets.FindAsync(id);
            if (skillset == null)
            {
                return NotFound();
            }

            return Ok(skillset);
        }

        // PUT: api/Skillsets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSkillset(int id, Skillset skillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skillset.SkillsetID)
            {
                return BadRequest();
            }

            db.Entry(skillset).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillsetExists(id))
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

        // POST: api/Skillsets
        [ResponseType(typeof(Skillset))]
        public async Task<IHttpActionResult> PostSkillset(Skillset skillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            skillset.IsActive = true;

            db.Skillsets.Add(skillset);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = skillset.SkillsetID }, skillset);
        }

        // DELETE: api/Skillsets/5
        [ResponseType(typeof(Skillset))]
        public async Task<IHttpActionResult> DeleteSkillset(int id)
        {
            Skillset skillset = await db.Skillsets.FindAsync(id);
            if (skillset == null)
            {
                return NotFound();
            }

            db.Skillsets.Remove(skillset);
            await db.SaveChangesAsync();

            return Ok(skillset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillsetExists(int id)
        {
            return db.Skillsets.Count(e => e.SkillsetID == id) > 0;
        }
    }
}