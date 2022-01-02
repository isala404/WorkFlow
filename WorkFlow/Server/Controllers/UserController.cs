using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkFlow.Server.Controllers {
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        public UserController(IUser userModel) {
            UserModel = userModel;
        }

        [Inject]
        protected IUser UserModel { get; set; }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> Get() {
            try
            {
                UserDto user = await UserModel.Get();
                return Ok(user);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id) {
            UserDto user = await UserModel.Get(id);
            return Ok(user);
        }

        // GET: api/user/Project/{projectUri}
        [HttpGet("project/{projectUri}")]
        public async Task<IActionResult> GetUsersByProject(String projectUri) {
            return Ok(await UserModel.GetUsersByProject(projectUri));
        }

        // GET: api/user/Project/{projectUri}
        [HttpGet("company/{companyId:guid}")]
        public async Task<IActionResult> GetUsersByCompany(Guid companyId) {
            try
            {
                return Ok(await UserModel.GetUsersByCompany(companyId));
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/user
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDto userDto) {
            try
            {
                UserDto user = await UserModel.Update(userDto);
                return Ok(user);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/user
        [HttpDelete]
        public async Task<IActionResult> Delete() {
            try
            {
                Boolean deleted = await UserModel.Delete();
                return Ok(deleted);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/user/company
        [HttpPut("company")]
        public async Task<IActionResult> ModifyCompany([FromBody] UserInvite userInvite) {
            try
            {
                UserCompanyDto userCompany = await UserModel.SetUserCompany(userInvite);
                return Ok(userCompany);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/user/company
        [HttpPost("company")]
        public async Task<IActionResult> LeaveCompany([FromBody] Guid companyId) {
            try
            {
                CompanyDto company = await UserModel.LeaveCompany(companyId);
                return Ok(company);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/user/project
        [HttpPut("project")]
        public async Task<IActionResult> ModifyProject([FromBody] Tuple<Guid, String> userProject) {
            (Guid projectId, String userId) = userProject;
            try
            {
                UserDto userCompany = await UserModel.ModifyProject(projectId, userId);
                return Ok(userCompany);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
