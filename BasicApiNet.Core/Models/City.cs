using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicApiNet.Core.Models;

public class City 
{
    [Key]
    public int Id { get; init; }
    [Required]
    [Column(TypeName = "nvarchar(80)")]
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
}