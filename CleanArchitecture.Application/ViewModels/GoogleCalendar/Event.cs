using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.GoogleCalendar;

public class Event
{
    public string Id { get; set; }
    public string Summary { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}