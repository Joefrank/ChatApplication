using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatapp.data.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ContextId { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(100)]
        public string ChatPseudo { get; set; }
       
        public Guid? RoomId { get; set; }

        public DateTime DateCreated { get; set; }
       
    }
}
