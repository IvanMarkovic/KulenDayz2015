using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Notifications;


namespace SendToNotificationHub
{
    class Program
    {
        static void Main(string[] args)
        {
            
                SendNotificationsAsync();
                Console.ReadLine();
            

        }
        private static async void SendNotificationsAsync()
        {

            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://notificationhub-namespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=LUFP4WKE4y7TRR/r8BJpuWjOARzd2t8w2FGb7KYIpl8=", "notificationhub");
            await hub.SendGcmNativeNotificationAsync("{ \"data\" : {\"msg\":\"Hello from Azure!!!\"}}");
        }


    }
}
