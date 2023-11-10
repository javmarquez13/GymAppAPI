using GymAppAPI.Models;
using GymAppAPI.Models.Response;

namespace GymAppAPI.Services
{
    public class MembershipService : IMembershipService
    {
        public MembershipStatusResponse IsActiveById(int IdClient)
        {
            MembershipStatusResponse response = new MembershipStatusResponse();

            using (var db = new GymAppDbContext())
            {             
                try
                {
                    var membershipStatus = db.MembershipStatuses.Where(d => d.IdClient == IdClient).FirstOrDefault();

                    response.PaymentDate = membershipStatus.PaymentDate;
                    response.ExpirationDate = membershipStatus.ExpirationDate;
                    response.Active = membershipStatus.Active;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return response;    
        }

        public MembershipStatusResponse IsActiveByEmail(string EmailClient)
        {
            MembershipStatusResponse response = new MembershipStatusResponse();

            using (var db = new GymAppDbContext())
            {
                try
                {
                    long idClient = db.Clients.Where(d => d.Email== EmailClient).FirstOrDefault().IdClient;
                    var membershipStatus = db.MembershipStatuses.Where(d => d.IdClient == idClient).FirstOrDefault();

                    response.PaymentDate = membershipStatus.PaymentDate;
                    response.ExpirationDate = membershipStatus.ExpirationDate;
                    response.Active = membershipStatus.Active;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return response;
        }
    }
}
