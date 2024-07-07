using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAppointmentCreatedNotificationAsync(Guid appointmentId, DateTime dateTime, string recipientEmail);
        Task SendAppointmentDeletedNotificationAsync(Guid appointmentId, string recipientEmail);

        Task SendAppointmentUpdatedNotificationAsync(Guid appointmentId, DateTime dateTime, string recipientEmail);
        Task SendNotificationAsync(string message, string recipient);
    }

}
