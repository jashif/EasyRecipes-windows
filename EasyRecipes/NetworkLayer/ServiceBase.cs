
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Services namespace.
/// </summary>
namespace EasyRecipes
{
    /// <summary>
    /// Class ServiceBase.
    /// </summary>
    public class ServiceBase
    {



        public enum ServiceRequest
        {
            NEWLYADDED,
            VEGETARIAN,
            NONVEG,SNACKS,DESSERTS,BEVERAGES,SWEETS,
            SEARCH
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        public ServiceBase()
        {
            //TODO:
        }

        protected string GetUrl(ServiceRequest servicerequest)
        {
            string url = "";

            switch (servicerequest)
            {
                case ServiceRequest.NEWLYADDED:
                    url = "http://pachakam.com/?d=" + DateTime.Now.ToString("hhmmss");
                    break;
                case ServiceRequest.VEGETARIAN://http://pachakam.com/
                    url = "http://pachakam.com/Recipe/Vegetarian=1?d=" + DateTime.Now.ToString("hhmmss");
                    break;
                case ServiceRequest.NONVEG:
                    url = "http://pachakam.com/Recipe/Non-Vegetarian=2?d=" + DateTime.Now.ToString("hhmmss");
                    break;
                case ServiceRequest.SNACKS:
                    url = "http://pachakam.com/Recipe/Snacks=3?d=" + DateTime.Now.ToString("hhmmss");
                    break;
                case ServiceRequest.DESSERTS:
                    url = "http://pachakam.com/Recipe/Desserts=5?d=" + DateTime.Now.ToString("hhmmss");
                    break;
                case ServiceRequest.BEVERAGES:
                    url = "http://pachakam.com/Recipe/Beverages=6?d=" + DateTime.Now.ToString("hhmmss");
                    break;

                case ServiceRequest.SWEETS:
                    url = "http://pachakam.com/Recipe/Sweeets=4?d=" + DateTime.Now.ToString("hhmmss");
                    break;
                case ServiceRequest.SEARCH:
                    url = "http://pachakam.com/search-recipe/Recipe={0}";
                    break;
            }
                    return url;
            
        }



        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        protected virtual async Task<Response> GetResponse(string url, Dictionary<string, string> header = null)
        {
            NetworkAPIHandler req = new NetworkAPIHandler(url, null, 120);
            var response = await req.GetAsync(url);
            //if (response != null)
            //{
            //    Logger.LogException(url);
            //    Logger.LogException(response.StatusCode.ToString());
            //    Logger.LogException(response.Content);
            //}
            return response;
        }
    }
}
