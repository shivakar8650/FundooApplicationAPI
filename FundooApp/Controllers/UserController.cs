

using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
                  //  RegisterResponse respoData = new RegisterResponse();
                  /*  foreach(RegisterResponse in respoData)
                    {
                        
                    }*/
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

        [Authorize]
        [AllowAnonymous]
        [HttpGet("GetAllUserDetails")]              //get all registered data
        public IActionResult GetAllUserDetails()
        {
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
                    return Ok(new { Success = true, message = "Error in send Reset password link" });
                }
            }
            catch(Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }

        }

       /* [HttpPost]
        [AllowAnonymous]
        [Route("SendPasswordResetCode")]
        public async Task<IActionResult> SendPasswordResetCode(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email should not be null or empty");
            }

            // Get Identity User details user user manager
            var user = await userManager.FindByNameAsync(email);

            // Generate password reset token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Generate OTP
            int otp = RandomNumberGeneartor.Generate(100000, 999999);

            var resetPassword = new ResetPassword()
            {
                Email = email,
                OTP = otp.ToString(),
                Token = token,
                UserId = user.Id,
                InsertDateTimeUTC = DateTime.UtcNow
            };

            // Save data into db with OTP
            await databaseContext.AddAsync(resetPassword);
            await databaseContext.SaveChangesAsync();

            // to do: Send token in email
            await EmailSender.SendEmailAsync(email, "Reset Password OTP", "Hello "
                + email + "<br><br>Please find the reset password token below<br><br><b>"
                + otp + "<b><br><br>Thanks<br>oktests.com");

            return Ok("Token sent successfully in email");
        }*/

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
      /*  [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string otp, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Email & New Password should not be null or empty");
            }

            // Get Identity User details user user manager
            var user = await userManager.FindByNameAsync(email);

            // getting token from otp
            var resetPasswordDetails = await databaseContext.ResetPasswords
                .Where(rp => rp.OTP == otp && rp.UserId == user.Id)
                .OrderByDescending(rp => rp.InsertDateTimeUTC)
                .FirstOrDefaultAsync();

            // Verify if token is older than 15 minutes
            var expirationDateTimeUtc = resetPasswordDetails.InsertDateTimeUTC.AddMinutes(15);

            if (expirationDateTimeUtc < DateTime.UtcNow)
            {
                return BadRequest("OTP is expired, please generate the new OTP");
            }

            var res = await userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, newPassword);

            if (!res.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }*/
    }
}
