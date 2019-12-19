using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bushour.api
{
    public class BusHourService : IBusHourService
    {
        private readonly string _urlBus = @"http://200.238.84.28/site/consulta/ajax.asp?opcao=0";
        private readonly string _urlHour = @"http://200.238.84.28/site/consulta/ajax.asp?opcao=1&linha=";
        private readonly HttpClient http = new HttpClient();

        public BusHourService() {}

        public async Task<IEnumerable<LinhaVM>> GetBusId(string id)
        {
            HttpResponseMessage response = await http.GetAsync(_urlBus);
            IEnumerable<LinhaVM> data = null;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<LinhaVM>>(result);
            }

            if (!string.IsNullOrEmpty(id) && !id.Equals("0"))
            {
                data = data.Where(b => b.codigo_linha.Equals(id));
            }

            return data;
        }

        public async Task<IEnumerable<LinhaVM>> GetBusName(string name)
        {
            HttpResponseMessage response = await http.GetAsync(_urlBus);
            IEnumerable<LinhaVM> data = null;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<LinhaVM>>(result);
            }

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(b => b.descricao_linha.Trim().ToUpper().Contains(name.ToUpper()));
            }

            return data;
        }

        public async Task<IEnumerable<HorarioVM>> GetHours(string line)
        {
            HttpResponseMessage response = await http.GetAsync(_urlHour + line);
            IEnumerable<HorarioVM> data = null;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<HorarioVM>>(result);                
            }
            return data;
        }

    }
}
