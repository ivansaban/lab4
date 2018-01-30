using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DALlab4.Entities;

namespace Lab4.ApiContollers
{
    [Produces("application/json")]
    //[ApiVersion("1.0")]
    //swagger ne radi ako se apiVersion ne zakomentira
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public EmployeesController(AdventureWorks2014Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee.Where(e => e.Gender == "M");
        }
    }

    [Produces("application/json")]
    //[ApiVersion("2.0")]
    //swagger ne radi ako se apiVersion ne zakomentira
    [Route("api/Employees")]
    public class EmployeesV2_Controller : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public EmployeesV2_Controller(AdventureWorks2014Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee.Where(e => e.Gender == "F");
        }
    }
}