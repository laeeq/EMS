using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class TestEmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> _employees = Repo.GetEmployees();
        private int _nextId = _employees.Count;
        public TestEmployeeRepository()
        {
        }
        public Task<List<Employee>> GetAll()
        {
            return Task.FromResult <List<Employee>>( _employees.OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList());
        }
        public Task<Employee> Get(long id)
        {
            return Task.FromResult < Employee > (_employees.Find(c => c.Id == id));
        }
        public async Task<Employee> Get(Employee employee)
        {
            employee.Id = ++_nextId;
            Employee emp = _employees.FirstOrDefault(c => (c.FirstName == employee.FirstName && c.LastName == employee.LastName) || c.Email == employee.Email);

            return emp;
        }
        public async Task<Employee> Add(Employee employee)
        {
            employee.Id = ++_nextId;
            _employees.Add(employee);
            return employee;
        }
        public async Task<bool> Remove(int id)
        {
            Employee employee = _employees.Find(c => c.Id == id);

            if (employee == null)
            {
                return false;
            }
            _employees.Remove(employee);
            return true;
        }
        public async Task<bool> Update(Employee employee)
        {
            int index = _employees.FindIndex(c => c.Id == employee.Id);
            if (index == -1)
            {
                return false;
            }
            _employees.RemoveAt(index);
            _employees.Add(employee);
            return true;
        }
    }
}

