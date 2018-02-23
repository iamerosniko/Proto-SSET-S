using Client.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
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
            var currentUser = Session["currentUser"].ToString();

            return currentUser;
        }
    }
}