using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaPresidencia.Models
{
    public class Position
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MinSalary { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MaxSalary { get; set; }

        // Navigation property
        public List<Employee> Employees { get; set; } = new();
    }
}
