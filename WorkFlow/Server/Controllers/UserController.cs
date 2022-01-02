﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlow.Shared.Dto;
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

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await UserModel.Get();
                return Ok(user);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var user = await UserModel.Get(id);
            return Ok(user);
        }

        // GET: api/user/Project/{projectUri}
        [HttpGet("project/{projectUri}")]
        public async Task<IActionResult> GetUsersByProject(String projectUri)
        {
            return Ok(await UserModel.GetUsersByProject(projectUri));
        }
        
        // GET: api/user/Project/{projectUri}
        [HttpGet("company/{companyId:guid}")]
        public async Task<IActionResult> GetUsersByCompany(Guid companyId)
        {
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
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            try
            {
                var user = await UserModel.Update(userDto);
                return Ok(user);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/user
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var deleted = await UserModel.Delete();
                return Ok(deleted);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/user/company
        [HttpPut("company")]
        public async Task<IActionResult> ModifyCompany([FromBody] UserInvite userInvite)
        {
            try
            {
                var userCompany = await UserModel.SetUserCompany(userInvite);
                return Ok(userCompany);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/user/company
        [HttpPost("company")]
        public async Task<IActionResult> LeaveCompany([FromBody] Guid companyId)
        {
            try
            {
                var company = await UserModel.LeaveCompany(companyId);
                return Ok(company);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/user/project
        [HttpPut("project")]
        public async Task<IActionResult> ModifyProject([FromBody] Tuple<Guid, string> userProject)
        {
            (Guid projectId, string userId) = userProject;
            try
            {
                var userCompany = await UserModel.ModifyProject(projectId, userId);
                return Ok(userCompany);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}