using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Shared.Interfaces {
    public interface IProject {
        Task<List<ProjectDto>> List();
        Task<List<ProjectDto>> List(Guid companyId);
        Task<ProjectDto> Get(Guid projectId);
        Task<ProjectDto> Get(String companyUri, String projectUri);
        Task<ProjectDto> Create(ProjectDto project);
        Task<ProjectDto> Update(Guid projectId, ProjectDto project);
        Task<Boolean> Delete(Guid projectId);
        Task<ProjectDto> ModifyUser(Guid projectId, UserDto user);
    }
}
