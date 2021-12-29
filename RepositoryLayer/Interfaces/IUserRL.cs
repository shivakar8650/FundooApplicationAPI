using CommonLayer.Model;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public bool Registration(UserRegistration User);

        public UserResponse GetLogin(UserLogin User1);
    }
}
