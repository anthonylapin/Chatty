using Chatty.Data;
using Chatty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public MessageController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(int roomId, string message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var room = await _db.Rooms
                .Include(r => r.Messages)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if(room == null)
            {
                return NotFound();
            }

            if(!room.Users.Contains(user))
            {
                return BadRequest();
            }

            var msg = new Message
            {
                Text = message,
                Created = DateTime.Now,
                User = user,
                Room = room
            };

            await _db.Messages.AddAsync(msg);
            room.Messages.Add(msg);

            await _db.SaveChangesAsync();

            return RedirectToAction("Individual", "Room", new { id = room.Id });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Index(int id)
        {
            var msg = await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);

            if(msg == null)
            {
                return Json(new { success = false, message = "Bad request" });
            }

            _db.Messages.Remove(msg);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Message was deleted" });

            
        }
    }
}


