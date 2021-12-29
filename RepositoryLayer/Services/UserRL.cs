using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {

        readonly UserContext context;

        public UserRL(UserContext context)
        {
            this.context = context;
        }

        public bool Registration(UserRegistration user)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Password = user.Password;
                newUser.EmailId = user.EmailId;
                newUser.Createdat = DateTime.Now;
                newUser.Modified = DateTime.Now;



                //adding user details to the database user table 
                this.context.UserTable.Add(newUser);

                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public UserResponse GetLogin(UserLogin User1)  //to check login and password
        {
            try
            {
                User ValidLogin = this.context.UserTable.Where(X => X.EmailId == User1.EmailId && X.Password == User1.Password).FirstOrDefault();
                if (ValidLogin.Id != 0 && ValidLogin.EmailId != null)
                {
                    UserResponse loginResponse = new UserResponse();
                    loginResponse.Id = ValidLogin.Id;
                    loginResponse.FirstName = ValidLogin.FirstName;
                    loginResponse.LastName = ValidLogin.LastName;
                    loginResponse.EmailId = ValidLogin.EmailId;
                    loginResponse.Createdat = ValidLogin.Createdat;
                    loginResponse.Modified = ValidLogin.Modified;
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
