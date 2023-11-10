namespace GymAppAPI.Models.Response
{
    public class MembershipStatusResponse
    {
        public DateTime PaymentDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? Active { get; set; }
    }
}
