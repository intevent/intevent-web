using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using intevent_web.Models;
using intevent_web.Services;

namespace intevent_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventsService EventsService { get; }

        public EventsController(IEventsService eventsService)
        {
            EventsService = eventsService;
        }

        // GET api/events/{id}
        [HttpGet("{id}")]
        public async Task<Event> Get(int id)
        {
            return await EventsService.GetEvent(id);
        }

        // POST api/votes
        [HttpPost]
        public async Task<Event> Post(/*[FromBody]string value*/)
        {
            return await EventsService.CreateEvent();
        }
    }
}
