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

    }
}
