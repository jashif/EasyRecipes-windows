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
    public class ReciepeDetailVM : ViewModelBase
    {
        private Recipe recipe;

        public Recipe Recipe
        {
            get { return recipe; }
            set { recipe = value; RaisePropertyChanged("Recipe"); }
        }

        public ICommand FavCommand
        {
            get
            {
                return new RelayCommand(() => 
                {
                    var arry = Encoding.UTF8.GetBytes(recipe.DetailUrl);
                    SerializeAndSave(Convert.ToBase64String(arry, 0, arry.Length), recipe);

                });
            }
        }

        public void GetReciepeDetail()
        {
            ReciepeAPI.Instance.GetReciepeDetails(Recipe.DetailUrl, (x) => 
            {
                RunSafeDispatcherThread(() => 
                {
                    recipe.Detail = x; IsBusy = false;
                });
            });
        }
    }
}
