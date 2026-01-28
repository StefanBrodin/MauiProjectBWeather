using MauiProjectBWeather.Models;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable
namespace MauiProjectBWeather.Models;

public class Forecast
{
    public string? City { get; set; }
    public List<ForecastItem>? Items { get; set; }
}
#nullable restore

