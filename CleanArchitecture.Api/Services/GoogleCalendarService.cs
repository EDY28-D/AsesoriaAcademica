using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Services
{
    public class GoogleCalendarService
    {
        private readonly Config _config;

        public GoogleCalendarService()
        {
            _config = LoadConfig();
        }

        private Config LoadConfig()
        {
            string configContent = File.ReadAllText("calendarSettings.json"); ;
            return JsonConvert.DeserializeObject<Config>(configContent);
        }

        private async Task<CalendarService> GetCalendarServiceAsync()
        {
            UserCredential credential;

            using (var stream = new FileStream(_config.CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    _config.User,
                    CancellationToken.None,
                    new FileDataStore(_config.TokenPath, _config.FileDataStore)
                );
            }

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _config.ApplicationName,
            });
        }

        public async Task<Events> GetUpcomingEventsAsync(int maxResults = 10)
        {
            var service = await GetCalendarServiceAsync();

            EventsResource.ListRequest request = service.Events.List(_config.CalendarId);
            request.TimeMin = DateTime.Now; 
            request.ShowDeleted = _config.ShowDeleted;
            request.SingleEvents = _config.SingleEvents;
            request.MaxResults = maxResults;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            return await request.ExecuteAsync();
        }

        public async Task<string> CreateEventAsync(string summary, string location, string description, DateTime startDateTime, DateTime endDateTime)
        {
            var service = await GetCalendarServiceAsync();

            Event newEvent = new Event()
            {
                Summary = summary,
                Location = location,
                Description = description,
                Start = new EventDateTime()
                {
                    DateTime = startDateTime,
                    TimeZone = _config.DefaultTimeZone,
                },
                End = new EventDateTime()
                {
                    DateTime = endDateTime,
                    TimeZone = _config.DefaultTimeZone,
                },
                Recurrence = new String[] { _config.Recurrence },
                Attendees = new EventAttendee[] { },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = _config.UseDefaultReminders,
                    Overrides = _config.DefaultReminders
                }
            };

            EventsResource.InsertRequest request = service.Events.Insert(newEvent, _config.CalendarId);
            Event createdEvent = await request.ExecuteAsync();
            return createdEvent.HtmlLink;
        }

        public async Task<bool> CancelEventAsync(string eventId)
        {
            var service = await GetCalendarServiceAsync();
            try
            {
                var request = service.Events.Delete(_config.CalendarId, eventId);
                await request.ExecuteAsync();
                return _config.CancelEventSuccess;
            }
            catch (Exception)
            {
                return _config.CancelEventFailure;
            }
        }

        public async Task<Events> GetAdvisorCalendarAsync(string advisorEmail)
        {
            var service = await GetCalendarServiceAsync();

            EventsResource.ListRequest request = service.Events.List(advisorEmail);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = _config.ShowDeleted;
            request.SingleEvents = _config.SingleEvents;
            request.OrderBy = (EventsResource.ListRequest.OrderByEnum)Enum.Parse(typeof(EventsResource.ListRequest.OrderByEnum), _config.OrderBy);

            return await request.ExecuteAsync();
        }

        public async Task<string> SetNotificationPreferencesAsync(string eventId, EventReminder[] reminders)
        {
            var service = await GetCalendarServiceAsync();

            Event eventToUpdate = await service.Events.Get(_config.CalendarId, eventId).ExecuteAsync();
            eventToUpdate.Reminders = new Event.RemindersData()
            {
                UseDefault = false,
                Overrides = reminders,
            };

            EventsResource.UpdateRequest updateRequest = service.Events.Update(eventToUpdate, _config.CalendarId, eventId);
            Event updatedEvent = await updateRequest.ExecuteAsync();
            return updatedEvent.HtmlLink;
        }

        public async Task<string> UpdateEventAsync(string eventId, string summary, string location, string description, DateTime startDateTime, DateTime endDateTime)
        {
            var service = await GetCalendarServiceAsync();

            Event eventToUpdate = await service.Events.Get(_config.CalendarId, eventId).ExecuteAsync();

            eventToUpdate.Summary = summary;
            eventToUpdate.Location = location;
            eventToUpdate.Description = description;
            eventToUpdate.Start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = _config.DefaultTimeZone,
            };
            eventToUpdate.End = new EventDateTime()
            {
                DateTime = endDateTime,
                TimeZone = _config.DefaultTimeZone,
            };
            eventToUpdate.Reminders = new Event.RemindersData()
            {
                UseDefault = _config.UseDefaultReminders,
            };

            EventsResource.UpdateRequest updateRequest = service.Events.Update(eventToUpdate, _config.CalendarId, eventId);
            Event updatedEvent = await updateRequest.ExecuteAsync();
            return updatedEvent.HtmlLink;
        }

        private class Config
        {
            public string CredentialsPath { get; set; }
            public string TokenPath { get; set; }
            public string User { get; set; }
            public bool FileDataStore { get; set; }
            public string ApplicationName { get; set; }
            public string CalendarId { get; set; }
            public string DefaultTimeZone { get; set; }
            public string Recurrence { get; set; }
            public bool UseDefaultReminders { get; set; }
            public EventReminder[] DefaultReminders { get; set; }
            public bool ShowDeleted { get; set; }
            public bool SingleEvents { get; set; }
            public int MaxResults { get; set; }
            public string OrderBy { get; set; }
            public bool CancelEventSuccess { get; set; }
            public bool CancelEventFailure { get; set; }
        }
    }
}
