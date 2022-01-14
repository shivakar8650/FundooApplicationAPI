using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly UserContext context;
        private readonly IDistributedCache distributedCache;
        private readonly INoteBL noteBL;
        public NoteController(INoteBL notesBL, IMemoryCache memoryCache, UserContext context, IDistributedCache distributedCache)
        {
            this.noteBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
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
                    return this.Ok(new { Success = true, message = "New Note created successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "New Node creation unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
    
        [Authorize]
        [HttpGet("AllNotes")]
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
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [Authorize]
        [HttpGet("AllNotesOfUser")]
        public IActionResult GetAllNotesofUSer()
        {
            long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);
            try
            {
                var noteResult = this.noteBL.GetAllNotesOfUser( UserId);
                if (noteResult == null)
                {
                    return this.BadRequest(new { Success = false, message = " Notes records not found" });
                }
                return this.Ok(new { Success = true, message = "Notes records found", notesdata = noteResult });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [Authorize]
        [HttpPut("{Noteid}")]
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
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
   
        [HttpDelete]
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
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPut]
        [Route("Pin")]
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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPut]
        [Route("Archive")]
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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPut]
        [Route("Trash")]
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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPut]
        [Route("Color")]
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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPut]
        [Route("Image")]
        public IActionResult UploadImage(long noteId, IFormFile Image)
        { 
           try
            {
                if (this.noteBL.UploadImage(noteId, Image))
                {
                    return this.Ok(new { Status = true, Message = "Upload Image Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Not Uploaded!" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var CacheKey = "noteDetailsList";
            string serializedNotesList;
            IEnumerable<Note> noteDetailsList = new List<Note>();
            var redisNotesList = await distributedCache.GetAsync(CacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                noteDetailsList = JsonConvert.DeserializeObject<List<Note>>(serializedNotesList);
            }
            else
            {
                noteDetailsList = (IEnumerable<Note>)noteBL.GetAllNotesOfUser(UserId);
                serializedNotesList = JsonConvert.SerializeObject(noteDetailsList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                 .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(CacheKey, redisNotesList, options);
            }
            return Ok(noteDetailsList);
        }
    }
}
    