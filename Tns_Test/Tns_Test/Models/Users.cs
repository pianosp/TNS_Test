using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tns_Test.Models
{
    [Table("user")]
    public class Users
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
    }
}
