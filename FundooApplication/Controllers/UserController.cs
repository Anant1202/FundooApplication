using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            var result = userBL.Register(userRegistrationModel);
            if(result != null)
            {
                return this.Ok(new { success=true,message="Registration is Successful",data=result});
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Registration is Unsuccessful"});
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
                return this.NotFound(new { success = false, message = "Reset is Unsuccessful" });
            }
        }
    }
}
