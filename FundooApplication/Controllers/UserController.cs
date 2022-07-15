using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
