using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please enter the user's first name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the user's last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
