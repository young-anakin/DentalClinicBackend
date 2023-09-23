using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public String RoleName { get; set; } = null!;

        //public UserAccount UserAccount { get; set; }

        public List<UserAccount> UserAccounts { get; set; }
    }
}
    