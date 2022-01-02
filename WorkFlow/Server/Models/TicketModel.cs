using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkFlow.Shared.Interfaces;
using WorkFlow.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Server.Models
{
    public class TicketModel : ITicket
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utilityService;
        public TicketModel(ApplicationDbContext context, IUtility utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        public async Task<List<TicketDto>> List()
        {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");
            
            List<TicketDto> tickets = await _context.Tickets.Include("Project.Company").Where(t => t.Assignee!.Id == user.Id).Select(t => new TicketDto(t)).ToListAsync();

            return tickets;
        }

        public async Task<List<TicketDto>> ListTicketsByProject(Guid projectId)
        {
            List<TicketDto> tickets = new List<TicketDto>();
            List<Ticket> projectTickets = await _context.Tickets.Include("Project").Include("Assignee")
                .Where(ticket => ticket.Project!.Id == projectId).ToListAsync();
            tickets.AddRange(projectTickets.Select(ticket => new TicketDto(ticket)));
            return tickets;
        }

        public async Task<List<TicketDto>> ListTicketsByUser(string userId)
        {
            List<TicketDto> tickets = new List<TicketDto>();
            List<Ticket> userTickets = await _context.Tickets.Include("Assignee").Where(ticket => ticket.Assignee!.Id == userId)
                .ToListAsync();
            tickets.AddRange(userTickets.Select(ticket => new TicketDto(ticket)));
            return tickets;
        }
        
        public async Task<TicketDto> Get(Guid ticketId)
        {
            Ticket? ticket = await _context.Tickets.Include("Project").Include("Assignee")
                .FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
            if (ticket == null) throw new InvalidDataException("Invalid Ticket ID.");
            return new TicketDto(ticket);
        }

        public async Task<TicketDto> Create(TicketDto ticket)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == ticket.Assignee.Id);
            if (user == null) throw new InvalidDataException("Invalid User.");

            Project? project = await _context.Projects.FirstOrDefaultAsync(project => project.Uri == ticket.TicketUrl);
            if (project == null) throw new InvalidDataException("Invalid Project.");

            EntityEntry<Ticket> result = _context.Tickets.Add(new Ticket
            {
                Id = default,
                Name = ticket.Name,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                Assignee = user,
                DueDate = ticket.DueDate,
                EstimatedTime = TimeSpan.FromHours(ticket.EstimatedTime),
                Project = project
            });
            await _context.SaveChangesAsync();
            return new TicketDto(result.Entity);
        }

        public async Task<bool> Delete(Guid ticketId)
        {
            Ticket? ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
            if (ticket == null) return false;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<TicketDto> Update(Guid ticketId, TicketDto ticket)
        {
            Ticket? targetTicket = await _context.Tickets.Include("Assignee").FirstOrDefaultAsync(t => t.Id == ticketId);
            if (targetTicket == null) throw new InvalidDataException("Invalid Ticket.");

            User? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == ticket.Assignee.Id);
            if (user == null) throw new InvalidDataException("Invalid User.");

            targetTicket.Name = ticket.Name;
            targetTicket.Description = ticket.Description;
            targetTicket.Priority = ticket.Priority;
            targetTicket.Status = ticket.Status;
            targetTicket.Assignee = user;
            targetTicket.DueDate = ticket.DueDate;
            targetTicket.EstimatedTime = TimeSpan.FromHours(ticket.EstimatedTime);

            await _context.SaveChangesAsync();

            return new TicketDto(targetTicket);
        }
    }
}