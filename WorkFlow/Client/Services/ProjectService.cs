using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class ProjectService: IProject
    {
        private readonly HttpClient _http;

        public ProjectService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<List<ProjectDto>> List()
        {
            var projects = await _http.GetFromJsonAsync<List<ProjectDto>>("api/project/");
            if (projects == null) throw new ApplicationException("Error while getting projects"); 
            return projects;
        }

        public async Task<List<ProjectDto>> List(Guid companyId)
        {
            var projects = await _http.GetFromJsonAsync<List<ProjectDto>>($"api/project/company/{companyId}/");
            if (projects == null) throw new ApplicationException("Error while getting projects"); 
            return projects;
        }

        public async Task<ProjectDto> Get(Guid projectId)
        {
            var project = await _http.GetFromJsonAsync<ProjectDto>($"api/project/{projectId}/");
            if (project == null) throw new ApplicationException("Error while getting project");
            return project;
        }
        
        public async Task<ProjectDto> Get(string uri)
        {
            var project = await _http.GetFromJsonAsync<ProjectDto>($"api/project/{uri}/");
            if (project == null) throw new ApplicationException("Error while getting project");
            return project;
        }


        public async Task<ProjectDto> Create(ProjectDto project)
        {
            var response = await _http.PostAsJsonAsync("api/project/", project);
            // TODO: Handle for constrain violations
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var newProject = await response.Content.ReadFromJsonAsync<ProjectDto>();
            if (newProject == null) throw new ApplicationException("Error while creating project");
            
            return newProject;
        }

        public async Task<ProjectDto> Update(Guid projectId, ProjectDto project)
        {
            var response = await _http.PutAsJsonAsync($"api/project/{projectId}/", project);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var updatedProject = await response.Content.ReadFromJsonAsync<ProjectDto>();
            if (updatedProject == null) throw new ApplicationException("Error while updating project");
            
            return updatedProject;
        }

        public async Task<bool> Delete(Guid projectId)
        {
            var response = await _http.DeleteAsync($"api/project/{projectId}/");
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<ProjectDto> ModifyUser(Guid projectId, UserDto user)
        {
            var response = await _http.PostAsJsonAsync($"api/project/user/{projectId}/", user);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var updatedProject = await response.Content.ReadFromJsonAsync<ProjectDto>();
            if (updatedProject == null) throw new ApplicationException("Error while modifying user from project");
            
            return updatedProject;
        }
    }
}