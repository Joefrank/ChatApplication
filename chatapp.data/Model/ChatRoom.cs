using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatapp.data.Model
{
    [Table("ChatRoom")]
    public class ChatRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
