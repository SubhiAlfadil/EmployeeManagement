using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ViewModel;

namespace EmployeeManagement.Models
{
    public class EmpDBContext :IdentityDbContext<ApplicationUser>
    {
        public EmpDBContext(DbContextOptions<EmpDBContext> option ):base (option)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        }
}
