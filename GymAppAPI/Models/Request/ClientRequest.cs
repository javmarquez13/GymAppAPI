using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GymAppAPI.Models.Request
{
    public class ClientRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }

        public DateTime RegistrationDate { get; set; }
        public string MembershipType { get; set; }

    }


 
}
