using PruebaTecnicaPresidencia.Models;

namespace PruebaTecnicaPresidencia.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            if (!context.Departments.Any())
            {
                var departments = new List<Department>
                {
                    new Department { Name = "Engineering", Manager = "John Smith", Budget = 1000000 },
                    new Department { Name = "Sales", Manager = "Mary Johnson", Budget = 750000 },
                    new Department { Name = "Marketing", Manager = "Robert Brown", Budget = 500000 },
                    new Department { Name = "Human Resources", Manager = "Sarah Davis", Budget = 300000 },
                    new Department { Name = "Finance", Manager = "Michael Wilson", Budget = 600000 },
                    new Department { Name = "IT Support", Manager = "David Miller", Budget = 400000 },
                    new Department { Name = "Research", Manager = "Lisa Anderson", Budget = 800000 },
                    new Department { Name = "Customer Service", Manager = "James Taylor", Budget = 350000 },
                    new Department { Name = "Operations", Manager = "Patricia White", Budget = 700000 },
                    new Department { Name = "Legal", Manager = "Thomas Moore", Budget = 450000 }
                };

                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();

                var positions = new List<Position>
                {
                    new Position { Name = "Manager", Description = "Oversees team operations and projects", MinSalary = 80000, MaxSalary = 120000 },
                    new Position { Name = "Senior Developer", Description = "Leads development efforts and mentors junior developers", MinSalary = 90000, MaxSalary = 150000 },
                    new Position { Name = "Junior Developer", Description = "Assists in software development and maintenance", MinSalary = 50000, MaxSalary = 80000 },
                    new Position { Name = "Analyst", Description = "Analyzes business processes and requirements", MinSalary = 60000, MaxSalary = 90000 },
                    new Position { Name = "Specialist", Description = "Provides specialized expertise in specific areas", MinSalary = 65000, MaxSalary = 95000 },
                    new Position { Name = "Coordinator", Description = "Coordinates team activities and projects", MinSalary = 45000, MaxSalary = 75000 },
                    new Position { Name = "Assistant", Description = "Provides support to team members", MinSalary = 35000, MaxSalary = 55000 },
                    new Position { Name = "Lead", Description = "Leads team initiatives and projects", MinSalary = 85000, MaxSalary = 130000 },
                    new Position { Name = "Director", Description = "Directs department strategy and operations", MinSalary = 100000, MaxSalary = 180000 },
                    new Position { Name = "Consultant", Description = "Provides expert advice and solutions", MinSalary = 70000, MaxSalary = 140000 }
                };

                await context.Positions.AddRangeAsync(positions);
                await context.SaveChangesAsync();

                var random = new Random();
                var employees = new List<Employee>();

                for (int i = 1; i <= 1000; i++)
                {
                    var department = departments[random.Next(departments.Count)];
                    var position = positions[random.Next(positions.Count)];
                    var salary = random.Next((int)position.MinSalary, (int)position.MaxSalary);
                    var hireDate = DateTime.Now.AddDays(-random.Next(1, 3650)); // Up to 10 years ago

                    employees.Add(new Employee
                    {
                        Name = $"Employee {i}",
                        Email = $"employee{i}@presidencia.go.cr",
                        PositionId = position.Id,
                        Salary = salary,
                        HireDate = hireDate,
                        DepartmentId = department.Id
                    });
                }

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}
