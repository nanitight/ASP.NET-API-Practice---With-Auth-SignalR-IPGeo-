using System.ComponentModel.DataAnnotations;

namespace RunGroupTUT.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
