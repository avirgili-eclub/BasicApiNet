using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Dtos;

public class CountryWithCitiesRequest
{
    public Country Country { get; set; }
    public List<City> Cities { get; set; }
}