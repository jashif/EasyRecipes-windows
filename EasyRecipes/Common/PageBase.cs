
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace EasyRecipes.Common
{
   public class PageBase:Page
    {
     
        public PageBase():base()
        {
           // Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            BackPress(e);
        }
        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            
        }
       public virtual void BackPress(Windows.Phone.UI.Input.BackPressedEventArgs e)
        {

        }
    }
}
