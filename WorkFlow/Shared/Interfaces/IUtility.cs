using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Interfaces
{
    public interface IUtility
    {
        Task<User?> GetUser();
    }
}
