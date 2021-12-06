using WorkFlow.Shared.Interfaces;
using WorkFlow.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace WorkFlow.Server.Models
{
    public class TicketModel : ITicket
    {
        readonly List<Ticket> _ticketList = new()
        {
            new Ticket
            {
                Id = Guid.Parse("eb4d87d3-f8d2-435a-83e1-b396922ac52c"),
                Name = "Create Something Something",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tinciduLorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed....",
                Priority = Priority.LOW,
                Assignee = new User { UserName = "supiri", Name = "Isala Piyarisi" },
                Status = Status.ToDo,
                DueDate = DateTime.Parse("03/11/2021"),
                EstimatedTime = TimeSpan.Parse("02:00:00")
            }
        };
        public Task<Ticket> CreateTicket(Ticket ticket)
        {
            _ticketList.Add(ticket);
            return Task.FromResult(ticket);
        }

        public Task<bool> DeleteTicket(Guid ticketID)
        {
            foreach(var ticket in _ticketList)
            {
                if(ticket.Id == ticketID)
                {
                    _ticketList.Remove(ticket);
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<Ticket?> GetTicket(Guid ticketID)
        {
            Ticket? ticket = null;
            for (int i = 0; i < _ticketList.Count; i++)
            {
                if (_ticketList[i].Id == ticketID)
                {
                    ticket = _ticketList[i];
                    break;
                }
            }
            return Task.FromResult(ticket);
        }

        public Task<List<Ticket>> List(User user)
        {
            if (true)
            {
                // TODO: Return only tickets of project user is part of
                return Task.FromResult(_ticketList);
            }
            // TODO: Return only tickets of Companies user is part of
            return Task.FromResult(_ticketList);
        }

        public Task<List<Ticket>> ListTicketsByProject(Guid projectID)
        {
            return Task.FromResult(_ticketList);
        }

        public Task<List<Ticket>> ListTicketsByUser(Guid userID)
        {
            return Task.FromResult(_ticketList);
        }

        public Task<Ticket?> UpdateTicket(Guid ticketID, Ticket ticket)
        {
            Ticket? _ticket = null;
            for (int i = 0; i < _ticketList.Count; i++)
            {
                if (_ticketList[i].Id == ticketID)
                {
                    _ticketList[i] = ticket;
                    _ticket = _ticketList[i];
                    break;
                }
            }
            return Task.FromResult(_ticket);
        }
    }
}
