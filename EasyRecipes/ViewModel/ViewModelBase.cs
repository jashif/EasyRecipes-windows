using EasyRecipes.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

/// <summary>
/// The ViewModel namespace.
/// </summary>
namespace EasyRecipes.ViewModel
{

    /// <summary>
    /// Class ViewModelBase.
    /// </summary>
    public class ViewModelBase : PropertyBase
    {
        /// <summary>
        /// Gets or sets the UI action.
        /// </summary>
        /// <value>The UI action.</value>
        public Action<string, object> UIAction { get; set; }
        /// <summary>
        /// Invokes the UI action.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="value">The value.</param>
        public virtual void InvokeUIAction(string param, object value)
        {
          
            if (UIAction != null)
            {
                UIAction(param, value);
            }
        }

        private bool isBusy=true;

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; RaisePropertyChanged("IsBusy"); }
        }
        
        protected void SerializeAndSave(string key,object val)
        {
            try
            {
                Common.StorageHelper.AddKeyValue(key, Newtonsoft.Json.JsonConvert.SerializeObject(val)) ;
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
    }
    /// <summary>
    /// Class PropertyBase.
    /// </summary>
   
}
