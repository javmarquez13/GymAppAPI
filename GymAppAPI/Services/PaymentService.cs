using GymAppAPI.Models;
using GymAppAPI.Models.Request;
using System.Transactions;

namespace GymAppAPI.Services
{
    public class PaymentService : IPaymentService
    {
        public void Add()
        {
            using(var db = new GymAppDbContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
