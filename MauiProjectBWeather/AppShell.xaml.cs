using MauiProjectBWeather.Models;
using MauiProjectBWeather.Views;

namespace MauiProjectBWeather
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Vi loopar igenom hela listan med städer i CityPicture-modellen
            foreach (var city in CityPicture.List)
            {
                var sc = new ShellContent
                {
                    Title = city.Name,
                    // Vi skapar en unik route baserat på namnet (utan mellanslag)
                    Route = city.Name.ToLower().Replace(" ", ""),
                    // Här skapar vi sidan och skickar med just den här stadens objekt
                    ContentTemplate = new DataTemplate(() => new ForecastPage(city))
                };

                // Lägg till staden i Shell-menyn (Items)
                this.Items.Add(sc);
            }
        }
    }
}
