using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApplication.Controllers
{
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
            var result = notesBL.CreateNote(noteCreateModel);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Note is Created Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note is Not Created" });
            }
        }
    }
}
