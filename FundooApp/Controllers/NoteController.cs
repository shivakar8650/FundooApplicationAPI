using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {

        private readonly INoteBL noteBL;
        public NoteController(INoteBL notesBL)
        {
            this.noteBL = notesBL;
        }
        [Authorize]
        [HttpPost]
        public IActionResult GenerateNote(UserNote notes)
        {
            long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);

            try
            {
                if (this.noteBL.GenerateNote(notes, UserId))
                {
                    return this.Ok(new { Success = true, message = "New Note created successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "New Node creation unsuccessful" });
                }

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.InnerException });
            }
        }
        [Authorize]
        [HttpGet("getAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var noteResult = this.noteBL.GetAllNotes();
                if (noteResult == null)
                {
                    return this.BadRequest(new { Success = false, message = " Notes records not found" });
                }
                return this.Ok(new { Success = true, message = "Notes records found", notesdata = noteResult });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.InnerException });
            }
        }
        [Authorize]
        [HttpPut("Update{Noteid}")]
        public IActionResult UpdateNotes(UserNote notes, long Noteid)
        {
            long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);
            try
            {
                UserNote response = noteBL.UpdateNotes(notes, UserId, Noteid);
                if (response != null)
                {
                    return this.Ok(new { Success = true, message = " Registration Deleted", Updated = response });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Registration Found" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }

        [HttpDelete("Delete")]

        public IActionResult DeleteNotes(long NotesId)
        {

            try
            {
                if (this.noteBL.DeleteNotes(NotesId))
                {
                    return this.Ok(new { Success = true, message = "Deleted successfully.." });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Registration Found" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }

        [HttpPut]
        [Route("PinNote")]
        public IActionResult PinORUnPinNote(long Noteid)
        {
            try
            {
                var result = this.noteBL.PinORUnPinNote(Noteid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("ArchiveNote")]
        public IActionResult ArchiveORUnarchiveNote(long Noteid)
        {
            try
            {
                var result = this.noteBL.ArchiveORUnarchiveNote(Noteid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("TrashOrRestoreNote")]
        public IActionResult TrashOrRestoreNote(long Noteid)
        {
            try
            {
                var result = this.noteBL.TrashOrRestoreNote(Noteid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result});
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("addColor")]
        public IActionResult ChangeColor(long NoteId, string color)
        {
            try
            {
                var message = this.noteBL.ColorNote(NoteId, color);
                if (message.Equals("New Color has set to this note !"))
                {
                    return this.Ok(new { Status = true, Message = message});
                }

                return this.BadRequest(new { Status = true, Message = message });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
    