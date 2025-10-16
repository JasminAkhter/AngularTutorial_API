using System.ComponentModel.DataAnnotations;

namespace AngularTutorial_API.DTOs
{
    public class BankDto
    {
        public int BankID { get; set; }

        [Display(Name = "Bank Name")]
        [Required(ErrorMessage = "Bank Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string BankName { get; set; }

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Branch Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string BranchName { get; set; }

        [Display(Name = "Account Number")]
        [Required(ErrorMessage = "Account Number is required.")]
        [StringLength(50, ErrorMessage = "Account Number cannot be longer than 50 characters.")]
        public string AccountNumber { get; set; }

        [Display(Name = "Account Type")] 
        [StringLength(50, ErrorMessage = "AccountType cannot exceed 50 characters.")]
        public string? AccountType { get; set; }

        [Display(Name = "Bank Address")]
        [StringLength(250, ErrorMessage = "Bank Address cannot exceed 250 characters.")]
        public string? BankAddress { get; set; }
    }
}
