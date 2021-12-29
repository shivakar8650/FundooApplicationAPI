using CommonLayer.Model;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public RegisterResponse Registration(UserRegistration User);

        public UserResponse GetLogin(UserLogin User1);
    }
}
