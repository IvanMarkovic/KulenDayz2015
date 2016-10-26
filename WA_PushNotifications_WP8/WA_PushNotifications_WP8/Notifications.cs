using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Microsoft.WindowsAzure.Messaging;
using Windows.Storage;

namespace KDPushNotification_WP
{
    public class Notifications
    {
        private NotificationHub hub;

        public Notifications()
        {
            hub = new NotificationHub("kdnotificationhub", "Endpoint=sb://kdnotificationhub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Yv7Xpeovd/Suldrl3KBWy7sLk2fzbgOqfSF8DDltuG4=");
        }

        public IEnumerable<string> RetrieveCategories()
        {
            var categories = (string)ApplicationData.Current.LocalSettings.Values["categories"];
            return categories != null ? categories.Split(',') : new string[0];              
        }
        public string RetrieveLocale()
        {
            var locale = (string)ApplicationData.Current.LocalSettings.Values["locale"];
            return locale != null ? locale : "English";
        }
    }
}
