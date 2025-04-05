using System.ComponentModel.DataAnnotations;

namespace MuralVirtual.Domain.Entities.Base;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
