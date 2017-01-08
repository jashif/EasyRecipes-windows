using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace EasyRecipes.Common
{
    public class PropertyBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyname">The propertyname.</param>
        protected void RaisePropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }


        }
        public void Navigate(Type type, object parameter)
        {
            (Window.Current.Content as Windows.UI.Xaml.Controls.Frame).Navigate(type, parameter);
        }
        protected async void RunSafeDispatcherThread(Action action)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                 new Windows.UI.Core.DispatchedHandler(action));
        }
    }
}
