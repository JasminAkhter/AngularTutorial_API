using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularTutorial_API.Models
{
    public class Bank
    {
        [Key, DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? BankAddress { get; set; }
    }
}
