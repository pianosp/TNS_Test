using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tns_Test.Models;

namespace Tns_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController: ControllerBase
	{
        private dbContext _context;
        public DepartmentsController(dbContext context) //create _coxtext for interact with db
        {
            _context = context;

        }

        // GET: api/<DepartmentsController>
        [HttpGet]
        public IActionResult getDept()
        {
            try {
                var departments = _context.Departments.Select(d => new { id = d.Id, dname = d.Dname, location = d.Location }).ToList();
                var response = new { department = departments };
                return Ok(response);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }


        }
        // GET api/<DepartmentsController>/5
        [HttpGet("{id:int}")]
        public IActionResult getDeptById(int id)
        {
            try
            {
                var department = _context.Departments.Find(id);
                if (department == null)
                {
                    return NotFound();
                }

                var response = new { department = new { id = department.Id, dname = department.Dname, location = department.Location } };

                return Ok(response);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public IActionResult createDept([FromBody] Departments model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Dname) || string.IsNullOrWhiteSpace(model.Location))
                {
                    // If model contains null or empty values, return a 400 Bad Request status code with an error message
                    return StatusCode(StatusCodes.Status422UnprocessableEntity);
                }

                var departmentExist = _context.Departments.Any(e => e.Dname == model.Dname);
                if (departmentExist == true)
                {
                    return Ok(new { Message = "Department Already Created" });

                }

                _context.Add(model);
                _context.SaveChanges();

                return Ok(new { Message = "Department Created" });
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        [HttpPut("{id}")]
        public IActionResult updateDept([FromBody] Departments model)
        {
            if (string.IsNullOrWhiteSpace(model.Dname) || string.IsNullOrWhiteSpace(model.Location))
            {
                // If model contains null or empty values, return a 400 Bad Request status code with an error message
                return StatusCode(StatusCodes.Status422UnprocessableEntity,
                    "E");
            }
            try
            {
                _context.Departments.Attach(model);
                _context.Entry(model).State = EntityState.Modified;


                // _context.Departments.Update(model);
                _context.SaveChanges();

                return Ok(new { Message = "Departments Updated" });
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult deleteDept(int id)
        {
            try {
                var department = _context.Departments.Find(id);
                if (department == null)
                {
                    return NotFound();
                }

                _context.Departments.Remove(department);
                _context.SaveChanges();

                return Ok(new { Message = "Departments Deleted" });
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}

