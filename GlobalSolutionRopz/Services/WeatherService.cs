using GlobalSolutionRopz.Model;
using System.Text.Json;

namespace GlobalSolutionRopz.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "Coloque sua chave do OpenWeather";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string descricao, double temperatura)> ObterClimaAsync(string estado)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={estado}&appid={_apiKey}&units=metric&lang=pt_br";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return ("Erro ao obter clima", 0);
            }

            var content = await response.Content.ReadAsStringAsync();
            using var json = JsonDocument.Parse(content);

            var descricao = json.RootElement.GetProperty("weather")[0].GetProperty("description").GetString();
            var temperatura = json.RootElement.GetProperty("main").GetProperty("temp").GetDouble();

            return (descricao!, temperatura);
        }

        internal async Task GetWeatherByCityAsync(string estado)
        {
            throw new NotImplementedException();
        }
    }

}

