using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public RegisterResponse Registration(UserRegistration user);
        IEnumerable<User> GetUserRegistrations();
        public UserResponse GetLogin(UserLogin User1);
        bool SendResetLink(string email);
        bool ResetPassword(ResetPassword reset,string email);
    }
}

