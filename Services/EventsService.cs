using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using intevent_web.Models;

namespace intevent_web.Services
{
    public interface IEventsService
    {
        Task<Event> GetEvent(int id);

        Task<Event> CreateEvent();
    }

    public class EventsService : IEventsService
    {
        private HttpClient Client { get; }

        public EventsService(HttpClient client)
        {
            Client = client;
        }

        public async Task<Event> GetEvent(int id)
        {
            HttpResponseMessage response = await Client.GetAsync($"api/Events/{id}");
            return await response.Content.ReadAsAsync<Event>();
        }

        public async Task<Event> CreateEvent()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync<Event>("api/Events/", null);
            return await response.Content.ReadAsAsync<Event>();
        }
    }
}
