using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services {
    public class TicketService : ITicket {
        private const String EntityName = "ticket";
        private readonly HttpClient _http;

        public TicketService(HttpClient http) {
            _http = http;
        }

        /// <summary>
        /// Get current User's Tickets
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<List<TicketDto>> List() {
            List<TicketDto>? tickets = await _http.GetFromJsonAsync<List<TicketDto>>("api/ticket/");
            if (tickets == null) throw new ApplicationException("Error while getting tickets");
            return tickets;
        }

        /// <summary>
        /// Get tickets for the given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<TicketDto>> ListTicketsByProject(Guid projectId) {
            List<TicketDto>? tickets = await _http.GetFromJsonAsync<List<TicketDto>>($"api/ticket/project/{projectId}");
            if (tickets == null) throw new ApplicationException("Error while getting tickets");
            return tickets;
        }

        /// <summary>
        /// Get Tickets for a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<List<TicketDto>> ListTicketsByUser(String userId) {
            List<TicketDto>? tickets = await _http.GetFromJsonAsync<List<TicketDto>>($"api/ticket/user/{userId}");
            if (tickets == null) throw new ApplicationException("Error while getting tickets");
            return tickets;
        }

        /// <summary>
        /// Get ticket data by id
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<TicketDto> Get(Guid ticketId) {
            TicketDto? ticket = await _http.GetFromJsonAsync<TicketDto>($"api/ticket/{ticketId}/");
            if (ticket == null) throw new ApplicationException($"Error while getting {EntityName}");
            return ticket;
        }

        /// <summary>
        /// Create new ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<TicketDto> Create(TicketDto ticket) {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/ticket/", ticket);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while creating {EntityName}, Reason: {response.ReasonPhrase}");

            TicketDto? newTicket = await response.Content.ReadFromJsonAsync<TicketDto>();
            if (newTicket == null) throw new ApplicationException($"Could not retrieved the created ${EntityName}");

            return newTicket;
        }

        /// <summary>
        /// Update an existing ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<TicketDto> Update(Guid ticketId, TicketDto ticket) {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/ticket/{ticketId}/", ticket);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating ${EntityName}, Reason: {response.ReasonPhrase}");

            TicketDto? updatedTicket = await response.Content.ReadFromJsonAsync<TicketDto>();
            if (updatedTicket == null) throw new ApplicationException($"Error while updating ${EntityName}");

            return updatedTicket;
        }

        /// <summary>
        /// Delete a project from the database
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<Boolean> Delete(Guid ticketId) {
            HttpResponseMessage response = await _http.DeleteAsync($"api/ticket/{ticketId}/");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while deleting ${EntityName}, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<Boolean>();
        }
    }
}
