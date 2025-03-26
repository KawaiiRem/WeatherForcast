using System.Collections.ObjectModel;
using WeatherForcast.Interfaces;
using WeatherForcast.Service;

namespace WeatherForcast;

public partial class WeatherApp : ContentPage
{
    public List<Models.List> WeatherList;
    //public ObservableCollection<Models.List> WeatherList; #another fix 
    private double latitude = 10.809461886679978;
    private double longitude = 106.65314688514171;
    private bool preference = true;
    private string currentCity = "Ho Chi Minh";
    private string locationState;
    private readonly IApiService apiService;
    public WeatherApp(IApiService service)
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
        apiService = service;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        locationState = "lalo";
        var result = preference ? await apiService.GetWeatherCelsius(latitude, longitude) : await apiService.GetWeatherFahrenheit(latitude, longitude);
        //var result = preference ? await ApiService.GetWeatherByCityCelsius(currentCity) : await ApiService.GetWeatherByCityFahrenheit(currentCity);
        updateUI(result);
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        locationState = "lalo";
        var result =preference ? await apiService.GetWeatherCelsius(latitude, longitude) : await apiService.GetWeatherFahrenheit(latitude, longitude); ;
        updateUI(result);
        
    }

    public async void ImageButtonClicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }
    public async Task GetWeatherDataByCity(string city)
    {
        locationState = "city";
        var result = preference ? await apiService.GetWeatherByCityCelsius(city) : await apiService.GetWeatherByCityFahrenheit(city);
        currentCity = result.city.name;
        updateUI(result);
    }

    public void updateUI(dynamic result)
    {
        WeatherList.Clear();
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = new List<Models.List>(WeatherList);
        //CvWeather.ItemsSource = WeatherList; #if WeatherList is ObservableCollection<Models.List>

        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        var scale = preference ? "℃" : "°F";
        LblTemperature.Text = result.list[0].main.temperature + scale;
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }

    private void TemperatureSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        preference = !preference;
        switch (locationState)
        {
            case "city":
                GetWeatherDataByCity(currentCity);
                break;
            case "lalo":
                GetWeatherDataByLocation(latitude, longitude);
                break;
            default: break;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        GetLocation();
    }
}
