using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForcast.Models;

namespace WeatherForcast.Interfaces
{
    public interface IApiService
    {
        public Task<Root> GetWeatherCelsius(double latitude, double longtitude);
        public Task<Root> GetWeatherFahrenheit(double latitude, double longtitude);
        public Task<Root> GetWeatherByCityCelsius(string cityName);
        public Task<Root> GetWeatherByCityFahrenheit(string cityName);
    }
}
