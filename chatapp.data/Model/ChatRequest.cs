using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatapp.data.Model
{
    [Table("ChatRequest")]
    public  class ChatRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid CreatorId { get; set; }

        public Guid RoomId { get; set; }

        public string UserName { get; set; }

        public string Link { get; set; }

        public Guid? RespondingUser { get; set; }
    }
}
