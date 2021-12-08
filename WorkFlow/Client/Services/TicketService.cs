using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class TicketService : ITicket
    {
        private readonly HttpClient _http;

        public TicketService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TicketDto> CreateTicket(TicketDto ticket)
        {
            var response = await _http.PostAsJsonAsync("api/ticket/", ticket);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var newTicket = await response.Content.ReadFromJsonAsync<TicketDto>();
            if (newTicket == null) throw new ApplicationException("Error while creating ticket");
            
            return newTicket;
        }

        public async Task<TicketDto> UpdateTicket(Guid ticketId, TicketDto ticket)
        {
            var response = await _http.PutAsJsonAsync($"api/ticket/{ticketId}/", ticket);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var updatedTicket = await response.Content.ReadFromJsonAsync<TicketDto>();
            if (updatedTicket == null) throw new ApplicationException("Error while updating ticket");
            
            return updatedTicket;
        }

        public async Task<bool> DeleteTicket(Guid ticketId)
        {
            var response = await _http.DeleteAsync($"api/ticket/{ticketId}/");
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<TicketDto> GetTicket(Guid ticketId)
        {
            var ticket = await _http.GetFromJsonAsync<TicketDto>($"api/ticket/{ticketId}/");
            if (ticket == null) throw new ApplicationException("Error while creating ticket");
            return ticket;
        }

        public async Task<List<TicketDto>> List(User? user = null)
        {
            var tickets = await _http.GetFromJsonAsync<List<TicketDto>>("api/ticket/");
            return tickets ?? new List<TicketDto>();
        }

        public async Task<List<TicketDto>> ListTicketsByProject(Guid projectId)
        {
            var tickets = await _http.GetFromJsonAsync<List<TicketDto>>($"api/ticket/project/{projectId}");
            return tickets ?? new List<TicketDto>();
        }

        public async Task<List<TicketDto>> ListTicketsByUser(string userId)
        {
            var tickets = await _http.GetFromJsonAsync<List<TicketDto>>($"api/ticket/user/{userId}");
            return tickets ?? new List<TicketDto>();
        }
    }
}
