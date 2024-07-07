using System;

namespace CleanArchitecture.Api.Models
{
    public class EventRequest
    {
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
