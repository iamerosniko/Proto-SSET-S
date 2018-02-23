﻿using API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace API.Controllers
{
    public class LoginController : ApiController
    {

        private CurrentUser currentUser;
        public string token;
        [HttpPost]
        public string Authenticate()
        {
            currentUser = new CurrentUser();
            var username = Environment.UserName;
            var setUsersController = new SetUsersController();
            var setUserAccessController = new SetUserAccessesController();
            var setGroupsController = new SetGroupsController();

            try
            {
                var users = setUsersController.GetSetUsers();
                var groups = setGroupsController.GetSetGroups();
                var userAccesses = setUserAccessController.GetSetUserAccesses();
                var user = users.Where(x => x.user_name == username).FirstOrDefault();

                if (user != null)
                {
                    currentUser.UserID = user.user_id;
                    currentUser.FirstName = user.user_first_name;
                    currentUser.LastName = user.user_last_name;

                    var userAccess = userAccesses.Where(x => x.user_id == user.user_id).FirstOrDefault();

                    if (userAccess != null)
                    {
                        var group = groups.Where(x => x.grp_id == userAccess.grp_id).FirstOrDefault();
                        currentUser.Role = group.grp_name;
                    }
                    else
                        currentUser.Role = "";
                    token = createToken();
                }
            }
            catch
            {
                return "";
            }
            return token;
        }
        [HttpGet]
        public string sample()
        {
            return "im workin";
        }

        private string createToken()
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.GivenName, currentUser.FirstName ),
                new Claim(ClaimTypes.Surname, currentUser.LastName ),
                new Claim(ClaimTypes.Role, currentUser.Role),
                new Claim(ClaimTypes.Sid,currentUser.UserID)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
