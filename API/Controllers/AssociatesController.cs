using API.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class AssociatesController : ApiController
    {
        private SkillSetContext db = new SkillSetContext();

        // GET: api/Associates
        public IQueryable<Associate> GetAssociates()
        {
            return db.Associates;
        }

        // GET: api/Associates/5
        [ResponseType(typeof(Associate))]
        public async Task<IHttpActionResult> GetAssociate(int id)
        {
            Associate associate = await db.Associates.FindAsync(id);
            if (associate == null)
            {
                return NotFound();
            }

            return Ok(associate);
        }

        // PUT: api/Associates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAssociate(int id, Associate associate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != associate.AssociateID)
            {
                return BadRequest();
            }

            associate.UpdatedOn = DateTime.Now;

            db.Entry(associate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssociateExists(id))
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

        // POST: api/Associates
        [ResponseType(typeof(Associate))]
        public async Task<IHttpActionResult> PostAssociate(Associate associate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            associate.IsActive = true;
            associate.UpdatedOn = DateTime.Now;

            db.Associates.Add(associate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = associate.AssociateID }, associate);
        }

        // DELETE: api/Associates/5
        [ResponseType(typeof(Associate))]
        public async Task<IHttpActionResult> DeleteAssociate(int id)
        {
            Associate associate = await db.Associates.FindAsync(id);
            if (associate == null)
            {
                return NotFound();
            }

            db.Associates.Remove(associate);
            await db.SaveChangesAsync();

            return Ok(associate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssociateExists(int id)
        {
            return db.Associates.Count(e => e.AssociateID == id) > 0;
        }
    }
}