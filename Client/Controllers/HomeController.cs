using Client.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private string _authToken;
        private HttpClient _client;
        //private TokenFactory _tokenFactory;
        public ActionResult Index()
        {

            var authSession = HttpContext.Session["authToken"];

            if (authSession == null)
            {
                return Redirect("Home/SignIn");
            }
            else
            {
                _authToken = authSession.ToString();
                GetCurrentUser(_authToken);
            }
            return View();
        }
        public async Task<ActionResult> SignIn()
        {
            var data = ConfigurationManager.AppSettings["ClientURL"];
            ViewData["homepage"] = data;
            //request a post to IDP server to gain an AuthToken
            string authToken = "";
            ApiAccess api = new ApiAccess("Login");
            var asyncTask = await api.PostRequest("");

            authToken = asyncTask == null ? null : asyncTask.Trim(new char[] { '\"' });
            Session["authToken"] = authToken;
            ViewData["sample"] = Session["authToken"];

            //var data = ConfigurationSettings.AppSettings["ClientURL"];
            return View();
        }
        public void GetCurrentUser(string token)
        {
            UsersController tc = new UsersController();
            CurrentUser user = tc.TokenToDetails(token);
            Session["currentUser"] = JsonConvert.SerializeObject(user);
        }
    }
}
