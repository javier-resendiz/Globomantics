using System;
using Globomantics.Services;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Controllers
{
    public class ConferenceController:Controller
    {
        public ConferenceController(IConferenceService conferenceService)
        {
        }
    }
}
