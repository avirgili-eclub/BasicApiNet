using System.ComponentModel.DataAnnotations;

namespace BasicApiNet.Core.Models;

public class Country : BaseEntity
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; }
    
    public ICollection<City> Cities { get; } = new List<City>();
}