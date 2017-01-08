using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using EasyRecipes.DataModel;
using EasyRecipes.Views;
using AppStudio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;

namespace EasyRecipes.ViewModel
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            Actions = new List<ActionInfo>();
            Actions.Add(new ActionInfo
                  {
                      Name = "AboutButton",
                      Style = ActionKnownStyles.About,
                      ActionType = ActionType.Secondary,
                      Command = new RelayCommand(GotoAbout)
                  });

            Actions.Add(new ActionInfo
             {
                 Name = "PrivacyButton",
                 Style = ActionKnownStyles.Privacy,
                 ActionType = ActionType.Secondary,
                 Command = new RelayCommand(OpenPrivacy)
             });

            Actions.Add(new ActionInfo
            {
                Name = "PrivacyButton",
                Style = ActionKnownStyles.Privacy,
                ActionType = ActionType.Secondary,
                Command = new RelayCommand(RateMe)
            });
            Actions.Add(new ActionInfo
            {
                Name = "Search",
                Style = ActionKnownStyles.Privacy,
                ActionType = ActionType.Secondary,
                Command = new RelayCommand(Search)
            });
            Actions.Add(new ActionInfo
            {
                Name = "Donate",
                Style = ActionKnownStyles.Privacy,
                ActionType = ActionType.Secondary,
                Command = new RelayCommand(() =>
                {
                    Launcher.LaunchUriAsync(new Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=jashif@live.com&item_name=Help for creating windows phone application&item_number=Windows phone development support&amount=2&currency_code=USD"));
                })
            });
        }

        private List<ActionInfo> actions;

        public List<ActionInfo> Actions
        {
            get { return actions; }
            set { actions = value; RaisePropertyChanged("Actions"); }
        }

         private List<YoutubeModel> youtubeVideos = new List<YoutubeModel>();

         public List<YoutubeModel> YoutubeVideos
         {
             get { return youtubeVideos; }
             set { youtubeVideos = value; RaisePropertyChanged("YoutubeVideos"); }
         }

        private List<Recipe> recipes = new List<Recipe>();

        public List<Recipe> Recipes
        {
            get { return recipes; }
            set { recipes = value; RaisePropertyChanged("Recipes"); }
        }
        private List<RecipeMenu> categories = new List<RecipeMenu>();

        public List<RecipeMenu> Categories
        {
            get { return categories; }
            set { categories = value; RaisePropertyChanged("Categories"); }
        }

        private bool showorHideMenu = false;

        public bool ShoworHideMenu
        {
            get { return showorHideMenu; }
            set { showorHideMenu = value; RaisePropertyChanged("ShoworHideMenu"); }
        }

        public ICommand ItemClickCommand
        {
            get
            {
                return new RelayCommand<object>((x) =>
                {
                    if (x is Recipe)
                    {
                        var rec = x as DataModel.Recipe;
                        Navigate(typeof(Views.ReciepeDetail), rec);
                    }
                    if(x is RecipeMenu)
                    {
                        var rec = x as DataModel.RecipeMenu;
                        Navigate(typeof(Views.MenuListPage), rec);
                        
                    }
                    if (x is YoutubeModel)
                    {
                        var rec = x as DataModel.YoutubeModel;
                        Navigate(typeof(Views.YoutubeDetail), rec);
                    }
                });
            }
        }

        public DateTime? LastUpdated
        {
            get
            ;
            set;
        }


        public void GotoAbout()
        {
            Navigate(typeof(AbouthisApp), null);
        }
        public void OpenPrivacy()
        {
            NavigationService.NavigateTo(new Uri("http://appstudio.windows.com/home/appprivacyterms")).RunAndForget();
            //  Navigate(typeof(AbouthisApp), null);//http://appstudio.windows.com/home/appprivacyterms
        }
        public void RateMe()
        {
            Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + "4745755b-e958-463f-bc54-ecf7fdb5e0de"));
        }
        public void Search()
        {
            Navigate(typeof(SearchPage), null);
        }
        public void ShowOrHide()
        {
            ShoworHideMenu = !showorHideMenu;
        }
        public void LoadInitialData()
        {
            GeCategories();
            GetNewlyAdded();
          
        }
        public void ShowFlipkartOffers()
        {

        }
        public void GetNewlyAdded()
        {
            
            EasyRecipes.ReciepeAPI.Instance.GetReciepes((x) =>
            {
                RunSafeDispatcherThread(() =>
                {
                    Recipes = x;
                    IsBusy = false;
                    LastUpdated = DateTime.Now;
                });
            });
         
        }
        public void GetYoutube()
        {
            EasyRecipes.ReciepeAPI.Instance.GetYoutubeVideos((x) =>
            {
                RunSafeDispatcherThread(() =>
                {
                    YoutubeVideos = x;
                    IsBusy = false;
                });
            });
            
        }
        private async void Refresh()
        {
            LoadInitialData();
        }

        public void GeCategories()
        {
            EasyRecipes.ReciepeAPI.Instance.GetCategories((x) =>
            {
                Categories = x;
            });
        }
    }
}
