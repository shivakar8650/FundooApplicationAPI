using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL ColabBL;
        public CollaboratorController(ICollaboratorBL ColabsBL)
        {
            this.ColabBL = ColabsBL;
        }
        [Authorize]
        [HttpPut]
        [Route("AddUser")]
        public IActionResult CollaborateWithUser(NoteCollaborate collaborate)
        {
            try
            {
                long UserId =Convert.ToInt64( User.FindFirst("UserId").Value);
                if (this.ColabBL.NoteCollaborate(collaborate,UserId))
                {
                    return this.Ok(new { Status = true, Message = "Note Shared successfully" });
                }

                return this.BadRequest(new { Status = false, Message = "You Do not have permission" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("RemoveUSer")]
        public IActionResult RemoveCollaborateWithUser(NoteCollaborate collaborate)
        {
            try
            {
                long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);
                 string response =this.ColabBL.RemoveCollaborate(collaborate, UserId);
                if (response != null)
                    return this.Ok(new {  Message = response });
                else
                    return this.BadRequest(new { Status = false, Message = response });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

    }
}
