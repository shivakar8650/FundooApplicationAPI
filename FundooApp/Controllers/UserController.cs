

using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Enitity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  /*  [EnableCors("AllowOrigin")]*/
    public class UserController : ControllerBase
    {
        IUserBL BL;
        private readonly IMemoryCache memoryCache;
        private readonly UserContext context;
        private readonly IDistributedCache distributedCache;
       
        public UserController(IUserBL BL, IMemoryCache memoryCache, UserContext context, IDistributedCache distributedCache)
        {
            this.BL = BL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
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
        [AllowAnonymous ]
        [HttpGet("AllUser")]    
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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPost("Login")]                        
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
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(ForgetPassword email)
        {
            if (string.IsNullOrEmpty(email.EmailId))
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
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("resetPassword")]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
            try
            {
                if (this.BL.ResetPassword(reset, email))
                {
                    return Ok(new { Success = true, message = "password Reset Successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Password Reset denied!" });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllUserUsingRedisCache()
        {
         //   long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var CacheKey = "UserDetailsList";
            string serializedUserList;
            IEnumerable<User> UserDetailsList = new List<User>();
            var redisUserList = await distributedCache.GetAsync(CacheKey);
            if (redisUserList != null)
            {
                serializedUserList = Encoding.UTF8.GetString(redisUserList);
                UserDetailsList = JsonConvert.DeserializeObject<List<User>>(serializedUserList);
            }
            else
            {
                UserDetailsList = (IEnumerable<User>)this.BL.GetUserRegistrations();
                serializedUserList = JsonConvert.SerializeObject(UserDetailsList);
                redisUserList = Encoding.UTF8.GetBytes(serializedUserList);
                var options = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                 .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(CacheKey, redisUserList, options);
            }
            return Ok(UserDetailsList);
        }
    }
}
