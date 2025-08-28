using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaPresidencia.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int PositionId { get; set; }
        public Position? Position { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
