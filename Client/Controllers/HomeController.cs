using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
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
            _authToken = Session["authToken"].ToString();
            if (_authToken == null)
            {
                return Redirect("Home/SignIn");
            }
            return View();
        }
        public ActionResult SignIn()
        {
            //request a post to IDP server to gain an AuthToken
            GetAuthentication();
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("appSettings");
            ViewData["homepage"] = section["ClientURL"];
            return View();
        }

        public void GetAuthentication()
        {
            ApiAccess api = new ApiAccess("Login");

            var authToken = api.PostRequest("");
            Session["authToken"] = authToken;
        }
    }
}
