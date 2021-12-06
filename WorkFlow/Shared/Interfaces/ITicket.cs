using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Interfaces
{
    public interface ITicket
    {
        Task<Ticket> CreateTicket(Ticket ticket);
        Task<Ticket?> UpdateTicket(Guid ticketID, Ticket ticket);
        Task<bool> DeleteTicket(Guid ticketID);
        Task<Ticket?> GetTicket(Guid ticketID);
        Task<List<Ticket>> List(User user);
        Task<List<Ticket>> ListTicketsByProject(Guid projectID);
        Task<List<Ticket>> ListTicketsByUser(Guid userID);
    }
}
