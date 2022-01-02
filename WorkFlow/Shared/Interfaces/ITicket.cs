using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Shared.Interfaces {
    public interface ITicket {
        Task<List<TicketDto>> List();
        Task<List<TicketDto>> ListTicketsByProject(Guid projectId);
        Task<List<TicketDto>> ListTicketsByUser(String userId);
        Task<TicketDto> Get(Guid ticketId);
        Task<TicketDto> Create(TicketDto ticket);
        Task<TicketDto> Update(Guid ticketId, TicketDto ticket);
        Task<Boolean> Delete(Guid ticketId);
    }
}
