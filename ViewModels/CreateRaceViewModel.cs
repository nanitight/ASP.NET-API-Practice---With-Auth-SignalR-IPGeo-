using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Enum;
using WebApplication1.Models;

namespace RunGroupTUT.ViewModels
{
    public class CreateRaceViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        public Address Address { get; set; }

        public RaceCategory RaceCategory { get; set; }

    }

}
