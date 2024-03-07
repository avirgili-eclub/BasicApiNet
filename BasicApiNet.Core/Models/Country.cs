using System.ComponentModel.DataAnnotations;

namespace BasicApiNet.Core.Models;

public class Country : BaseEntity
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; }
    
    public List<City> Cities { get; } = new List<City>();
    // si se va acceder a la lista de ciudades, se debe hacer a traves de un metodo en vez de una propiedad.
    // Para evitar que se modifique la lista sin pasar por el metodo de negocio citiservice.
    
    // public IReadOnlyCollection<City> Cities => _cities.AsReadOnly();

    public void AddCities(IEnumerable<City> cities)
    {
        Cities.AddRange(cities);
    }
}