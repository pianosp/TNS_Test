using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tns_Test.Models;

namespace Tns_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private dbContext _context;
        public UsersController(dbContext context) //create _coxtext for interact with db
        {
            _context = context;

        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult getAllUsers()
        {
            try {
                var users = _context.Users;
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // GET api/<UsersController>/5
        [HttpGet("{id:int}")]
        public IActionResult getUserById(int id)
        {
            try
            {
                var users = _context.Users.Find(id);
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult createUser([FromBody] Users model)
        {
            try {
                var userExist = _context.Users.Any(e => e.Id == model.Id);
                if (userExist)
                {
                    return Ok(new { Message = "User Already Created" });
                }

                var deptExist = _context.Departments.Any(d => d.Id == model.DepartmentId);
                if (!deptExist)
                {
                    return Ok(new { Message = "Department does not exist" });
                }

                _context.Users.Add(model);
                _context.SaveChanges();

                return Ok(new { Message = "User Created" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }


        [HttpPut("{id}")]
        public IActionResult updateUser([FromBody] Users model)
        {
            try {
                var deptExist = _context.Departments.Any(d => d.Id == model.DepartmentId);
                if (!deptExist)
                {
                    return Ok(new { Message = "Department does not exist" });
                }

                _context.Users.Attach(model);
                _context.Entry(model).State = EntityState.Modified;


                // _context.Users.Update(model);
                _context.SaveChanges();

                return Ok(new { Message = "Users Updated" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult deleteUser(int id)
        {
            try {
                var Users = _context.Users.Find(id);
                if (Users == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(Users);
                _context.SaveChanges();

                return Ok(new { Message = "Users Deleted" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}

