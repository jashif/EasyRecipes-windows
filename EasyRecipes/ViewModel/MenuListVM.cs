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
    public class MenuListVM : ViewModelBase
    {
        private List<Recipe> recipes = new List<Recipe>();

        public List<Recipe> Recipes
        {
            get { return recipes; }
            set { recipes = value; RaisePropertyChanged("Recipes"); }
        }
        private RecipeMenu menu;

        public RecipeMenu Menu
        {
            get { return menu; }
            set { menu = value; RaisePropertyChanged("Menu"); }
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
        public void GetMenuRecipe(RecipeMenu menu)
        {
            string name = menu.Title;
            Menu = menu;
            if (name == "Non-Vegetarian") { name = "Nonveg"; }
            var en = (EasyRecipes.ServiceBase.ServiceRequest)Enum.Parse(typeof(EasyRecipes.ServiceBase.ServiceRequest), name.ToUpper());
            EasyRecipes.ReciepeAPI.Instance.GetReciepes((x) =>
            {
                RunSafeDispatcherThread(() =>
                {
                    Recipes = x;
                    IsBusy = false;
                });
            },en);
        }
    }
}
