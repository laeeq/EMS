using WebApi.Models;

namespace WebApi
{
    public class Repo
    {
        public static List<Employee> GetEmployees() 
        {
            int _nextId = 0;
            List<Employee> _employees = new List<Employee>();
            _employees.Add(new Employee() { Id = ++_nextId, FirstName = "Michael", LastName = "White", Email = "mike@domain.com", Department = "Sales", BirthDate = DateTime.Parse("9/12/1973") });
            _employees.Add(new Employee() { Id = ++_nextId, FirstName = "Robert", LastName = "Craft", Email = "rob@domain.com", Department = "IT", BirthDate = DateTime.Parse("9/11/1983") });
            _employees.Add(new Employee() { Id = ++_nextId, FirstName = "Adam", LastName = "Gilchrist", Email = "adam@domain.com", Department = "Accounts", BirthDate = DateTime.Parse("12/2/1995") });
            _employees.Add(new Employee() { Id = ++_nextId, FirstName = "Ricky", LastName = "Ponting", Email = "ricky@domain.com", Department = "Manufacturing", BirthDate = DateTime.Parse("4/5/1989") });
            _employees.Add(new Employee() { Id = ++_nextId, FirstName = "Josh", LastName = "Abbot", Email = "josh@domain.com", Department = "Logistics", BirthDate = DateTime.Parse("2/17/1980") });

            return _employees;
        }
    }
}
