using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesBL notesBL;
        public NoteController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        [HttpPost("Create")]
        public IActionResult Create(NoteCreateModel noteCreateModel)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBL.CreateNote(noteCreateModel, userid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note is Created Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is Not Created" });
            }
        }
        [HttpGet("Retrieve")]
        public IActionResult Get()
        {
            var result = notesBL.GetNote();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is not Retrieved" });
            }
        }
        [HttpPut("Update")]
        public IActionResult Update(UpdateNoteModel updateNoteModel,long NoteId)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = notesBL.UpdateNote(updateNoteModel,NoteId);
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
        public IActionResult DeleteNote(long NoteId)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = notesBL.DeleteNote(NoteId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Delete is Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Delete is Unsuccessful" });
            }
        }
        [HttpPost("Archieve")]
        public IActionResult ArchieveNote(long NoteId)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBL.Archieve(NoteId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note is Archieved Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is Not Archieved" });
            }
        }
        [HttpPost("Pin")]
        public IActionResult PinNote(long NoteId)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBL.Pin(NoteId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note is Pinned Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is Not Pinned" });
            }
        }
        [HttpPost("Trash")]
        public IActionResult TrashNote(long NoteId)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = notesBL.Trash(NoteId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note is Trashed Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is Not Trashed" });
            }
        }
        [HttpPost("Image")]
        public IActionResult ImageUpload(long NoteId,IFormFile image)
        {
            var result = notesBL.Image(NoteId,image);
            if (result != null)
            {
                return this.Ok(new { message = "Image uploaded Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { message = "Image upload Unsuccessful" });
            }
        }
    }
}
