using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult Get(long id)
        {
            var result = notesBL.GetNote(id);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is not Retrieved" });
            }
        }
    }
}
