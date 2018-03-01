using SkillsetClient.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SkillsetClient.Controllers
{
    public class HomeController : Controller
    {

        private string _user;
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public JsonResult GetUserCreds()
        {
            _user = HttpContext.User.Identity.Name.ToString();

            if (_user.Equals("") || _user == null)
            {
                _user = _user.StartsWith("PRD") ? _user.Substring(3) : _user;
                _user = _user.StartsWith("MLIDDOMAIN1") || _user.StartsWith("MLIDDOMAIN2") ? _user.Substring(10) : _user;
            }
            else
            {
                _user = Environment.UserName;
            }

            return Json(new CurrentUser
            {
                FirstName = "",
                UserID = _user
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TokenToDetails(MyToken token)
        {
            CurrentUser currentUser = new CurrentUser();
            JwtSecurityToken jwtToken = new JwtSecurityToken(token.TokenValue);

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


            return Json(currentUser, JsonRequestBehavior.AllowGet);

        }
    }
}
