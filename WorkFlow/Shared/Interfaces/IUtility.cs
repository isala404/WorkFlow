using System.Threading.Tasks;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Interfaces {
    public interface IUtility {
        Task<User?> GetUser();
    }
}
