using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api2.Entities
{
    [Table("ms_user")]
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public virtual ICollection<ProductivityEntity> Users { get; set; } = new HashSet<ProductivityEntity>();
    }
}
