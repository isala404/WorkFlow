using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Interfaces
{
    public interface ITicket
    {
        Task<List<TicketDto>> List(User? user = null);
        Task<List<TicketDto>> ListTicketsByProject(Guid projectId);
        Task<List<TicketDto>> ListTicketsByUser(String userId);
        Task<TicketDto> Get(Guid ticketId);
        Task<TicketDto> Create(TicketDto ticket);
        Task<TicketDto> Update(Guid ticketId, TicketDto ticket);
        Task<bool> Delete(Guid ticketId);
    }
}
