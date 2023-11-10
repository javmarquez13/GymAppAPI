using GymAppAPI.Models.Request;

namespace GymAppAPI.Services
{
    public interface IEmailService
    {
        public void SendEmail(EmailRequest oModel);

    }
}
