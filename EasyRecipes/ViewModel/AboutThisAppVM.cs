using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace EasyRecipes.ViewModel
{
    public class AboutThisAppVM: ViewModelBase
    {
        public string Publisher
        {
            get
            {
                return "Jashif | contact: jashif@live.com";
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            }
        }

        public string AboutText
        {
            get
            {
                return "The application fetches data from pachakam.com website,This app mainly focused on creating indian dishes which includes vegetarian,non-vegetarian,sweets,snacks,dessert and beverages.";
            }
        }
    }
}
