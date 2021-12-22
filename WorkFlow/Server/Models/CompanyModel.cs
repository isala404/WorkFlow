using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models
{
    public class CompanyModel: ICompany
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utilityService;

        public CompanyModel(ApplicationDbContext context, IUtility utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        } 
        
        public async Task<List<CompanyDto>> List()
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            List<CompanyDto> companies = new();
            
            var userCompanies = await _context.UserCompany.Where(userCompany => userCompany.User == user).ToListAsync();
            companies.AddRange(userCompanies.Select(userCompany => new CompanyDto(userCompany.Company)));
            
            return companies;
        }
        
        public async Task<CompanyDto> Get(Guid companyId)
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            var userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.CompanyId == companyId && userCompany.UserId == user.Id);
            
            if(userCompany == null)  throw new InvalidDataException("Invalid CompanyId.");
            
            return new CompanyDto(userCompany.Company);
        }
        
        public async Task<CompanyDto> Create(CompanyDto company)
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            var newCompany = new Company
            {
                Name = company.Name,
                Uri = company.Uri
            };
            newCompany.Users.Add(new UserCompany
            {
                User = user,
                Role = UserRole.Admin
            });
            
            var result = await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return new CompanyDto(result.Entity);
        }
        
        public async Task<CompanyDto> Update(Guid companyId, CompanyDto company)
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            var userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.CompanyId == companyId && userCompany.UserId == user.Id);
            
            if(userCompany == null)  throw new InvalidDataException("Invalid CompanyId.");

            var targetCompany = userCompany.Company;
            targetCompany.Name = company.Name;
            targetCompany.Uri = company.Uri;
            
            await _context.SaveChangesAsync();
            return new CompanyDto(targetCompany);
        }

        public async Task<bool> Delete(Guid companyId)
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            var userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.CompanyId == companyId && userCompany.UserId == user.Id);
            
            if(userCompany == null)  throw new InvalidDataException("Invalid CompanyId.");
            
            _context.Companies.Remove(userCompany.Company);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}