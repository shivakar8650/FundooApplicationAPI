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
        [Route("User")]
        public IActionResult CollaborateWithUser(NoteCollaborate collaborate)
        {
            try
            {
                long UserId =Convert.ToInt64( User.FindFirst("UserId").Value);
                if (this.ColabBL.NoteCollaborate(collaborate, UserId))
                {
                    return this.Ok(new { Status = true, Message = "Note Shared successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "You Do not have permission" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("User")]
        public IActionResult RemoveCollaborateWithUser(NoteCollaborate collaborate)
        {
            try
            {
                if (this.ColabBL.RemoveCollaborate(collaborate))
                    return this.Ok(new { Status = true, Message = "collaboration removed successfully" });
                else
                    return this.BadRequest(new { Status = false, Message = "collaboration not removed" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
    }
}
