using System;
using System.Threading.Tasks;
using Foxtrot.Dtos;
using Foxtrot.Enums;
using Foxtrot.Extensions;
using Foxtrot.Models;
using Microsoft.AspNetCore.Mvc;
using Foxtrot.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Foxtrot.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly FoxtrotContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentsController(IAppointmentRepository appointmentRepository,
            IServiceRepository serviceRepository, IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor, FoxtrotContext context)
        {
            _appointmentRepository = appointmentRepository;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            if (!_httpContextAccessor.HttpContext.IsUserLoggedIn())
                return RedirectToAction("Index", "Access");
            
            return View(await _appointmentRepository.Get());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _appointmentRepository.GetById(id);
            return appointment == null
                ? (IActionResult) NotFound() 
                : View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await _serviceRepository.Get();
            ViewBag.Providers = await _userRepository.Get(u => u.Role.Id == RoleEnum.Provider);
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AppointmentDto appointmentDto)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepository.Insert(new Appointment
                {
                    Note = appointmentDto.Note,
                    Creator = await _userRepository.GetById(_httpContextAccessor.HttpContext.GetLoggedUserId()),
                    Provider = await _userRepository.GetById(appointmentDto.ProviderId),
                    Service = await _serviceRepository.GetById(appointmentDto.ServiceId),
                    Status = await _context.AppointmentStatus
                        .FirstOrDefaultAsync(s => s.Id == (int) AppointmentStatusEnum.Opened),
                    StartDate = appointmentDto.StartDate,
                    EndDate = appointmentDto.EndDate
                });
                await _appointmentRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _appointmentRepository.GetById(id);
            ViewBag.Appointment = appointment;
            ViewBag.Services = await _serviceRepository.Get();
            ViewBag.Providers = await _userRepository.Get();
            ViewBag.Statuses = await _context.AppointmentStatus.ToListAsync();

            return appointment == null 
                ? (IActionResult) NotFound() 
                : View();
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] AppointmentDto appointmentDto)
        {
            if (id != appointmentDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                Appointment appointment = await _appointmentRepository.GetById(id);
                appointment.Note = appointmentDto.Note;
                appointment.StartDate = appointmentDto.StartDate;
                appointment.EndDate = appointmentDto.EndDate;
                appointment.Service = await _serviceRepository.GetById(appointmentDto.ServiceId);
                appointment.Provider = await _userRepository.GetById(appointmentDto.ProviderId);
                appointment.Status = await _context.AppointmentStatus.FindAsync(appointmentDto.StatusId);
                
                await _appointmentRepository.Update(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appointmentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
