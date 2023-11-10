using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;

namespace GymAppAPI.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest oModel);

        void CreateAccount(UserRequest oModel);
    }
}
