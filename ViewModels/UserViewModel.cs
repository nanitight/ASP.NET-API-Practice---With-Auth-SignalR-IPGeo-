using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroupTUT.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ProfileImageUrl { get; set; }
        public int? Pace { get; set; }
        public int? Milage { get; set; }
        //[ForeignKey("Address")]
        //public int? AddressId { get; set; }
    }
}
