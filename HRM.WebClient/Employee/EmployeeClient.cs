using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using HRM.Models.Employee;

namespace HRM.WebClient.Employee
{
    public class EmployeeClient : IEmployeeClient
    {
        private readonly string apiBaseUrl;

        public EmployeeClient()
        {
            apiBaseUrl = "https://localhost:44339/api/EmployeeAPI";
        }

        public List<EmployeeDetails> GetEmployees()
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl;
                var Response = client.GetAsync(endpoint).Result;

                if (Response.IsSuccessStatusCode)
                {
                    var data = Response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<EmployeeDetails>>(data);
                    return list;
                }
            }
            return list;
        }

        public EmployeeDetails GetEmployeeById(int id)
        {
            EmployeeDetails employee = new EmployeeDetails();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/" + id;
                var Response = client.GetAsync(endpoint).Result;

                if (Response.IsSuccessStatusCode)
                {
                    var data = Response.Content.ReadAsStringAsync().Result;
                    employee = JsonConvert.DeserializeObject<EmployeeDetails>(data);
                    return employee;
                }
            }
            return employee;
        }

        public bool AddEmployee(EmployeeDetails employeeDetails)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employeeDetails), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl;
                var Response = client.PostAsync(endpoint, content).Result;
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateEmployee(EmployeeDetails employeeDetails)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employeeDetails), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl;

                var Response = client.PutAsync(endpoint, content).Result;
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public bool DeleteEmployee(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/" + id;
                var Response = client.DeleteAsync(endpoint).Result;
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
