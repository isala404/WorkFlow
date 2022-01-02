using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [Microsoft.AspNetCore.Components.Inject]
        protected IProject ProjectModel { get; set; }

        public ProjectController(IProject projectModel)
        {
            ProjectModel = projectModel;
        }

        // GET: api/project
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var companies = await ProjectModel.List();
                return Ok(companies);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, "Something went wrong")
                };
            }
        }
        
        // GET: api/project/company/{companyID}
        [HttpGet("company/{companyId:guid}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            try
            {
                var companies = await ProjectModel.List(companyId);
                return Ok(companies);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, "Something went wrong")
                };
            }
        }

        // GET: api/project/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var project = await ProjectModel.Get(id);
                return Ok(project);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, "Something went wrong")
                };
            }
        }

        // GET: api/project/{companyUri}/{projectUri}
        [HttpGet("{companyUri}/{projectUri}")]
        public async Task<IActionResult> Get(String companyUri, String projectUri)
        {
            try
            {
                var project = await ProjectModel.Get(companyUri, projectUri);
                return Ok(project);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, "Something went wrong")
                };
            }
        }
        
        // POST: api/project
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDto project)
        {
            try
            {
                var newProject = await ProjectModel.Create(project);
                return Ok(newProject);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, $"Something went wrong: {e.Message}")
                };
            }
        }

        // PUT: api/project/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProjectDto project)
        {
            try
            {
                var updatedProject = await ProjectModel.Update(id, project);
                return Ok(updatedProject);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, $"Something went wrong: {e.Message}")
                };
            }
        }

        // PATCH: api/project/5
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UserDto user)
        {
            try
            {
                var project = await ProjectModel.ModifyUser(id, user);
                return Ok(project);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, "Something went wrong")
                };
            }
        }

        // DELETE: api/project/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await ProjectModel.Delete(id);
                return Ok(deleted);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, "Something went wrong")
                };
            }
        }
    }
}