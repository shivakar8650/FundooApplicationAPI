using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public RegisterResponse Registration(UserRegistration User);
        public UserResponse GetLogin(UserLogin User1);
        IEnumerable<User> GetUserRegistrations();
        bool SendResetLink(ForgetPassword email);
        bool ResetPassword(ResetPassword reset,string email);
    }
}
