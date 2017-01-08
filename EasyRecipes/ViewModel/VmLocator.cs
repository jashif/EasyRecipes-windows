using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRecipes.ViewModel
{
    public class VmLocator
    {

        public SearchVM SearchVM
        {
            get
            {

                return new SearchVM();

            }
        }
        public MainVM MainVM
        {
            get
            {

                return new MainVM();

            }
        }
        public ReciepeDetailVM ReciepeDVM
        {
            get
            {

                return new ReciepeDetailVM();

            }
        }

        public MenuListVM MenuListVM
        {
            get
            {

                return new MenuListVM();

            }
        }

        public AboutThisAppVM AboutThisAppVM
        {
            get
            {

                return new AboutThisAppVM();

            }
        }
        public YoutubeVM YoutubeVM
        {
            get
            {

                return new YoutubeVM();

            }
        }
        
        
    }
}
