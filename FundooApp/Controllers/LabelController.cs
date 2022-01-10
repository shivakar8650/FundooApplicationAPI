﻿using BusinessLayer.Interfaces;
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
    public class LabelController : ControllerBase
    {

        private readonly ILabelBL LabelBL;
        public LabelController(ILabelBL LabelsBL)
        {
            this.LabelBL = LabelsBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(LabelClass labelInput)
        {
            try
            {
                long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);

                if (this.LabelBL.CreateLabel(labelInput, UserId))
                {
                    return Ok(new { Success = true, message = "Label created Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label already exists !!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("AddNote")]
        public IActionResult AddNoteToLabel(string LabelName,long noteId)
        {
            try
            {
                long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);
                if (this.LabelBL.AddNoteToExistingLabel(LabelName, noteId, UserId))
                {
                    return Ok(new { Success = true, message = $"Note added  Successfully to {LabelName} !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label doesn't exists !!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteLabel(string LabelName)
        {
            try
            {
            
                if (this.LabelBL.DeleteLabel(LabelName))
                {
                    return Ok(new { Success = true, message = $" {LabelName} delete successfully!!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "deletion failed!!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("note")]
        public IActionResult RemoveNoteFromLabel(string LabelName ,long NoteId)
        {
            try
            {

                if (this.LabelBL.RemoveNoteFromLabel(LabelName,NoteId))
                {
                    return Ok(new { Success = true, message = $"Note {NoteId} is removed from {LabelName} successfully!!"});
                }
                else
                {
                    return BadRequest(new { Success = false, message = "deletion failed!!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }
    }
}

