using System.ComponentModel.DataAnnotations;

namespace AuthSample.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = @"Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string InvalidEmailMessage => "Invalid Email";
        public string InvalidPasswordMessage => "Invalid Password";
    }
}