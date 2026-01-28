// See https://aka.ms/new-console-template for more information
using NotificationSystem;

Console.WriteLine("--- Appointment Notification System Demo ---");

INotificationService smsService = new SMSNotifier();

AppointmentServices smsAppointmentService = new AppointmentServices(smsService);

smsAppointmentService.BookAppointment();

INotificationService emailService = new EmailNotifier();

AppointmentServices emailAppointmentService = new AppointmentServices(emailService);


emailAppointmentService.BookAppointment();

AppointmentServices pushAppointmentService = new AppointmentServices(new PushNotifier());
pushAppointmentService.BookAppointment();