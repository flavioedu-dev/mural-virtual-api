using MuralVirtual.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuralVirtual.Domain.Entities;

[Table("users")]
public class User : BaseEntity
{
    public string? FullName { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
