using BelgianTreat.Services;
using BelgianTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BelgianTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send the mail
                _mailService.SendMessage("quentin.carpentier@outlook.be",
                    model.Subject, 
                    $"From: {model.Name} - {model.Email}, Message: {model.Message}");

                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
