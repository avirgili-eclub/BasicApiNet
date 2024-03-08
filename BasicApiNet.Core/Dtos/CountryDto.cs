namespace BasicApiNet.Core.Dtos;

public class CountryDto
{
    
    public string Name { get; set; }
    
    public List<CityDto> Cities { get; } = new List<CityDto>();
    // si se va acceder a la lista de ciudades, se debe hacer a traves de un metodo en vez de una propiedad.
    // Para evitar que se modifique la lista sin pasar por el metodo de negocio citiservice.
    
    // public IReadOnlyCollection<City> Cities => _cities.AsReadOnly();

    public void AddCities(IEnumerable<CityDto> cities)
    {
        Cities.AddRange(cities);
    }
}