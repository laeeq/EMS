namespace ApiTest
{
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using WebApi.Models;

    [TestClass]
    public class UnitTest1
    {
        readonly string ApiURL = "http://localhost:7218/api/employee/";
        Employee employee = new Employee()
        {
            FirstName = "Test",
            LastName = "Test",
            BirthDate = DateTime.Now,
            Department = "Test",
            Email = "Test"
        };        

        [TestMethod]
        [DoNotParallelize]
        public void Add()
        {
            try 
            {
                var emp =  PostApi(ApiURL, employee).Result;
                
                Assert.IsNotNull(emp);
                Assert.IsNotNull(emp.Email) ;                 
            }
            catch(Exception ex) 
            {
                Assert.Fail(ex.Message );
            }     
        }

        [TestMethod]
        [DoNotParallelize]
        public void Get()
        {
            try
            {
                var emps = GetAllApi(ApiURL).Result;
                Assert.IsNotNull(emps);
                var emp = GetApi(ApiURL + emps.Last().Id).Result;
                Assert.IsNotNull(emp);
                Assert.IsNotNull(emp.Email);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [DoNotParallelize]
        public void Edit()
        {
            try
            {
                var emps = GetAllApi(ApiURL).Result;
                Assert.IsNotNull(emps);
                var emp = emps.Last();
                
                UpdateApi(ApiURL, emp);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }        
        

        [TestMethod]
        [DoNotParallelize]
        public void GetALL()
        {
            try
            {

                var emp =  GetAllApi(ApiURL ).Result;
                Assert.IsNotNull(emp);
                Assert.IsTrue(emp.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        [DoNotParallelize]
        public void Remove()
        {
            try
            {
                var emps = GetAllApi(ApiURL).Result;
                Assert.IsNotNull(emps);
                var emp = emps.Last();
                
                DeleteApi(ApiURL + emp.Id);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        private HttpClient GetHttpClient(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        private async Task<Employee> PostApi(string apiURL, Employee employee ) 
        {
            Employee employee1 = null;
            using (HttpClient client = GetHttpClient(apiURL))
            {
                //GET Method
                var response = await client.PostAsJsonAsync(apiURL, employee);
                if (response.IsSuccessStatusCode)
                {
                   employee1 = await response.Content.ReadFromJsonAsync<Employee>();                   
                }
                else

                    throw new Exception(await response.Content.ReadAsStringAsync());

                return employee1;
            }
        }
        private async Task UpdateApi(string apiURL, Employee employee )
        {
            using (HttpClient client = GetHttpClient(apiURL))
            {
                //GET Method
                var response = await client.PutAsJsonAsync(apiURL, employee);
                if (!response.IsSuccessStatusCode)                
                    throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
        private async Task DeleteApi(string apiURL)
        {
            using (HttpClient client = GetHttpClient(apiURL))
            {
                var response = await client.DeleteAsync(apiURL);
                if (!response.IsSuccessStatusCode)
                    throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
        private async Task<Employee> GetApi(string apiURL)
        {
            Employee employee = null;
            using (HttpClient client = GetHttpClient(apiURL))
            {
                //GET Method
                var response = await client.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    employee = await response.Content.ReadFromJsonAsync<Employee>(); 
                }
                else
                    throw new Exception(await response.Content.ReadAsStringAsync());

                return employee;
            }
        }
        private async Task<List<Employee>> GetAllApi(string apiURL)
        {
           List< Employee> employee = null;
            using (HttpClient client = GetHttpClient(apiURL))
            {
                //GET Method
                var response = await client.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    employee = await response.Content.ReadFromJsonAsync<List<Employee>>();
                }
                else
                    throw new Exception(await response.Content.ReadAsStringAsync());

                return employee;
            }
        }
    }
}