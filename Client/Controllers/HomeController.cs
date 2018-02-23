using Client.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : AsyncController
    {
        private string _authToken;
        private HttpClient _client;
        //private TokenFactory _tokenFactory;
        public ActionResult Index()
        {
            var authSession = Session["authToken"];
            //_authToken = .ToString();
            if (authSession == null)
            {
                return Redirect("Home/SignIn");
            }
            else
                _authToken = authSession.ToString();
            return View();
        }
        public ViewResult SignIn()
        {
            //request a post to IDP server to gain an AuthToken
            GetAuthentication();
            //var data = ConfigurationSettings.AppSettings["ClientURL"];
            var data = ConfigurationManager.AppSettings["ClientURL"];
            ViewData["homepage"] = data;
            ViewData["sample"] = Session["authToken"];
            return View();
        }

        public void GetAuthentication()
        {
            string authToken = "";
            ApiAccess api = new ApiAccess("Login");
            var asyncTask = Task.Run(() =>
            {
                var temp = api.PostRequest("");
                return temp.Result;
            });
            //asyncTask.RunSynchronously();

            authToken = asyncTask.Result.Trim(new char[] { '\"' });
            Session["authToken"] = authToken;
            GetCurrentUser(authToken);
        }

        public void GetCurrentUser(string token)
        {
            UsersController tc = new UsersController();
            //extract token
            CurrentUser user = tc.TokenToDetails(token);
            //save current user
            Session["currentUser"] = JsonConvert.SerializeObject(user);
        }
    }
}
