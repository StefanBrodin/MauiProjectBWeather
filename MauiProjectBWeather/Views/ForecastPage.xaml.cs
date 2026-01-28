using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MauiProjectBWeather.Models;
using MauiProjectBWeather.Services;

namespace MauiProjectBWeather.Views
{
    public class GroupedForecast
    {
        public string City { get; set; }
        public IEnumerable<IGrouping<DateTime, ForecastItem>> Items { get; set; }
    }

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
            // ({Binding Key, StringFormat='{0:D}'}), the data needs to be grouped by DateTime.Date.

            //var groupedData = forecast.Items
            //    .GroupBy(item => item.DateTime.Date)
            //    .Select(group => new Grouping<DateTime, ForecastItem>(group.Key, group))
            //    .ToList();

            var groupedData = forecast.Items
            .GroupBy(item => item.DateTime.Date)           // Group by DATE (2026-01-28)
            .OrderBy(g => g.Key)                           // Sort days ascending
            .Select(g => new
            {
                Key = g.Key,                               // DateTime (används i GroupHeader)
                Items = g.OrderBy(item => item.DateTime)   // sortera timmar inom dagen
            })
            .ToList();

            // 3. Koppla datan till ListView i XAML
            GroupedForecast.ItemsSource = groupedData;
        }
    }
    //public class Grouping<K, T> : List<T>
    //{
    //    public K Key { get; private set; }
    //    public Grouping(K key, IEnumerable<T> items)
    //    {
    //        Key = key;
    //        this.AddRange(items);
    //    }
    //}
}