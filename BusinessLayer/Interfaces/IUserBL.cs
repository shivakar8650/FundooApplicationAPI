using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public RegisterResponse Registration(UserRegistration user);

        public UserResponse GetLogin(UserLogin User1);
    }
}

