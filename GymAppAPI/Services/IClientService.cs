using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;

namespace GymAppAPI.Services
{
    public interface IClientService
    {
        public object GetAll();

        public MembershipStatusResponse Add(ClientRequest oModel);

        public void Update(ClientRequest oModel);

        public void Delete(long Id);
    }
}
