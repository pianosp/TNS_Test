using System;
using Microsoft.AspNetCore.Authorization;
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

                var department = _context.Departments;
                return Ok(department);

            }catch (Exception)
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
                return Ok(department);
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
                if (model == null)
                {
                    return NotFound();
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

