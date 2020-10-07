using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EducationalGames.Models
{
    public class PeriodicTableDAL
    {
        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.datastro.eu/api/records/1.0/search/");
            return client;
        }

        public async Task<PeriodicTable> GetElements()
        {
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync("?dataset=periodic-table&q=&rows=400&facet=symbol&facet=name&facet=yeardiscovered&facet=standardstate&facet=groupblock");
            PeriodicTable table = await response.Content.ReadAsAsync<PeriodicTable>();
            return table;
        }
    }
}
