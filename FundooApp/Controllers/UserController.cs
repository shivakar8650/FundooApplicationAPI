

using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;


namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        IUserBL BL;
        public UserController(IUserBL BL)
        {
            this.BL = BL;
        }

        [HttpPost]
        public IActionResult UserRegistration(UserRegistration user)
        {
            try
            {
                RegisterResponse respoData = this.BL.Registration(user);
                if (respoData.EmailId == user.EmailId)
                {
                  
                    return this.Ok(new { Success = true, message = "User register succesfully", data = respoData});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Facing Problem in register" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }

     /*   [Authorize]*/
        [AllowAnonymous ]
   
        [HttpGet("GetAllUserDetails")]              //get all registered data
        public IActionResult GetAllUserDetails()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            try
            {
                var userDetailsList = this.BL.GetUserRegistrations();
                if (userDetailsList != null)
                {
                    return this.Ok(new { Success = true, userlist = userDetailsList });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No user records found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }

       
        [HttpPost("Login")]                             //post login Details
        public IActionResult GetLogin(UserLogin user1)
        {
            try
            {
                UserResponse result = this.BL.GetLogin(user1);
                if (result.EmailId == null)
                {
                    return BadRequest(new { Success = false, message = "Email or Password Not Found" });
                }
                return Ok(new { Success = true, message = "Login Successful", data = result });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email should not be null or empty");
            }

            try
            {
                if(this.BL.SendResetLink(email))
                {
                    return Ok(new { Success = true, message = "Reset password link send on Email Successfully"});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error in send Reset password link" });
                }
            }
            catch(Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }

        }
        [Authorize]
        [HttpPost]
        [Route("resetPassword")]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
           
            if (this.BL.ResetPassword(reset,email ))
            {
               return Ok(new { Success = true, message = "password Reset Successfully" });
            }
            else
            {
                return BadRequest(new { Success =false, message = "Password Reset denied!" });
            }

         
        }

            

    }
}
