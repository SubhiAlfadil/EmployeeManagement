using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public List<Employee> employees { get; set; }
        public MockEmployeeRepository()
        {
            employees = new List<Employee>() {
                new Employee{ Id=1,Name="Ahmet 1" ,Email= "Ahmet1@gmail.com" ,Department=Dept.HR },
                new Employee{ Id=2,Name="Ahmet 2" ,Email= "Ahmet2@gmail.com" ,Department=Dept.IT },
                new Employee{ Id=3,Name="Ahmet 3" ,Email= "Ahmet3@gmail.com" ,Department=Dept.Other_Dept }
            };
        }

        public Employee GetEmployee(int Id)
        {
            Employee employee =  employees.SingleOrDefault( e => e.Id ==Id);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return employees;
        }

        public Employee Add(Employee employee)
        {
            employee.Id = employees.Max(e => e.Id) + 1;
            employees.Add(employee);
            return employee;
        }

        public Employee Update(Employee employeeChange)
        {
            Employee employee = employees.Find(e => e.Id == employeeChange.Id);
            if (employee != null)
            {
                employee.Name = employeeChange.Name;
                employee.Email= employeeChange.Email;
                employee.Department = employeeChange.Department;
            }
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = employees.Find(e => e.Id == Id);
            if (employee != null)
            {
                employees.Remove(employee);
            }
            return employee;
        }
    }
}
