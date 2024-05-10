using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "You must enter an email address.")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
