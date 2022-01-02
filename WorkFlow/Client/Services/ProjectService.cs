using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services {
    public class ProjectService : IProject {
        private const String EntityName = "project";
        private readonly HttpClient _http;

        public ProjectService(HttpClient http) {
            _http = http;
        }

        /// <summary>
        /// Get Current user's projects
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<List<ProjectDto>> List() {
            List<ProjectDto>? projects = await _http.GetFromJsonAsync<List<ProjectDto>>("api/project/");
            if (projects == null) throw new ApplicationException("Error while getting projects");
            return projects;
        }

        /// <summary>
        /// Get projects of the given company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<List<ProjectDto>> List(Guid companyId) {
            List<ProjectDto>? projects = await _http.GetFromJsonAsync<List<ProjectDto>>($"api/project/company/{companyId}/");
            if (projects == null) throw new ApplicationException("Error while getting projects");
            return projects;
        }

        /// <summary>
        /// Get Project by ID
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<ProjectDto> Get(Guid projectId) {
            ProjectDto? project = await _http.GetFromJsonAsync<ProjectDto>($"api/project/{projectId}/");
            if (project == null) throw new ApplicationException($"Error while getting {EntityName}");
            return project;
        }

        /// <summary>
        /// Get Project by it's URI
        /// </summary>
        /// <param name="companyUri"></param>
        /// <param name="projectUri"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<ProjectDto> Get(String companyUri, String projectUri) {
            ProjectDto? project = await _http.GetFromJsonAsync<ProjectDto>($"api/project/{companyUri}/{projectUri}/");
            if (project == null) throw new ApplicationException($"Error while getting {EntityName}");
            return project;
        }

        /// <summary>
        /// Create a new Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<ProjectDto> Create(ProjectDto project) {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/project/", project);
            // TODO: Handle for constrain violations
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while creating {EntityName}, Reason: {response.ReasonPhrase}");

            ProjectDto? newProject = await response.Content.ReadFromJsonAsync<ProjectDto>();
            if (newProject == null) throw new ApplicationException($"Could not retrieved the created ${EntityName}");

            return newProject;
        }

        /// <summary>
        /// Update a existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<ProjectDto> Update(Guid projectId, ProjectDto project) {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/project/{projectId}/", project);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating ${EntityName}, Reason: {response.ReasonPhrase}");

            ProjectDto? updatedProject = await response.Content.ReadFromJsonAsync<ProjectDto>();
            if (updatedProject == null) throw new ApplicationException($"Error while updating ${EntityName}");

            return updatedProject;
        }

        /// <summary>
        /// Delete a project from database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<Boolean> Delete(Guid projectId) {
            HttpResponseMessage response = await _http.DeleteAsync($"api/project/{projectId}/");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while deleting ${EntityName}, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<Boolean>();
        }

        /// <summary>
        /// Add or Remove a user from the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<ProjectDto> ModifyUser(Guid projectId, UserDto user) {
            HttpResponseMessage response = await _http.PostAsJsonAsync($"api/project/user/{projectId}/", user);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(
                $"Error while modifying ${EntityName} users, Reason: {response.ReasonPhrase}");

            ProjectDto? updatedProject = await response.Content.ReadFromJsonAsync<ProjectDto>();
            if (updatedProject == null) throw new ApplicationException($"Error while modifying ${EntityName} users");

            return updatedProject;
        }
    }
}
