using WeatherForcast.Service;

namespace WeatherForcast
{
    public partial class App : Application
    {
        public App()
        {
                InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new WeatherApp(new ApiService())) { Title = "WeatherForcast" };
        }
    }
}
