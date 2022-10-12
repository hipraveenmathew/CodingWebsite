using CodingWebsite.Data;
using CodingWebsite.Models;
using CodingWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodingWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _logger = logger;
        }

        
        public IActionResult Index()
        {
           string userId = _userService.GetUserId();
            if(userId != null)
            {
                var userlogged = _context.UserDetails.FirstOrDefault(m => m.UserId== userId);
                if(userlogged == null)
                { 
                    return View("SelectRole");
                }
                else
                {
                    if (userlogged.UserRole == 1)
                    {
                        return View("Student");
                        
                    }
                    else
                    {
                        return View("Teacher");
                    }
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}