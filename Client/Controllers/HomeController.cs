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
                //Session["authToken"];
            //_authToken = .ToString();
            if (authSession == null)
            {
                return Redirect("Home/SignIn");
            }
            else
                _authToken = authSession.ToString();
            return View();
        }
        public async Task<ActionResult> SignIn()
        {
            //request a post to IDP server to gain an AuthToken

            GetAuthentication();
            var data = ConfigurationManager.AppSettings["ClientURL"];
            ViewData["homepage"] = data;
            ViewData["sample"] = Session["authToken"];


            //var data = ConfigurationSettings.AppSettings["ClientURL"];
            return View();
        }




        public async void GetAuthentication()
        {
            string authToken = "";
            ApiAccess api = new ApiAccess("Login");
            //var asyncTask = Task.Run(() =>
            //{
            //    var temp = api.PostRequest("");
            //    return temp.Result;
            //});
            var asyncTask = await api.PostRequest("");
            //asyncTask.RunSynchronously();

            authToken = asyncTask == null ? "" : asyncTask.Trim(new char[] { '\"' });
            Session["authToken"] = authToken;
            GetCurrentUser(authToken);
        }

        public void GetCurrentUser(string token)
        {
            UsersController tc = new UsersController();
            //extract token
            CurrentUser user = tc.TokenToDetails(token);
            //save current user

            //HttpContext.Session.
            Session["currentUser"] = JsonConvert.SerializeObject(user);
        }
    }
}
