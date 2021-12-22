using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [Microsoft.AspNetCore.Components.Inject]
        protected ICompany CompanyModel { get; set; }

        public CompanyController(ICompany companyModel)
        {
            CompanyModel = companyModel;
        }
        
        // GET: api/company
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var companies = await CompanyModel.List();
                return Ok(companies);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/company/5
        [HttpGet("{id:guid}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var company = await CompanyModel.Get(id);
                return Ok(company);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/company
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDto company)
        {
            try
            {
                var newCompany = await CompanyModel.Create(company);
                return Ok(newCompany);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/company/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CompanyDto company)
        {
            try
            {
                var updatedCompany = await CompanyModel.Update(id, company);
                return Ok(updatedCompany);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PATCH: api/company/5
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UserCompanyDto user)
        {
            try
            {
                var company = await CompanyModel.ModifyUser(id, user);
                return Ok(company);
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
        
        // DELETE: api/company/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await CompanyModel.Delete(id);
                return Ok(deleted);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
