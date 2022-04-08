using AuthDemo.Entity;
using AuthDemo.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IJwtAuthenticaionRepository jwtManager;

        public EmployeeController(IJwtAuthenticaionRepository jwt)
        {
            this.jwtManager = jwt;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserCredential userCredential)
        {
            string token = jwtManager.Authenticate(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
