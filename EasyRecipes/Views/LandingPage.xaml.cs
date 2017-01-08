using EasyRecipes.Common;
using EasyRecipes.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EasyRecipes.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : PageBase
    {
        public LandingPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            reviewfunction();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var vm = this.DataContext as MainVM;
            if (e.NavigationMode == NavigationMode.New)
            {
              
                vm.LoadInitialData();
            }

            vm.GetYoutube();

        }
        public async void reviewfunction()
        {

            //For windows phone 8.1 app or universal app use the following line of code
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;


            //set the app name
            string Appname = "easyrecipes";

            if (!settings.Values.ContainsKey("review"))
            {
                settings.Values.Add("review", 1);
                settings.Values.Add("rcheck", 0);
            }
            else
            {
                int no = Convert.ToInt32(settings.Values["review"]);
                int check = Convert.ToInt32(settings.Values["rcheck"]);
                no++;
                if ((no == 3 || no == 6 || no % 10 == 0) && check == 0)
                {
                    settings.Values["review"] = no;
                    MessageDialog mydial = new MessageDialog("Thank you for using this application.\nWould you like to give some time to rate and review this application to help us improve");
                    mydial.Title = Appname;
                    mydial.Commands.Add(new UICommand(
                        "Yes",
                        new UICommandInvokedHandler(this.CommandInvokedHandler_yesclick)));
                    mydial.Commands.Add(new UICommand(
                       "No",
                       new UICommandInvokedHandler(this.CommandInvokedHandler_noclick)));
                    await mydial.ShowAsync();

                }
                else
                {
                    settings.Values["review"] = no;
                }
            }
        }

        private void CommandInvokedHandler_noclick(IUICommand command)
        {

        }

        private async void CommandInvokedHandler_yesclick(IUICommand command)
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            settings.Values["rcheck"] = 1;

            //add your app id here
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + "4745755b-e958-463f-bc54-ecf7fdb5e0de"));
        }

    }
}
