using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskCrud_20_10_23.Models
{
    public class Users
    {
        [Key]
        [Required(ErrorMessage ="UserId Is Required")]
        public int UserId { get; set; }
        [ForeignKey("Departments")]
        [Required(ErrorMessage ="DepartmentId Is Required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage ="Name Is Required")]
        public string? Name { get; set; }
        
        [JsonIgnore]
        public string? Password { get; set; }
    }
}
