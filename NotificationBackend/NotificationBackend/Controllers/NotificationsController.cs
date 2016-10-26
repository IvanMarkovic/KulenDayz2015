using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using SecurePush.Models;
using System.Threading.Tasks;
using System.Web;
using NotificationBackend.Models;


namespace NotificationBackend.Controllers
{
    [AllowAnonymous]
    public class NotificationsController : ApiController
    {
        [Route("api/Notifications")]

        public async Task<HttpResponseMessage> Post(string message = "")
        {

            var categories = new string[] { "DataBase", "ProjectManagement", "AdvancedDevelopment", 
            "ITPro", "Web", "OpenSession"};

            foreach (var category in categories)
            {
                var user = HttpContext.Current.User.Identity.Name;
                var userTag = category;

                var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" + user + " | " + message + " | " + userTag + @"</text></binding></visual></toast>";

                await Notifications.Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Notifications/Bold")]
        public async Task<HttpResponseMessage> PostWithBoldText(string message = "")
        {

            var categories = new string[] { "DataBase", "ProjectManagement", "AdvancedDevelopment", 
            "ITPro", "Web", "OpenSession"};

            foreach (var category in categories)
            {
                var user = HttpContext.Current.User.Identity.Name;
                var userTag = category;

                var toast = @"<toast><visual><binding template=""ToastText02""><text id=""1"">"
                    + user + " | " + message + " | " + userTag + @"</text><text id=""2"">" +
                    "Second text row" + "</text></binding></visual></toast>";

                await Notifications.Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Notifications/Picture")]
        public async Task<HttpResponseMessage> PostWithPicture(string message = "")
        {

            var categories = new string[] { "DataBase", "ProjectManagement", "AdvancedDevelopment", 
            "ITPro", "Web", "OpenSession"};

            foreach (var category in categories)
            {
                var user = HttpContext.Current.User.Identity.Name;
                var userTag = category;


                var toast = @"<toast><visual><binding template=""ToastImageAndText01""><image id=""1"" 
                                src=""http://flaticons.net/icons/Science%20and%20Technology/Bell.png"" 
                                alt=""image1""/><text id=""1"">" + user + " | " + message + " | "
                                 + userTag + @"</text></binding></visual></toast>";

                await Notifications.Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
