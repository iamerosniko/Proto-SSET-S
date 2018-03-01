using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace Client.Controllers
{
    public class ValuesController : ApiController
    {
        // GET: api/Values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            try
            {
                var session = HttpContext.Current.Session;

                if (session["Time"] == null)
                    session["Time"] = DateTime.Now;

                return "Session Time: " + session["Time"] + id;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
