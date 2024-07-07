using CleanArchitecture.Api.Services;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly GoogleCalendarService _calendarService;

        public CalendarController()
        {
            _calendarService = new GoogleCalendarService();
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _calendarService.GetUpcomingEventsAsync();
            return Ok(events);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventModel model)
        {
            var eventLink = await _calendarService.CreateEventAsync(model.Summary, model.Location, model.Description, model.StartDateTime, model.EndDateTime);
            return Ok(new { Link = eventLink });
        }

        [HttpDelete("cancel/{eventId}")]
        public async Task<IActionResult> CancelEvent(string eventId)
        {
            var success = await _calendarService.CancelEventAsync(eventId);
            if (success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("advisor/{advisorEmail}")]
        public async Task<IActionResult> GetAdvisorCalendar(string advisorEmail)
        {
            var events = await _calendarService.GetAdvisorCalendarAsync(advisorEmail);
            return Ok(events);
        }

        [HttpPost("setNotification/{eventId}")]
        public async Task<IActionResult> SetNotificationPreferences(string eventId, [FromBody] EventReminder[] reminders)
        {
            var eventLink = await _calendarService.SetNotificationPreferencesAsync(eventId, reminders);
            return Ok(new { Link = eventLink });
        }

        [HttpPut("update/{eventId}")]
        public async Task<IActionResult> UpdateEvent(string eventId, [FromBody] EventModel model)
        {
            var eventLink = await _calendarService.UpdateEventAsync(eventId, model.Summary, model.Location, model.Description, model.StartDateTime, model.EndDateTime);
            return Ok(new { Link = eventLink });
        }
    }

    public class EventModel
    {
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}