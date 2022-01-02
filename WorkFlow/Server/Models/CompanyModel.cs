using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models {
    /// <summary>
    /// Handle all Company related database operations
    /// </summary>
    public class CompanyModel : ICompany {
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utilityService;

        public CompanyModel(ApplicationDbContext context, IUtility utilityService) {
            _context = context;
            _utilityService = utilityService;
        }

        /// <summary>
        /// Return list of companies related to the user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<List<CompanyDto>> List() {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            List<CompanyDto> companies = new List<CompanyDto>();

            List<UserCompany> userCompanies = await _context.UserCompany.Include(u => u.Company)
                .Where(userCompany => userCompany.User!.Id == user.Id).ToListAsync();
            companies.AddRange(userCompanies.Select(userCompany => new CompanyDto(userCompany.Company!)));

            return companies;
        }

        /// <summary>
        /// Get Company data by id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<CompanyDto> Get(Guid companyId) {
            (_, Company company) = await VerifyRequest(companyId, true, true);

            return new CompanyDto(company);
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<CompanyDto> Create(CompanyDto company) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            Company newCompany = new Company
            {
                Name = company.Name,
                Uri = company.Uri,
                Users = new List<UserCompany>
                {
                    new UserCompany
                    {
                        User = user,
                        Role = UserRole.Admin
                    }
                }
            };

            EntityEntry<Company> result = await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return new CompanyDto(result.Entity);
        }

        /// <summary>
        /// Update the company data
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<CompanyDto> Update(Guid companyId, CompanyDto company) {
            (_, Company targetCompany) = await VerifyRequest(companyId);

            targetCompany.Name = company.Name;
            targetCompany.Uri = company.Uri;

            await _context.SaveChangesAsync();
            return new CompanyDto(targetCompany);
        }

        /// <summary>
        /// Delete a company by ID
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<Boolean> Delete(Guid companyId) {
            (_, Company company) = await VerifyRequest(companyId);

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Add or Remove a user from company (Deprecated, use  UserModel.ModifyCompany())
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userCompanyDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        [Obsolete("Please use the UserModel.ModifyCompany()")]
        public async Task<CompanyDto> ModifyUser(Guid companyId, UserCompanyDto userCompanyDto) {
            (_, Company company) = await VerifyRequest(companyId);

            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userCompanyDto.UserId);
            if (user == null) throw new InvalidDataException("Invalid UserId.");

            UserCompany? userCompany = await _context.UserCompany.FirstOrDefaultAsync(uc =>
                uc.UserId == userCompanyDto.UserId && uc.CompanyId == companyId);

            if (userCompany != null)
                company.Users!.Remove(userCompany);
            else
                company.Users!.Add(new UserCompany
                    {UserId = userCompanyDto.UserId, CompanyId = companyId, Role = userCompanyDto.Role});

            await _context.SaveChangesAsync();

            return new CompanyDto(company);
        }

        /// <summary>
        /// Verify the incoming request is valid and has permission to do the required operation
        /// </summary>
        /// <param name="companyId">Company that's intended to be interacted with</param>
        /// <param name="admin">Admin only Method</param>
        /// <param name="includeUsers">Database query should include join to user table</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        /// <exception cref="UnauthorizedAccessException">Raised if the request is only allowed for admins (HTTP 401)</exception>
        private async Task<Tuple<UserCompany, Company>> VerifyRequest(Guid companyId, Boolean admin = true, Boolean includeUsers = false) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");
            Company? company;
            if (includeUsers)
                company = await _context.Companies.Include("Users.User").FirstOrDefaultAsync(c => c.Id == companyId);
            else
                company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
            if (company == null) throw new InvalidDataException("Invalid Company.");

            UserCompany? userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.Company == company && userCompany.User == user);

            if (userCompany == null) throw new UnauthorizedAccessException("User does not have required permission");

            if (admin && userCompany.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("User does not have required permission");

            return new Tuple<UserCompany, Company>(userCompany, company);
        }
    }
}
