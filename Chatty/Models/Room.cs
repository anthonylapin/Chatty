using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
