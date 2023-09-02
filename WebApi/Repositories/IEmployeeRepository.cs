using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> Get(long id);
        Task<Employee> Get(Employee id);
        Task<Employee> Add(Employee item);
        Task<bool> Remove(int id);
        Task<bool> Update(Employee item);
    }
}
