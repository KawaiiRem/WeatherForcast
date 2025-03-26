using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherForcast.Interfaces;
using WeatherForcast.Models;

namespace WeatherForcast.Service
{
    public class ApiService : IApiService
    {
        public async Task<Root> GetWeatherCelsius(double latitude, double longtitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=23b99159099cc0032bf0bcd5f5f343de", latitude, longtitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }
        public async Task<Root> GetWeatherFahrenheit(double latitude, double longtitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid=23b99159099cc0032bf0bcd5f5f343de", latitude, longtitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }

        public async Task<Root> GetWeatherByCityCelsius(string cityName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=23b99159099cc0032bf0bcd5f5f343de", cityName));
            return JsonConvert.DeserializeObject<Root>(response);
        }
        public async Task<Root> GetWeatherByCityFahrenheit(string cityName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&appid=23b99159099cc0032bf0bcd5f5f343de", cityName));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
