using WebApplication1.Models;

namespace RunGroupTUT.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<Club> Races { get; set; }

        public string City { get; set; }
        public string State { get; set; }
    }
}
