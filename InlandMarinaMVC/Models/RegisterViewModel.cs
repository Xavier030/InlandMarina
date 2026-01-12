using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InlandMarinaMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain letters")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain letters")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone")]
        public string Phone
        {
            get => _phone;
            set => _phone = FormatPhoneNumber(value);
        }
        private string _phone;

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "City can only contain letters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-z0-9]+$", ErrorMessage = "Username can only contain lowercase letters and numbers")]
        [Display(Name = "Username")]
        public string Username
        {
            get => _username;
            set => _username = value?.ToLower();
        }
        private string _username;

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-z]+$", ErrorMessage = "Password can only contain lowercase letters")]
        [Display(Name = "Password")]
        public string Password
        {
            get => _password;
            set => _password = value?.ToLower();
        }
        private string _password;

        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return phone;

            var digitsOnly = Regex.Replace(phone, @"[^\d]", "");

            if (digitsOnly.Length == 10)
            {
                return $"({digitsOnly.Substring(0, 3)}) {digitsOnly.Substring(3, 3)}-{digitsOnly.Substring(6, 4)}";
            }

            return phone; 
        }
    }
}