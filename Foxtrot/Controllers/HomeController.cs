using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foxtrot.Dtos;
using Foxtrot.Enums;
using Foxtrot.Extensions;
using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foxtrot.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppointmentRepository _appointmentRepository;

        public HomeController(IHttpContextAccessor httpContextAccessor, IAppointmentRepository appointmentRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (!_httpContextAccessor.HttpContext.IsUserLoggedIn())
                return RedirectToAction("Index", "Access");

            DashboardDto data = new DashboardDto
            {
                Opened = await _appointmentRepository
                    .Count(a => !a.IsDeleted && a.Status.Id == (int) AppointmentStatusEnum.Opened),
                Closed = await _appointmentRepository
                    .Count(a => !a.IsDeleted && a.Status.Id == (int) AppointmentStatusEnum.Closed),
                Pending = await _appointmentRepository
                    .Count(a => !a.IsDeleted && a.Status.Id == (int) AppointmentStatusEnum.Pending),
                Cancelled = await _appointmentRepository
                    .Count(a => !a.IsDeleted && a.Status.Id == (int) AppointmentStatusEnum.Cancelled)
            };

            data.ClosedAvg = Math.Round(data.Closed / (double) await _appointmentRepository.Count(), 2);
            
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}