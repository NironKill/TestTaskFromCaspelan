using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}
