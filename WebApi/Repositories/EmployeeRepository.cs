using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;
        public DbSet<Employee> _employees { get { return _context.Employees; } }
        private int _nextId = 0;
        public EmployeeRepository(DatabaseContext cs)
        {
            //var o = new DbContextOptions<DatabaseContext>();
            _context = cs;//new DatabaseContext();
        }
        public Task< List<Employee>> GetAll()
        {
            return Task.FromResult < List<Employee >>( _employees.OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToList());

        }
        public Task<Employee> Get(long id)
        {
            return Task.FromResult<Employee>( _employees.FirstOrDefault(c => c.Id == id));
        }
        public async Task<Employee> Get(Employee employee)
        {
            //employee.Id = ++_nextId;
            Employee emp = _employees.FirstOrDefault(c => (c.FirstName == employee.FirstName && c.LastName == employee.LastName) || c.Email == employee.Email);

            return emp;
        }
        public async Task<Employee> Add(Employee employee)
        {
            //employee.Id = ++_nextId;
            Employee emp = _employees.FirstOrDefault(c => (c.FirstName == employee.FirstName && c.LastName == employee.LastName) || c.Email == employee.Email);
            if (emp != null)
            {

            }
            _employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<bool> Remove(int id)
        {
            Employee employee = _employees.FirstOrDefault(c => c.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            else return false;

            return true;
        }
        public async Task<bool> Update(Employee employee)
        {
            var emp =await Get(employee.Id);
            if (emp != null)
            {
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Email = employee.Email;
                emp.Department = employee.Department;
                emp.BirthDate = employee.BirthDate;

                _context.Update(emp);
                await _context.SaveChangesAsync();
            }
            else
                return false;

            return true;
        }
    }
}
