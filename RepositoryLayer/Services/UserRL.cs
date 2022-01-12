using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly UserContext context;
        private readonly IConfiguration _config;
        public UserRL(UserContext context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterResponse Registration(UserRegistration user)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Password = encryptpass(user.Password);
                newUser.EmailId = user.EmailId;
                newUser.Createdat = DateTime.Now;
                newUser.Modified = DateTime.Now;
                //adding user details to the database user table 
                this.context.UserTable.Add(newUser);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    RegisterResponse response = new RegisterResponse();
                    response.FirstName = user.FirstName;
                    response.LastName = user.LastName;
                    response.EmailId = user.EmailId;
                    response.Createdat = DateTime.Now;

                    return response;
                }
                return default;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        /// <summary>
        /// Get the login info
        /// </summary>
        /// <param name="User1"></param>
        /// <returns></returns>
        public UserResponse GetLogin(UserLogin User1 )  //to check login and password
        {  
            try
            {
                User ValidLogin = this.context.UserTable.Where(X => X.EmailId == User1.EmailId).FirstOrDefault();
                if (Decryptpass(ValidLogin.Password) == User1.Password)
                {
                    string token = "";
                    UserResponse loginResponse = new UserResponse();
                    token = GenerateJWTToken(ValidLogin.EmailId, ValidLogin.Id);
                    loginResponse.Id = ValidLogin.Id;
                    loginResponse.FirstName = ValidLogin.FirstName;
                    loginResponse.LastName = ValidLogin.LastName;
                    loginResponse.EmailId = ValidLogin.EmailId;
                    loginResponse.Createdat = ValidLogin.Createdat;
                    loginResponse.Modified = ValidLogin.Modified;
                    loginResponse.Token = token;

                    return loginResponse;
                }
                   return null;   
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// get all user registration
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserRegistrations()
        {
            return context.UserTable.ToList();
        }
        /// <summary>
        /// Generate JWT token
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string EmailId, long UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
           new Claim(ClaimTypes.Email,EmailId),
           new Claim("UserId",UserId.ToString())
           };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], EmailId,
              claims,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Method to encrypt password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
        /// <summary>
        /// method to decrypt password.
        /// </summary>
        /// <param name="encryptpwd"></param>
        /// <returns></returns>
        private string Decryptpass(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        /// <summary>
        /// send reset link.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SendResetLink(string email)
        {
            User ValidLogin = this.context.UserTable.Where(X => X.EmailId == email).FirstOrDefault();
            if (ValidLogin.EmailId != null)
            {
                var token = GenerateJWTToken(ValidLogin.EmailId, ValidLogin.Id);
                new MsmqOperation().Sender(token);
                return true;
             }
             return false;
        }
        /// <summary>
        /// Reset the password link.
        /// </summary>
        /// <param name="reset"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ResetPassword(ResetPassword reset, string email)
        {
            var ValidLogin = this.context.UserTable.SingleOrDefault(x => x.EmailId == email);
            if (ValidLogin.EmailId != null)
            { context.UserTable.Attach(ValidLogin); 
                ValidLogin.Password = encryptpass(reset.ConfirmPassword); 
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }  
}
