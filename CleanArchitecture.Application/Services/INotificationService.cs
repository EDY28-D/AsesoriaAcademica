using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.Logging;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly string _senderEmail = "junioredinsonmatiasbardales@gmail.com";
    private readonly string _senderPassword = "jdnzsysvwrcunyby";

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendNotificationAsync(string message, string recipient)
    {
        try
        {
            string subject = "Notificación de Cita";
            MailMessage mailMessage = new MailMessage(_senderEmail, recipient, subject, message);
            mailMessage.IsBodyHtml = false;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);
            _logger.LogInformation("Correo electrónico enviado exitosamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al enviar el correo electrónico: {0}", ex.Message);
        }
    }

    public async Task SendAppointmentCreatedNotificationAsync(Guid appointmentId, DateTime appointmentDateTime, string recipient)
    {
        string message = $"Estimado usuario,\n\n" +
                         $"Se ha creado su cita satisfactoriamente con ID: {appointmentId}.\n" +
                         $"Fecha y Hora: {appointmentDateTime:dd/MM/yyyy HH:mm}\n\n" +
                         "Saludos cordiales,\n" +
                         "Su Equipo de Soporte";
        await SendNotificationAsync(message, recipient);
    }

    public async Task SendAppointmentUpdatedNotificationAsync(Guid appointmentId, DateTime appointmentDateTime, string recipient)
    {
        string message = $"Estimado usuario,\n\n" +
                         $"Su cita con ID: {appointmentId} ha sido actualizada.\n" +
                         $"Nueva Fecha y Hora: {appointmentDateTime:dd/MM/yyyy HH:mm}\n\n" +
                         "Saludos cordiales,\n" +
                         "Su Equipo de Soporte";
        await SendNotificationAsync(message, recipient);
    }

    public async Task SendAppointmentDeletedNotificationAsync(Guid appointmentId, string recipient)
    {
        string message = $"Estimado usuario,\n\n" +
                         $"Su cita con ID: {appointmentId} ha sido eliminada.\n\n" +

                         "Saludos cordiales,\n" +
                         "Su Equipo de Soporte";
        await SendNotificationAsync(message, recipient);
    }
}
