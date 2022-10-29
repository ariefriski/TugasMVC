using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        
        public int Id { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("RoleId")]

        public int RoleId { get; set; }

        public virtual Role Roles{ get; set; }

        public virtual Employee Employees{ get; set; }
    }
}
