using Client.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private SessionsController _sc = new SessionsController();
        private string _authToken;

        private string _apiURL;
        private HttpClient _client;
        private string _user;
        private string _result;
        //private TokenFactory _tokenFactory;
        public  ActionResult Index()
        {
            _user = HttpContext.User.Identity.Name.ToString();
            ViewData["authToken"] = _user==null ? "null" : _user ;

            _client = new HttpClient();
            _apiURL = ConfigurationManager.AppSettings["ClientURL"];
            try
            {
                var request =  _client.GetAsync(_apiURL + "api/books/authToken").Result;
                if (request.IsSuccessStatusCode)
                {
                    _result = request.Content.ReadAsStringAsync().Result;
                    var authSession = _result;
                    if (authSession == null || authSession == "null")
                    {
                        return Redirect("Home/SignIn");
                    }
                    else
                    {
                        _authToken = authSession.ToString();
                        ViewData["authToken"] = authSession.ToString() == null ? _user : authSession.ToString();
                        GetCurrentUser(_authToken);
                    }
                }

            }
            catch (Exception ex)
            {
                ViewData["authToken"] = ex;
            }

            return View();
        }
        public async Task<ActionResult> SignIn()
        {
            _user = HttpContext.User.Identity.Name.ToString();
            
            var data = ConfigurationManager.AppSettings["ClientURL"];
            ViewData["homepage"] = data;
            //request a post to IDP server to gain an AuthToken
            string authToken = "";
            ApiAccess api = new ApiAccess("Login");
            var asyncTask = await api.PostRequest(JsonConvert.SerializeObject(new CurrentUser { UserID = _user }));

            authToken = asyncTask == null ? "no" : asyncTask.Trim(new char[] { '\"' });


            _client = new HttpClient();
            _apiURL = ConfigurationManager.AppSettings["ClientURL"];

            MyToken token = new MyToken();
            token.TokenName = authToken;

            var content = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, "application/json");
            var request = await _client.PostAsync(_apiURL + "api/save", content);

            if (request.IsSuccessStatusCode)
            {
                var _result = request.Content.ReadAsStringAsync().Result;
                TempData["res"] = _result;
            }


            //System.Web.HttpContext.Current.Session["authToken"] = authToken;
            //_sc.get("authToken", authToken);

            return View();
        }
        public void GetCurrentUser(string token)
        {
            UsersController tc = new UsersController();
            //CurrentUser user = tc.TokenToDetails(token);
            //System.Web.HttpContext.Current.Session["currentUser"] = JsonConvert.SerializeObject(user);
        }
    }
}
