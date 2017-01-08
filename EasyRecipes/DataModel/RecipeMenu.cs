using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRecipes.DataModel
{
    public class RecipeMenu
    {
        public string Icon
        {
            get { return "ms-appx:///Assets/DataImages/Menu/" + Title + ".png"; }
        }
        public string MenuType { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
    }
}
