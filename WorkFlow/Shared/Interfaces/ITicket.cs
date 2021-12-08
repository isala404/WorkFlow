using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Interfaces
{
    public interface ITicket
    {
        Task<TicketDto> CreateTicket(TicketDto ticket);
        Task<TicketDto> UpdateTicket(Guid ticketId, TicketDto ticket);
        Task<bool> DeleteTicket(Guid ticketId);
        Task<TicketDto> GetTicket(Guid ticketId);
        Task<List<TicketDto>> List(User? user = null);
        Task<List<TicketDto>> ListTicketsByProject(Guid projectId);
        Task<List<TicketDto>> ListTicketsByUser(String userId);
    }
}
