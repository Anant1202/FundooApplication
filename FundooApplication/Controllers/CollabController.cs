using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [HttpPost("Create")]
        public IActionResult Create(CollabModel collabModel, long userid, long noteid)
        {
            long id = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = collabBL.CreateCollab(collabModel, userid,noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Collab is Created Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Collab is Not Created" });
            }
        }
        [HttpGet("Retrieve")]
        public IActionResult GetCollab()
        {
            var result = collabBL.GetCollabDetails();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Collab is not Retrieved" });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long CollabID)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = collabBL.DeleteCollab(CollabID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Delete is Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Delete is Unsuccessful" });
            }
        }
    }
}
