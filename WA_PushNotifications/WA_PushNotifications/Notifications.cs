using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Microsoft.WindowsAzure.Messaging;
using Windows.Storage;

namespace KDPushNotification
{
    public class Notifications
    {
        private NotificationHub hub;

        public IEnumerable<string> RetrieveCategories()
        {
            var categories = (string)ApplicationData.Current.LocalSettings.Values["categories"];
            return categories != null ? categories.Split(',') : new string[0];
        }
    }
}
