using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;


namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {

        IUserRL UserRL;
        public UserBL(IUserRL userRL)
        {
            this.UserRL = userRL;
        }
        public bool Registration(UserRegistration user)
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

        public UserResponse GetLogin(UserLogin User1)   //to post emailid and password-login part
        {
            try
            {
                return this.UserRL.GetLogin(User1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
