using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Microsoft.AspNetCore.Components.Inject]
        protected IUser UserModel { get; set; }

        public UserController(IUser userModel)
        {
            UserModel = userModel;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await UserModel.GetUsersByProject("sdfsd"));
        }
        
        // GET: api/<UserController>/Project/{projectUri}
        [HttpGet("project/{projectUri}")]
        public async Task<IActionResult> GetUsersByProject(String projectUri)
        {
            return Ok(await UserModel.GetUsersByProject(projectUri));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var user = await UserModel.GetUser(id);
            if (user == null) NotFound("Invalid User Id");
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            throw new NotImplementedException();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Ticket ticket)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
