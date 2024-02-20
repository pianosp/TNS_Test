using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tns_Test.Models
{
    [Table("department")]
	public class Departments
	{
        [Key,Required]
        public int Id { get; set; }
        public string Dname { get; set; }
        public string Location { get; set; }
        //[JsonIgnore]
        //public ICollection<Users> Users { get; set; }
    }
}

