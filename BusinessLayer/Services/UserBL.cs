using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {

        IUserRL UserRL;
        public UserBL(IUserRL userRL)
        {
            this.UserRL = userRL;
        }
        public RegisterResponse Registration(UserRegistration user)
        {
           
            try
            {
                return this.UserRL.Registration(user);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IEnumerable<User> GetUserRegistrations()       //to get all registered data
        {
            return this.UserRL.GetUserRegistrations();
        }
        public UserResponse GetLogin(UserLogin User1)   //to post emailid and password-login part
        {
            try
            {
                return this.UserRL.GetLogin(User1);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
}
