using AngularTutorial_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularTutorial_API.DTOs
{
    public class BranchDto
    {
        public int BranchID { get; set; }

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Branch Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string BranchName { get; set; }

        [Display(Name = "Phone")]
        [StringLength(14, ErrorMessage = "Give a valid Phone number")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Give a valid Email number")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }
        public int BankID { get; set; }
        public BankDto? BankDto { get; set; }
    }
}
