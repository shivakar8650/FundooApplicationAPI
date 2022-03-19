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
                    return this.BadRequest(new { Success = false, message = "New Note creation unsuccessful" });
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
        [HttpPut]
        public IActionResult UpdateNotes(UpdateNotes notes)
        {
            long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);
            try
            {
                UpdateNotes response = noteBL.UpdateNotes(notes, UserId);
                if (response != null)
                {
                    return this.Ok(new { Success = true, message = " note updated successfully!", Updated = response });
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
        public IActionResult DeleteNotes(takeNoteId note)
        {
            try
            {
                if (this.noteBL.DeleteNotes(note))
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
        public IActionResult ArchiveORUnarchiveNote(takeNoteId note)
        {
            try
            {
                var result = this.noteBL.ArchiveORUnarchiveNote(note);
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
        public IActionResult TrashOrRestoreNote(takeNoteId note)
        {
            try
            {
                var result = this.noteBL.TrashOrRestoreNote(note);
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
        public IActionResult ChangeColor(ChangeColorNote note)
        {
            try
            {
                var message = this.noteBL.ColorNote(note);
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
    