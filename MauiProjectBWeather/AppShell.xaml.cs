using MauiProjectBWeather.Models;
using MauiProjectBWeather.Views;

namespace MauiProjectBWeather
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Looping through the list of cities to create a ShellContent for each
            foreach (var city in CityPicture.List)
            {
                var sc = new ShellContent
                {
                    Title = city.Name,
                    // Create a route based on the city name (lowercase, no spaces)
                    Route = city.Name.ToLower().Replace(" ", ""),
                    // Create the ContentTemplate to point to ForecastPage with the city as parameter
                    ContentTemplate = new DataTemplate(() => new ForecastPage(city))
                };

                // Add the city to the shell menu 
                this.Items.Add(sc);
            }
        }
    }
}
