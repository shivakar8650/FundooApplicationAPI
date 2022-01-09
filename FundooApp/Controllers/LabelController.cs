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
                    return Ok(new { Success = true, message = "Label added Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label already exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        /*[HttpPost]
        [Route("{noteId}/{labelName}")]
        public IActionResult AddNoteToExistingLabel(long noteId, string labelName)
        {
            try
            {
                LabelModel labelModel = new LabelModel();
                long userId = GetTokenId();
                bool result = _labelBL.AddNoteToExistingLabel(noteId, userId, labelName);

                if (result == true)
                {
                    return Ok(new { Success = true, message = $"Note added to {labelName} Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label doesn't exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpPost]
        public IActionResult AddLabelToUser(LabelModel labelModel)
        {
            try
            {
                long userId = GetTokenId();
                bool result = _labelBL.AddLabelToUser(userId, labelModel);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label added Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label already exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpGet]
        [Route("{noteId}")]
        public IActionResult GetNoteLables(long noteId)
        {
            try
            {
                long userId = GetTokenId();
                var labelList = _labelBL.GetNoteLables(noteId, userId);

                if (labelList.Count != 0)
                {
                    return this.Ok(new { Success = true, message = "These are Your all the Labels.", Data = labelList });
                }
                else if (labelList.Count == 0)
                {
                    return BadRequest(new { Success = false, message = "No Label is added to this note." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpGet]
        public IActionResult GetUserLabels()
        {
            try
            {
                long userId = GetTokenId();
                List<Label> labelList = _labelBL.GetUserLabels(userId);

                if (labelList.Count != 0)
                {
                    return this.Ok(new { Success = true, message = "These are Your all the Labels.", Data = labelList });
                }
                else if (labelList.Count == 0)
                {
                    return BadRequest(new { Success = false, message = "No Label is added." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpPut]
        [Route("{labelId}")]
        public IActionResult EditLabelName(long labelId, LabelModel labelModel)
        {
            try
            {
                long userId = GetTokenId();
                bool result = _labelBL.EditLabelName(labelId, userId, labelModel);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label changed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label does not exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpDelete]
        [Route("{labelId}/{noteId}")]
        public IActionResult RemoveLabelFromNote(long labelId, long noteId)
        {
            try
            {
                long userId = GetTokenId();
                bool result = _labelBL.RemoveLabel(labelId, noteId, userId);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label Removed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Failed to Remove label !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpDelete]
        public IActionResult DeleteLabel(LabelModel labelModel)
        {
            try
            {
                long userId = GetTokenId();
                bool result = _labelBL.DeleteLabel(userId, labelModel.labelName);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label Removed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Failed to Remove label !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }*/




    } }

