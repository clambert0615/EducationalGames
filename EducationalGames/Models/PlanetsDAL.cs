using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EducationalGames.Models
{
    public class PlanetsDAL
    {
        public HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://localhost:44360");
            return client;
        }
        public async Task<List<Planets>> GetPlanets()
        {
            HttpClient client = GetHttpClient();
            var response = await client.GetAsync($"api/planets/");
            //Install-package Microsoft.AspNet.WebAPI.Client
            List<Planets> booklist = await response.Content.ReadAsAsync<List<Planets>>();
            return booklist;

        }


    }
}
