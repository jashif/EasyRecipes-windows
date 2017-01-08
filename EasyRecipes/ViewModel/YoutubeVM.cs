using EasyRecipes.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRecipes.ViewModel
{
    public class YoutubeVM : ViewModelBase
    {
        public YoutubeVM()
        {

        }
        private YoutubeModel youtubeModel;

        public YoutubeModel YoutubeModel
        {
            get { return youtubeModel; }
            set { youtubeModel = value; RaisePropertyChanged("YoutubeModel"); }
        }
        
    }
}
