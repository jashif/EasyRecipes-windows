using AppStudio.Common.Commands;
using EasyRecipes.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyRecipes.ViewModel
{
   public class SearchVM:ViewModelBase
    {
       public SearchVM()
       {
           IsBusy = false;
       }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged("SearchText"); }
        }
        private List<Recipe> recipes = new List<Recipe>();

        public List<Recipe> Recipes
        {
            get { return recipes; }
            set { recipes = value; RaisePropertyChanged("Recipes"); }
        }
        public ICommand ItemClickCommand
        {
            get
            {
                return new RelayCommand<object>((x) =>
                {

                    var rec = x as DataModel.Recipe;
                    Navigate(typeof(Views.ReciepeDetail), rec);
                });
            }
        }
        public void Search()
        {
            IsBusy = true;
            EasyRecipes.ReciepeAPI.Instance.Search((x) =>
            {
                RunSafeDispatcherThread(() =>
                {
                    Recipes = x;
                    IsBusy = false;
                });
            }, SearchText);
        }
    }
}
