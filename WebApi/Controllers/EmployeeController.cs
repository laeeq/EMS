using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]    
    public class EmployeeController : ControllerBase
    {
        static IEmployeeRepository repository = null;
        public EmployeeController(IEmployeeRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await repository.GetAll();            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            Employee item =await repository.Get(id);
            if (item == null)
            {
                return NotFound(id); //throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return Ok(item);
        }
        
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] Employee item)//HttpResponseMessage
        {
            var existing = await repository.Get(item);

            if (!string.IsNullOrEmpty(existing?.FirstName))
            {
                return Conflict(item.ToString() + " already exist");
            }

            item = await repository.Add(item);      
            return Ok(item);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> PutEmployee(int id, [FromBody] Employee employee)
        {
            employee.Id = id;
            if (!await repository.Update(employee))
            {
                return NotFound(id); //throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return Ok($"Employee {employee.Id} updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteEmployee(int id)
        {
            if (!await repository.Remove(id))
            {
                return NotFound(id);
            }
            return Ok($"Employee {id} deleted successfully");
        }
    }
}