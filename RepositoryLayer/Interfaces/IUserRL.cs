using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public RegisterResponse Registration(UserRegistration User);

        public UserResponse GetLogin(UserLogin User1);

        IEnumerable<User> GetUserRegistrations();  //to get all registered data
        bool SendResetLink(string email);
    }
}
