using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;

        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Register(userRegistrationModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration is Successful", data = result });
                }
                else
                {
                    throw new Exception("Error occured");
                    return this.BadRequest(new { success = false, message = "Registration is Unsuccessful" });
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
                
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = userBL.Login(loginModel);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Login is Successful", data = result });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Login is Unsuccessful" });
            }
        }
        [HttpPost("Forget")]
        public IActionResult Forget(string EmailID)
        {
            var result = userBL.ForgetPassword(EmailID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Mail Send is Successful"});
            }
            else
            {
                return this.NotFound(new { success = false, message = "Mail Send is Unsuccessful" });
            }
        }
        [Authorize]
        [HttpPost("Reset")]
        public IActionResult Reset(ResetModel resetModel)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var result = userBL.ResetPassword(resetModel);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Reset is Successful" });
            }
            else
            {
                return this.Unauthorized(new { success = false, message = "Reset is Unsuccessful" });
            }
        }
    }
}
