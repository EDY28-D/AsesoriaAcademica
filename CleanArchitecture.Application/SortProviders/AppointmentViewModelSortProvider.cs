using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class AppointmentViewModelSortProvider : ISortingExpressionProvider<AppointmentViewModel, Appointment>
{
    private static readonly Dictionary<string, Expression<Func<Appointment, object>>> s_expressions = new()
    {
        { "Id", appointment => appointment.Id },
        { "ProfessorId", appointment => appointment.ProfessorId },
        { "StudentId", appointment => appointment.StudentId },
        { "CalendarId", appointment => appointment.CalendarId },
        { "DateTime", appointment => appointment.DateTime },
        { "ProfessorProgress", appointment => appointment.ProfessorProgress },
        { "StudentProgress", appointment => appointment.StudentProgress }
    };

    public Dictionary<string, Expression<Func<Appointment, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}
