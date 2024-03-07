using System.ComponentModel.DataAnnotations;

namespace BasicApiNet.Core.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}