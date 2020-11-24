using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Models
{
    public class User : IdentityUser
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
