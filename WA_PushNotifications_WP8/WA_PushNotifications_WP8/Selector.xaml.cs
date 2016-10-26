using KDPushNotification_WP.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.Storage;
using System.Net.Http.Headers;
using Windows.Networking.PushNotifications;
using Windows.UI.Popups;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace KDPushNotification_WP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Selector : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Selector()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        

       
        #endregion

        

        private async void Push_Click(object sender, RoutedEventArgs e)
        {
            var POST_URL = "http://localhost:55682/api/notifications";

            using (var httpClient = new HttpClient())
            {
                var settings = ApplicationData.Current.LocalSettings.Values;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", (string)settings["AuthenticationToken"]);

                await httpClient.PostAsync(POST_URL, new StringContent(""));
            }
        }

        private async void Subscribe_Click(object sender, RoutedEventArgs e)
        {

            var categories = new HashSet<string>();
            if (DataBase.IsChecked == true) categories.Add("DataBase");
            if (ProjectManagement.IsChecked == true) categories.Add("ProjectManagement");
            if (AdvancedDevelopment.IsChecked == true) categories.Add("AdvancedDevelopment");
            if (ITPro.IsChecked == true) categories.Add("ITPro");
            if (Web.IsChecked == true) categories.Add("Web");
            if (OpenSession.IsChecked == true) categories.Add("OpenSession");

           
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            var stringResponse = await new RegisterClient().RegisterAsync(channel.Uri, categories);


            if (stringResponse == "Accepted")
            {
                string categoriesForMessageDialog = "";
                foreach (var category in categories)
                {
                    categoriesForMessageDialog = categoriesForMessageDialog + " " + category;
                }
                var dialog = new MessageDialog("Subscribed to" + categoriesForMessageDialog);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("You are not subscribed!");
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var categories = ((App)Application.Current).notifications.RetrieveCategories();

            if (categories.Contains("DataBase")) DataBase.IsEnabled = true;
            if (categories.Contains("ProjectManagement")) ProjectManagement.IsEnabled = true;
            if (categories.Contains("AdvancedDevelopment")) AdvancedDevelopment.IsEnabled = true;
            if (categories.Contains("ITPro")) ITPro.IsEnabled = true;
            if (categories.Contains("Web")) Web.IsEnabled = true;
            if (categories.Contains("OpenSession")) OpenSession.IsEnabled = true;

        }

        
        


        
    }
}
