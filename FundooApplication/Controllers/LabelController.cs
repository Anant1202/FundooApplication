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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [HttpPost("Create")]
        public IActionResult Create(LabelModel labelModel, long noteid)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = labelBL.CreateLabel(labelModel, UserId, noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Label is Created Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Label is Not Created" });
            }
        }
        [HttpGet("Retrieve")]
        public IActionResult GetLabel()
        {
            var result = labelBL.GetAllLabel();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Label is not Retrieved" });
            }
        }
        [HttpPut("Update")]
        public IActionResult Update(LabelModel labelModel, long LabelID)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = labelBL.UpdateLabel(labelModel,LabelID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Update is Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Update is Unsuccessful" });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long LabelID)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = labelBL.DeleteLabel(LabelID);
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
