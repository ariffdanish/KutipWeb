using KutipWeb.Data;
using KutipWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KutipWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly KutipDbContext _context;
        public UserController(KutipDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<User> users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User user = new User();        
            return View(user);
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash the password
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }


        public IActionResult Details(int Id)
        {
            User user = _context.Users.FirstOrDefault(a => a.UserId == Id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.UserId == user.UserId);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Preserve CreatedAt and update UpdatedAt
                user.CreatedAt = existingUser.CreatedAt;
                user.UpdatedAt = DateTime.Now;

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    // Password field empty: keep old hashed password
                    user.Password = existingUser.Password;
                }
                else
                {
                    // Password provided: hash it
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}
