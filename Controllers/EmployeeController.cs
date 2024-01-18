using ASPNETMVC_EFCORE_CRUD.Data;
using ASPNETMVC_EFCORE_CRUD.Models.Entities;
using ASPNETMVC_EFCORE_CRUD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVC_EFCORE_CRUD.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly MvcDemoDbContext _demoDbContext;

        public EmployeeController(MvcDemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _demoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Salary = model.Salary,
                Department = model.Department,
                DateofBirth = model.DateofBirth
            };
            await _demoDbContext.Employees.AddAsync(employee);
            await _demoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task< IActionResult> View(Guid id)
        {
           var employee= await _demoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateofBirth = employee.DateofBirth
                };
                return await Task.Run(()=> View("View",viewModel));
            }
            return RedirectToAction("Index");
          
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _demoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateofBirth = model.DateofBirth;
                employee.Department = model.Department;
                
               await _demoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete (UpdateEmployeeViewModel model)
        {
            var employee = await _demoDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                _demoDbContext.Employees.Remove(employee);
                await _demoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }


}
