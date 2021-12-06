using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            var result = await _http.PostAsJsonAsync<Ticket>("api/ticket", ticket);
            return await result.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task<bool> DeleteTicket(Guid ticketID)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket?> GetTicket(Guid ticketID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> List(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> ListTicketsByProject(Guid projectID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> ListTicketsByUser(Guid userID)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket?> UpdateTicket(Guid ticketID, Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
