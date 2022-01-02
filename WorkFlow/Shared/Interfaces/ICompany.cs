using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Shared.Interfaces {
    public interface ICompany {
        Task<List<CompanyDto>> List();
        Task<CompanyDto> Get(Guid companyId);
        Task<CompanyDto> Create(CompanyDto company);
        Task<CompanyDto> Update(Guid companyId, CompanyDto company);
        Task<Boolean> Delete(Guid companyId);
        Task<CompanyDto> ModifyUser(Guid companyId, UserCompanyDto user);
    }
}
