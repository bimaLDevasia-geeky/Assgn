using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem
{
    internal class AppointmentServices
    {
        private readonly INotificationService _notificationService;

      
        public AppointmentServices(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

       
        public void BookAppointment()
        {
            
            Console.WriteLine($"\nBooking appointment...");
            Console.WriteLine("...Appointment booked successfully.");

            
            string message = $"Your appointment is confirmed. See you then!";

            
            _notificationService.Notify(message);
        }
    }
}
