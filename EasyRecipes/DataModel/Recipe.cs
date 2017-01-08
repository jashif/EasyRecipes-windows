using EasyRecipes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRecipes.DataModel
{
    public class Recipe : PropertyBase
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string Summary { get; set; }
        public string DetailUrl { get; set; }
        private RecipeDetail detail;

        public RecipeDetail Detail
        {
            get { return detail; }
            set { detail = value; RaisePropertyChanged("Detail"); }
        }
    }
    public class RecipeDetail : PropertyBase
    {
        private List<string> ingredients; //{ get; set; }

        public List<string> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; RaisePropertyChanged("Ingredients"); }
        }
        private List<string> directions;// { get; set; }

        public List<string> Directions
        {
            get { return directions; }
            set { directions = value; RaisePropertyChanged("Directions"); }
        }

        private string authorImg;

        public string AuthorImg
        {
            get { return authorImg; }
            set { authorImg = value; RaisePropertyChanged("AuthorImg"); }
        }
        
    }
    
}
