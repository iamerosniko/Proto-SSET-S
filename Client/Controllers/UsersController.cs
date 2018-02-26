using Client.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private string _authToken;

        public CurrentUser TokenToDetails(string token)
        {
            CurrentUser currentUser = new CurrentUser();
            JwtSecurityToken jwtToken = new JwtSecurityToken(token);

            var roles = jwtToken.Claims.
                Where(x => x.Type == "role").Select(x => x.Value);

            var idS = jwtToken.Claims.
                Where(x => x.Type == ClaimTypes.Sid).Select(x => x.Value);

            var firstNames = jwtToken.Claims.
                Where(x => x.Type == "given_name").Select(x => x.Value);

            var surnames = jwtToken.Claims.
                Where(x => x.Type == "family_name").Select(x => x.Value);

            System.Diagnostics.Debug.Write(jwtToken.Claims.ElementAt(2).Type);

            currentUser.FirstName = firstNames != null ? firstNames.FirstOrDefault() : "";
            currentUser.LastName = surnames != null ? surnames.FirstOrDefault() : "";
            currentUser.Role = roles != null ? roles.FirstOrDefault() : "";
            currentUser.UserID = idS != null ? idS.FirstOrDefault() : "";


            return currentUser;

        }

        public string GetCurrentUser()
        {
            //HttpContext.Session["authTOken"].ToString() ;
            var temp = Session["currentUser"];
            string currentUser = "";
            if (temp != null)
            {

                currentUser = temp.ToString();
            }
            else
            {
                goToSignIn();
            }

            return currentUser;
        }

        public string GetToken()
        {
            _authToken = Session["authToken"].ToString();

            AppToken appToken = new AppToken();
            appToken = new AppToken { Token = _authToken, TokenName = "AuthToken" };

            return JsonConvert.SerializeObject(appToken);
        }

        public ActionResult goToSignIn()
        {
            return Redirect("Home/SignIn");
        }
    }
    public class AppToken
    {
        public string TokenName { get; set; }
        public string Token { get; set; }
    }
}