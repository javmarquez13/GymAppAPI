using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;

namespace GymAppAPI.Services
{
    public interface IMembershipService
    {
        public MembershipStatusResponse IsActiveById(int IdClient);

        public MembershipStatusResponse IsActiveByEmail(string EmailClient);
    }
}
