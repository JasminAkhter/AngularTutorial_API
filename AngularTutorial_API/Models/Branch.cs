using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularTutorial_API.Models
{
    public class Branch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchID { get; set; }

        
        public string BranchName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [ForeignKey("Bank")]
        public int BankID { get; set; }
        public Bank Bank { get; set; }
    }
}
