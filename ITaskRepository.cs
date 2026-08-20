using TaskTrackerApi.Models;

namespace TaskTrackerApi.Repositories
{
    // DAO-style abstraction: controllers depend on this interface, not on
    // EF Core directly, so the data-access implementation can be swapped
    // (e.g. SQLite -> SQL Server) without touching business logic.
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
    }
}
