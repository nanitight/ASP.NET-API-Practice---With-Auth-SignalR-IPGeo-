using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class AppUser  : IdentityUser
    {
        //[Key]
        public string Id { get; set; }
        public int? Pace{ get; set; }
        public int? Milage { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        /*
         * To fix null issue, database had to be migrated.
         * Using Package Manager Console.
         * `Add-Migration Name
         * `Update-Database
         
         */
        public Address? Address { get; set; }

		public string? ProfileImageUrl { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public ICollection<Race> Races { get; set; }

        public ICollection<Club> Clubs { get; set; }
    }

}
