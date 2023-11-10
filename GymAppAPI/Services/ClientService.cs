using GymAppAPI.Models;
using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;

namespace GymAppAPI.Services
{
    public class ClientService : IClientService
    {
        public object GetAll()
        {
            using (GymAppDbContext db = new GymAppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _list = db.Clients.OrderByDescending(d => d.IdClient).ToList();
                        return _list;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        public MembershipStatusResponse Add(ClientRequest oModel)
        {
            MembershipStatusResponse membershipStatusResponse = new MembershipStatusResponse();

            using (GymAppDbContext db = new GymAppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = db.Users.Where(d => d.Email == oModel.Email).FirstOrDefault();

                        if (user == null)
                            throw new Exception($"User Account for {oModel.Email} did not found registered in Database");



                        var clientValidation = db.Clients.Where(d => d.Email == oModel.Email).FirstOrDefault();

                        if (clientValidation != null)
                            throw new Exception($"Client {user.Email} already registered");


                        Client client = new Client();
                        client.IdUser = user.IdUser;
                        client.Email = user.Email;
                        client.Address = oModel.Address;
                        client.BirthDate = oModel.BirthDate;
                        client.LastName = oModel.LastName;
                        client.RegistrationDate = DateTime.Now;
                        client.MembershipType = oModel.MembershipType;
                        client.Name = oModel.Name;
                        client.Phone = oModel.Phone;

                        db.Clients.Add(client);
                        db.SaveChanges();

                        
                        MembershipStatus membershipStatus = new MembershipStatus();
                        membershipStatus.IdClient = client.IdClient;
                        membershipStatus.PaymentDate = DateTime.Now;
                        membershipStatus.Active = true;
                        if (client.MembershipType == "annual") membershipStatus.ExpirationDate = DateTime.Now.AddMonths(12);
                        if (client.MembershipType == "monthly") membershipStatus.ExpirationDate = DateTime.Now.AddMonths(1);
                        if (client.MembershipType == "weekly") membershipStatus.ExpirationDate = DateTime.Now.AddDays(7);

                        db.MembershipStatuses.Add(membershipStatus);
                        db.SaveChanges();              
                        


                        Payment payment = new Payment();
                        payment.IdClient = client.IdClient;
                        payment.PaymentDate = membershipStatus.PaymentDate;
                        payment.Amount = db.MembershipPrices.Where(d => d.MembershipType == oModel.MembershipType).FirstOrDefault().Price;
                        
                        db.Payments.Add(payment);
                        db.SaveChanges();

                        transaction.Commit();

                        membershipStatusResponse.PaymentDate = membershipStatus.PaymentDate;
                        membershipStatusResponse.ExpirationDate = membershipStatus.ExpirationDate;
                        membershipStatusResponse.Active = membershipStatus.Active;
                        return membershipStatusResponse;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                        return null;
                    }
                }
            }
        }


        public void Update(ClientRequest oModel)
        {
            using(var db = new GymAppDbContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        public void Delete(long Id)
        {
            using (var db = new GymAppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Client client = db.Clients.Find(Id);

                        if (client == null)
                            throw new Exception($"None client exist under following Id: {Id.ToString()}");

                        db.Remove(client);
                        db.SaveChanges();

                        transaction.Commit();
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
