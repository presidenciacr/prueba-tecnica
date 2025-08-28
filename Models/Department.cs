using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaPresidencia.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Manager { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; }

        public List<Employee> Employees { get; set; } = new();
    }
}
