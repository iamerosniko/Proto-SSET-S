using Client.Models;
using System.Web;
using System.Web.Http;

namespace Client.Controllers
{
    public class SessionsController : ApiController
    {
        // GET: api/Sessions
        [HttpGet]
        [Route("api/books/{a}")]
        public string get(string a)
        {
            var x = HttpContext.Current.Session["authToken"];
            return x == null ? null : x.ToString();
        }

        [HttpGet]
        [Route("api/books1/{a}/{b}")]
        public string get(string a, string b)
        {
            HttpContext.Current.Session[a] = b;
            return b;
        }

        [HttpPost]
        [Route("api/save")]
        public void saveToke(MyToken token)
        {
            HttpContext.Current.Session["authToken"] = token.TokenName;
        }
    }

}
