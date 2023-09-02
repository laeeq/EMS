using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public  class EmployeeService
    {
        string baseUri;
        public EmployeeService(string BaseUri)
        {
            baseUri = BaseUri;
        }
        public  async Task<List<Employee>> GetEmployees()
        {
                List<Employee> employees = new List<Employee>();
                using (HttpClient client = GetHttpClient())
                {
                    var response =await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
                    return employees;
                }
                else
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
        }
        public async Task<bool> AddEmployee(Employee emp)
        {
                List<Employee> employees = new List<Employee>();
                using (HttpClient client = GetHttpClient())
                {
                    var response =await client.PostAsJsonAsync(baseUri, emp);
                    if (response.IsSuccessStatusCode)                 
                        return true;
                    else
                        throw new Exception(await response.Content.ReadAsStringAsync());
                }
        }
        public async Task<bool> DeleteEmployee(int id)
        {
                List<Employee> employees = new List<Employee>();
                using (HttpClient client = GetHttpClient())
                {
                    var response = await client.DeleteAsync(baseUri+"/"+ id);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        throw new Exception(await response.Content.ReadAsStringAsync());
                }
           
        }
        public async Task<bool> UpdateEmployee(Employee emp)
        {
                List<Employee> employees = new List<Employee>();
                using (HttpClient client = GetHttpClient())
                {
                    var response = await client.PutAsJsonAsync($"{baseUri}/{emp.Id}", emp);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        throw new Exception(await response.Content.ReadAsStringAsync());
                }
        }
        public HttpClient GetHttpClient() 
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
