using System;
using System.Linq;
using System.Threading.Tasks;
using Foxtrot.Dtos;
using Foxtrot.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;
using Microsoft.AspNetCore.Http;

namespace Foxtrot.Controllers
{
    public class UsersController : Controller
    {
        private readonly FoxtrotContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(FoxtrotContext context, IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (!_httpContextAccessor.HttpContext.IsUserLoggedIn())
                return RedirectToAction("Index", "Access");
            
            var users = await _context.Users.Where(x => !x.IsDeleted).ToListAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDto userDto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userDto.Email) && !string.IsNullOrWhiteSpace(userDto.Dni))
                {
                    await _context.AddAsync(new User
                    {
                        Id = new Guid(),
                        FullName = userDto.FullName,
                        Email = userDto.Email,
                        Address = userDto.Address,
                        Dni = userDto.Dni,
                        Role = await _context.Roles.FindAsync(userDto.RoleId)
                    });
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Has been an error."});
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
            
            ViewBag.User = user;
            ViewBag.Roles = await _context.Roles.ToListAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = await _context.Users.FindAsync(userDto.Id);
                    user.Address = userDto.Address;
                    user.FullName = userDto.FullName;
                    user.Email = userDto.Email;
                    user.Dni = userDto.Dni;
                    user.Role = await _context.Roles.FindAsync(userDto.RoleId);
                    
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
