using BusinessLayer.Interfaces;
using CommonLayer.Model;
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
        [HttpPost]
        public IActionResult GenerateNote(UserNote notes)
        {
            try
            {
                if (this.noteBL.GenerateNote(notes))
                {
                    return this.Ok(new { Success = true, message = "New note created successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "New node creation unsuccessful" });
                }

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.InnerException });
            }
        }

        [HttpGet("getAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var noteResult= this.noteBL.GetAllNotes();
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

        [HttpPut("{id}")]
        public IActionResult UpdateNotes(UserNote notes)
        {
            try
            {
                UserNote response = noteBL.UpdateNotes(notes);
                if (response != null)
                {
                    return this.Ok(new { Success = true, message = " Registration Deleted" });
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
        public IActionResult DeleteNotes(long Id)
        {
            long ID = Id;
            try
            {
                if (this.noteBL.DeleteNotes( ID))
                {
                    return this.Ok(new { Success = true, message = " Registration Deleted" });
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
    }
}
    