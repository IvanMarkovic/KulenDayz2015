using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.ServiceBus.Notifications;



namespace KDConsoleBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            SendNotificationAsync();
            Console.ReadLine();
        }

        private static async void SendNotificationAsync()
        {
            NotificationHubClient hub = NotificationHubClient
                .CreateClientFromConnectionString("Endpoint=sb://kdnotificationhub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Yv7Xpeovd/Suldrl3KBWy7sLk2fzbgOqfSF8DDltuG4=", "kdnotificationhub");
            
            String message = "{\"data\":{\"message\":\"Hello Chrome from Azure Notification Hubs\"}}";

            await hub.SendGcmNativeNotificationAsync(message);
        }
    }


}
