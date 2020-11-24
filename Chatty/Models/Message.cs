using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
