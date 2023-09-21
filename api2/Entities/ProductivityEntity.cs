using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api2.Entities
{
    [Table("mt_productivity")]
    public class ProductivityEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } 
        public Guid IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public virtual UserEntity User { get; set; }
    }
}
