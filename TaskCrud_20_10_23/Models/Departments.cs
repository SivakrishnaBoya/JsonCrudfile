using System.ComponentModel.DataAnnotations;

namespace TaskCrud_20_10_23.Models
{
    public class Departments
    {
        [Key]
        [Required(ErrorMessage ="DepartmentId Is Required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage ="DepartmentName Is Required")]
        public string? DepartmentName { get; set; }
    }
}
