// Stefan Brodin
// https://github.com/StefanBrodin/MauiProjectBWeather.git

using MauiProjectBWeather.Helpers;
using MauiProjectBWeather.Models;
using MauiProjectBWeather.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiProjectBWeather.Views
{

    public partial class ForecastPage : ContentPage
    {
        private OpenWeatherService _service;
        private CityPicture _city;

        public ForecastPage(CityPicture city)
        {
            InitializeComponent();

            _city = city;
            _service = new OpenWeatherService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = $"Forecast for {_city.Name}";

            MainThread.BeginInvokeOnMainThread(async () => { await LoadForecast(); });
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await LoadForecast();
        }

        private async Task LoadForecast()
        {
            // 1. Load the forecast data from the service
            Forecast forecast = await _service.GetForecastAsync(_city.Name);

            // 2. Since IsGroupingEnabled="true" in the XAML ListView, and has a GroupHeaderTemplate that shows date 
            // ({Binding Key, StringFormat='{0:D}'}), the data needs to be grouped by DateTime.

            var groupedData = forecast.Items
                .GroupBy(item => item.DateTime.Date)
                .Select(group => new Grouping<DateTime, ForecastItem>(group.Key, group))
                .ToList();

            // 3. Connect the data to ListView i XAML
            GroupedForecast.ItemsSource = groupedData;
        }
    }
    public class Grouping<K, T> : List<T>
    {
        public K Key { get; private set; }

        // This line fixes the formatting of the date with capital letter in the Group Header
        public string FormattedDate => Key is DateTime dt
            ? dt.ToString("dddd 'den' d MMMM yyyy").FirstCharToUpper()
            : Key?.ToString();

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            this.AddRange(items);
        }
    }
}