using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SHITPurchase.Models;
using SHITPurchase.Data;
using SHITPurchase.Dtos;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

namespace SHITPurchase.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IWebAPIRepo _repository;
        public UsersController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(UserInputDto userInput)
        {
            User user = _repository.GetUserByUsername(userInput.UserName);
            if (user == null) {
                User new_user = new User
                {
                    UserName = userInput.UserName,
                    Password = userInput.Password,
                    Address = userInput.Address
                };
                _repository.RegisterUser(new_user);
                return Ok("User successfully registered.");
            }
            return Ok("Username not available.");
        }

        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ActionResult<string> GetVersionA()
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("username");
            string username = c.Value;
            User user = _repository.GetUserByUsername(username);
            string versionNumber = _repository.GetVersion(user);
            return Ok(versionNumber);
        }
    }
}