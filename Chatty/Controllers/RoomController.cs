using Chatty.Data;
using Chatty.Models;
using Chatty.ViewModels;
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
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public RoomController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Room> rooms = _db.Rooms
                .Include(r => r.Users)
                .Where(r => r.Users.Contains(user))
                .ToList();

            return View(rooms);
        }

        [Authorize]
        public async Task<IActionResult> Individual(int id)
        {
            var room = await _db.Rooms
                .Include(r => r.Messages)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            if(room == null)
            {
                return NotFound();
            }

            if(!room.Users.Contains(user))
            {
                room.Users.Add(user);
                await _db.SaveChangesAsync();
            }


            return View(room);
        }

        [Authorize]
        public async Task<IActionResult> Search(string name)
        {
            var rooms = await _db.Rooms.Where(r => r.Name == name).ToListAsync();
            return View(rooms);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RoomViewModel roomVM)
        {
            if(ModelState.IsValid)
            {
                var owner = await _userManager.FindByNameAsync(User.Identity.Name);
                var room = new Room { Name = roomVM.Name, Created = DateTime.Now };

                await _db.Rooms.AddAsync(room);
                room.Users.Add(owner);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(roomVM);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Leave(int id)
        {
            string msg = "Error, room is not found";
            bool success = false;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var room = await _db.Rooms.Include(r => r.Users)
                .Where(r => r.Users.Contains(user))
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room != null)
            {
                if(room.Users.Count == 1)
                {
                    _db.Rooms.Remove(room);
                }
                else
                {
                    room.Users.Remove(user);
                }
                
                await _db.SaveChangesAsync();
                msg = "room was leaved";
                success = true;
            }

            return Json(new { msg, success });
        }
    }
}
