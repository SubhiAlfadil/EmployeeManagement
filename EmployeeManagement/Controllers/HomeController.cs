using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public IHostingEnvironment HostingEnvironment { get; }

        public HomeController(IEmployeeRepository employeeRepository,
             IHostingEnvironment HostingEnvironment)
        {
            this.employeeRepository = employeeRepository;
            this.HostingEnvironment = HostingEnvironment;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var Employees = employeeRepository.GetAllEmployee();
            return View(Employees);
        }


        [AllowAnonymous]
        public IActionResult Details(int Id)
        {
            var Emp = employeeRepository.GetEmployee(Id);
            if (Emp == null)
            {
                Response.StatusCode = 404;
                // HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                return View("EmployeeNotFound",Id);
            }
            return View(Emp);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Employee employee = employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistPhotoPath = employee.PhotoPath,
            };

            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employeeEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Id = employeeEditViewModel.Id,
                    Name = employeeEditViewModel.Name,
                    Email = employeeEditViewModel.Email,
                    Department = employeeEditViewModel.Department,
                };
                if (employeeEditViewModel.Photo != null)
                {
                    // save the New Image and delete the old image 
                    string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                    string photoName = Guid.NewGuid().ToString() + "_" + employeeEditViewModel.Photo.FileName;
                    string FilePath = Path.Combine(uploadFolder, photoName);
                    // model.Photo.CopyTo(uploadFolder, photoName);
                    using (var fileStream=new FileStream(FilePath, FileMode.Create))
                    {
                        employeeEditViewModel.Photo.CopyTo(fileStream);
                    }
                    // employeeEditViewModel.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
                    string ExistPhotoPath = employeeEditViewModel.ExistPhotoPath;
                    string ExistPhotoPathForDelete = Path.Combine(HostingEnvironment.WebRootPath, 
                                          "images",ExistPhotoPath);
                    System.IO.File.Delete(ExistPhotoPathForDelete);
                    employee.PhotoPath = photoName;
                }
                else if (employeeEditViewModel.ExistPhotoPath != null)
                {
                    employee.PhotoPath = employeeEditViewModel.ExistPhotoPath;
                }
                else { employee.PhotoPath = ""; }
                Employee emp = employeeRepository.Update(employee);

                return RedirectToAction("Details", new { Id = emp.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormEmployee model)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (model.Photo != null)
                {
                    string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                    string photoName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string FilePath = Path.Combine(uploadFolder, photoName);
                    fileName = photoName;
                    // model.Photo.CopyTo(uploadFolder, photoName);
                    model.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
                }
                Employee employee = new Employee()
                {
                    Department = model.Department,
                    Name = model.Name,
                    Email = model.Email,
                    PhotoPath = fileName
                };
                employeeRepository.Add(employee);
                //int Id = employee.Id;
                //return RedirectToAction("Details", Id);
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var Model = employeeRepository.GetEmployee(Id);
            return View(Model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult ConfirmDelete(int Id, Employee Model)
        {
            Employee employee = employeeRepository.Delete(Id);
            if (employee == null)
            {
                return View("Not_deleted", Model);
            }
            return View("Deleted", employee);
        }
    }
}
