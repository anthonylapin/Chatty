using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.ViewModels
{
    public class RoomViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
