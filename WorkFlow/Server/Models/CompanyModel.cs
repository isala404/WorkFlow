using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
            
            var userCompanies = await _context.UserCompany.Include(u => u.Company).Where(userCompany => userCompany.User.Id == user.Id).ToListAsync();
            companies.AddRange(userCompanies.Select(userCompany => new CompanyDto(userCompany.Company)));
            
            return companies;
        }
        
        public async Task<CompanyDto> Get(Guid companyId)
        {
            (_, Company company) = await VerifyRequest(companyId);
            
            return new CompanyDto(company);
        }
        
        public async Task<CompanyDto> Create(CompanyDto company)
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            var newCompany = new Company
            {
                Name = company.Name,
                Uri = company.Uri,
                Users = new List<UserCompany>
                {
                    new()
                    {
                        User = user,
                        Role = UserRole.Admin
                    }
                }
            };

            var result = await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return new CompanyDto(result.Entity);
        }
        
        public async Task<CompanyDto> Update(Guid companyId, CompanyDto company)
        {
            (_, Company targetCompany) = await VerifyRequest(companyId);
            
            targetCompany.Name = company.Name;
            targetCompany.Uri = company.Uri;
            
            await _context.SaveChangesAsync();
            return new CompanyDto(targetCompany);
        }

        public async Task<bool> Delete(Guid companyId)
        {
            (_, Company company) = await VerifyRequest(companyId);
            
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<CompanyDto> ModifyUser(Guid companyId, UserCompanyDto userCompanyDto)
        {
            (_, Company company) = await VerifyRequest(companyId);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userCompanyDto.UserId);
            if (user == null) throw new InvalidDataException("Invalid UserId.");

            var userCompany = await _context.UserCompany.FirstOrDefaultAsync(uc =>
                uc.UserId == userCompanyDto.UserId && uc.CompanyId == companyId);
            
            if (userCompany != null)
            {
                company.Users.Remove(userCompany);
            }
            else
            {
                company.Users.Add(new UserCompany{UserId = userCompanyDto.UserId, CompanyId = companyId, Role = userCompanyDto.Role});   
            }

            await _context.SaveChangesAsync();

            return new CompanyDto(company);
        }
        
        private async Task<Tuple<UserCompany, Company>> VerifyRequest(Guid companyId, bool admin = true)
        {
            var user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            var company = await _context.Companies.FirstOrDefaultAsync(company => company.Id == companyId);
            if (company == null) throw new InvalidDataException("Invalid Company.");

            var userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.Company == company && userCompany.User == user);

            if (userCompany == null) throw new UnauthorizedAccessException("User does not have required permission");
            
            if (admin && userCompany.Role != UserRole.Admin) throw new UnauthorizedAccessException("User does not have required permission");
            
            return new Tuple<UserCompany, Company>(userCompany, company);
        }
    }
}