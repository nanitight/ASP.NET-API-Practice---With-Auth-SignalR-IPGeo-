using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class AppUser  : IdentityUser
    {
        //[Key]
        //public string Id { get; set; }
        public int? Pace{ get; set; }
        public int? Milage { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Race> Races { get; set; }

        public ICollection<Club> Clubs { get; set; }
    }

}
