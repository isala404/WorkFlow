using System;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto {
    public class UserProductivityDto {
        public String Name { get; set; }

        public String Email { get; set; }

        public UserRole UserRole { get; set; }

        public Int32 NumOfProjects { get; set; } = 0;

        public Int32 NumOfTodoTickets { get; set; } = 0;

        public Int32 NumOfInProgressTickets { get; set; } = 0;

        public Int32 NumOfCompletedTickets { get; set; } = 0;
    }
}
