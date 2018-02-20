using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private string _authToken;
        private string _apiToken;
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
            //getAuthentication();
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("secureAppSettings");
            ViewData["homepage"] = section["ClientURL"];
            return View();
        }
    }
}
