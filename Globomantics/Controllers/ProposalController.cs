using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Globomantics.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Globomantics.Controllers
{
    public class ProposalController : Controller
    {
        private readonly IConferenceService conferenceService;
        private readonly IProposalService proposalService;
        public ProposalController(IConferenceService conferenceService, IProposalService proposalService)
        {
            this.proposalService = proposalService;
            this.conferenceService = conferenceService;
        }
        public async Task<IActionResult> Index(int conferenceId)
        {
            var conference = await conferenceService.GetById(conferenceId);
            ViewBag.Title = $"Proposals for conference {conference.Name} {conference.Location}";
            ViewBag.ConferenceId = conferenceId;
            return View(await proposalService.GetAll(conferenceId));
        }

        public IActionResult Add(int conferenceId)
        {
            ViewBag.Title = "Add Proposal";
            return View(new ProposalModel { ConferenceId = conferenceId });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProposalModel proposal)
        {
            if (ModelState.IsValid)
                await proposalService.Add(proposal);
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }

        public async Task<IActionResult> Approve(int proposalId)
        {
            var proposal = await proposalService.Approve(proposalId);
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }
    }
}
