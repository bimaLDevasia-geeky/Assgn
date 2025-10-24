using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem
{
    public interface INotificationService
    {
        void Notify(string message);
    }

    public class SMSNotifier : INotificationService
    {
        public void Notify(string message)
        {
            
            Console.WriteLine($"[SMS NOTIFICATION]: Sending to phone: '{message}'");
        }
    }


    public class EmailNotifier : INotificationService
    {
        public void Notify(string message)
        {
            
            Console.WriteLine($"[EMAIL NOTIFICATION]: Sending email with body: '{message}'");
        }
    }

    public class PushNotifier : INotificationService
    {
        public void Notify(string message)
        {
            
            Console.WriteLine($"[PUSH NOTIFICATION]: Pushing to device: '{message}'");
        }
    }


}
