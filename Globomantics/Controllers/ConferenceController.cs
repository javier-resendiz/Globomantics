using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Globomantics.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Globomantics.Controllers
{
    public class ConferenceController:Controller
    {
        private readonly IConferenceService conferenceService;
        public ConferenceController(IConferenceService conferenceService)
        {
            this.conferenceService = conferenceService;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Conferences";
            return View(await conferenceService.GetAll());
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Add Conference";
            return View(new ConferenceModel());
        }
        [HttpPost]
        public async Task<ActionResult> Add(ConferenceModel model)
        {
            if (ModelState.IsValid)
            {
                await conferenceService.Add(model);
            }
            return RedirectToAction("Index");
        }
    }
}
