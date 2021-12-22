using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Shared.Interfaces
{
    public interface IProject
    {
        Task<List<ProjectDto>> List();
        Task<ProjectDto> Get(Guid projectId);
        Task<ProjectDto> Create(ProjectDto project);
        Task<ProjectDto> Update(Guid projectId, ProjectDto project);
        Task<bool> Delete(Guid projectId);
        Task<ProjectDto> ModifyUser(Guid projectId, UserDto user);
    }
}