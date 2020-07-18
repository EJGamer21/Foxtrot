using System;
using System.Linq;
using System.Threading.Tasks;
using Foxtrot.Dtos;
using Foxtrot.Enums;
using Foxtrot.Extensions;
using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bcrypt = BCrypt.Net.BCrypt;

namespace Foxtrot.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public AccessController(IUserRepository userRepository, IRoleRepository roleRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            if (_httpContextAccessor.HttpContext.IsUserLoggedIn())
                return RedirectToAction("Index", "Appointments");
            
            return View("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] AccessDto data)
        {
            try
            {
                var users = await _userRepository.Get(u => !u.IsDeleted);
                var user = users.FirstOrDefault(u => u.Email == data.Email || u.Dni == data.Dni);

                if (user == null)
                    return Unauthorized(new {Message = "Incorrect user or password"});

                if (!Bcrypt.Verify(data.Password, user.Password ?? string.Empty))
                    return Unauthorized(new {Message = "Incorrect user or password"});

                _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("User_FullName", user.FullName);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(new {Message = "Error.", Data = e.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] AccessDto data)
        {
            try
            {
                var user = await _userRepository.Get(u => !u.IsDeleted && u.Email == data.Email || u.Dni == data.Dni);

                if (user.Any())
                    return BadRequest(new {Message = "Existing user"});

                await _userRepository.Insert(new User
                {
                    Id = new Guid(),
                    FullName = data.FullName,
                    Dni = data.Dni,
                    Role = await _roleRepository.GetById(RoleEnum.Standard),
                    Email = data.Email,
                    Password = Bcrypt.HashPassword(data.Password)
                });
                await _userRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error.", Data = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Logout([FromForm] int value)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserId", string.Empty);
            return RedirectToAction(nameof(Index));
        }
    }
}