using EasyRecipes.DataModel;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Html;

namespace EasyRecipes
{
    public class ReciepeAPI : ServiceBase
    {
        private static ReciepeAPI instance;
        public static ReciepeAPI Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReciepeAPI();
                return instance;
            }
        }

        public void Search(Action<List<Recipe>> callback,string keyword)
        {
            List<Recipe> reciepes = null;
            Task.Run(async () =>
            {
                try
                {
                    string requrl = String.Format( GetUrl(ServiceRequest.SEARCH),keyword);
                    var response = await GetResponse(requrl);
                    if (response != null)
                    {
                        reciepes = new List<Recipe>();
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(response.Content);
                        IEnumerable<HtmlNode> nodes = htmlDocument.DocumentNode.Descendants()
         .Where(x => x.Name == "div" && x.Attributes.Contains("class")
         && x.Attributes["class"].Value.Split().Contains("recipe-post"));
                        foreach (HtmlNode node in nodes)
                        {
                            Recipe rc = new Recipe();
                            rc.ImageUrl = node.Descendants().FirstOrDefault(x => x.Name == "img").Attributes["src"].Value;
                            rc.Name = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div" && x.Attributes["class"].Value == "rp-title").InnerText);
                            rc.Summary = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div"
                                && x.Attributes["class"].Value == "rp-content").InnerText);
                            rc.Author = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div"
                                && x.Attributes["class"].Value == "rp-auth-name pull-left").InnerText);
                            rc.Duration = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div"
                                && x.Attributes["class"].Value == "rp-time pull-left").InnerText);
                            rc.Rating = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "a" && x.Attributes.Contains("class")
                               && x.Attributes["class"].Value == "list_like recipeLike ").InnerText);
                            rc.DetailUrl = "http://pachakam.com" + HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "a" && x.Attributes.Contains("href")).Attributes["href"].Value);
                            //&& !string.IsNullOrEmpty(x.Attributes["href"].Value)).Attributes["href"].Value);

                            reciepes.Add(rc);
                        }
                        if (callback != null) { callback(reciepes); }
                    }
                    else
                    {
                        if (callback != null) { callback(null); }
                    }


                }
                catch (Exception)
                {

                }
            });

        }

        public void GetReciepes(Action<List<Recipe>> callback, EasyRecipes.ServiceBase.ServiceRequest re = ServiceRequest.NEWLYADDED )
        {
            List<Recipe> reciepes = null;
            Task.Run(async () =>
            {
                try
                {
                    string requrl = GetUrl(re);
                    var response = await GetResponse(requrl);
                    if (response != null)
                    {
                        reciepes = new List<Recipe>();
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(response.Content);
                        IEnumerable<HtmlNode> nodes = htmlDocument.DocumentNode.Descendants()
         .Where(x => x.Name == "div" && x.Attributes.Contains("class")
         && x.Attributes["class"].Value.Split().Contains("recipe-post"));
                        foreach (HtmlNode node in nodes)
                        {
                            Recipe rc = new Recipe();
                            rc.ImageUrl = node.Descendants().FirstOrDefault(x => x.Name == "img").Attributes["src"].Value;
                            rc.Name = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div" && x.Attributes["class"].Value == "rp-title").InnerText);
                            rc.Summary = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div"
                                && x.Attributes["class"].Value == "rp-content").InnerText);
                            rc.Author = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div"
                                && x.Attributes["class"].Value == "rp-auth-name pull-left").InnerText);
                            rc.Duration = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "div"
                                && x.Attributes["class"].Value == "rp-time pull-left").InnerText);
                            rc.Rating = HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "a" && x.Attributes.Contains("class")
                               && x.Attributes["class"].Value == "list_like recipeLike ").InnerText);
                            rc.DetailUrl = "http://pachakam.com" + HtmlUtilities.ConvertToText(node.Descendants().FirstOrDefault(x => x.Name == "a" && x.Attributes.Contains("href")).Attributes["href"].Value);
                            //&& !string.IsNullOrEmpty(x.Attributes["href"].Value)).Attributes["href"].Value);

                            reciepes.Add(rc);
                        }
                        if (callback != null) { callback(reciepes); }
                    }
                    else
                    {
                        if (callback != null) { callback(null); }
                    }


                }
                catch (Exception)
                {

                }
            });

        }

        public void GetReciepeDetails(string url, Action<RecipeDetail> callback)
        {
            Task.Run(async () =>
               {
                   RecipeDetail dt = new RecipeDetail();
                   string requrl = url;
                   var response = await GetResponse(requrl);
                   if (response != null)
                   {
                       try
                       {
                           HtmlDocument htmlDocument = new HtmlDocument();
                           htmlDocument.LoadHtml(response.Content);
                           //  document.getElementById('recipe-content-wrapper')
                           var authimage = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "img" && x.Attributes.Contains("class") && x.Attributes["class"].Value == "author-image").FirstOrDefault().Attributes["src"].Value;
                           dt.AuthorImg = authimage;
                           var ingdf = (from n in htmlDocument.DocumentNode.Descendants()
                                        where n.Name == "div" && n.Id != null && (n.Id == "ingredients" || n.Id == "instructions")
                                        select n).ToList();
                           // nodes.Descendants().FirstOrDefault(x=> x.Name == "div"&& x.Attributes.Contains("Id") &&  x.Attributes["Id"].Value.Split().Contains("ingredients"));

                           var ingredients = (from ing in ingdf.FirstOrDefault(y => y.Id == "ingredients").Descendants().Where(x => x.Name == "div" && x.Attributes.Contains("class") && x.Attributes["class"].Value == ("ingredient-wrap"))
                                              select ParseIng(HtmlUtilities.ConvertToText(ing.InnerText))).ToList();

                           var directions = (from ing in ingdf.FirstOrDefault(y => y.Id == "instructions").Descendants().Where(x => x.Name == "div" && x.Attributes.Contains("class") && x.Attributes["class"].Value == ("instructions-wrap"))
                                             select HtmlUtilities.ConvertToText(ing.InnerText)).ToList();
                           if (ingredients != null)
                           {
                               dt.Ingredients = ingredients;
                           }
                           if (directions != null)
                           {
                               dt.Directions = directions;
                           }
                       }
                       catch (Exception)
                       {

                           // throw;
                       }
                       if (callback != null) { callback(dt); }
                       //<div id="ingredients" class="col-md-4 columns">
                       //<h3>Ingredients</h3><div class="ingredient-wrap"><div class="ingredient">chicken 1/2kg</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">onion 2</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">tomato 1</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">green chilli 1</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">ginger garlic paste 5 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">mint leaves 1/2 cup</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">red chilli powder 1 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">black pepper 1/4 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">turmeric powder 1 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">salt to taste</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">coriander powder 2 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">chicken masala 1 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">cumin seed powder 1/2 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">coconut paste 1/2 cup</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">oil 6 tsp</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">bay leaf 2</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">cinnamon 1</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">cloves 2</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">aniseed 1</div><div class="clr"></div></div><div class="ingredient-wrap"><div class="ingredient">cardamom 2</div><div class="clr"></div></div></div>)
                   }
                   else
                   {
                       if (callback != null) { callback(null); }
                   }
               });

        }
        public string ParseIng(string txt)
        {

            var split = txt.Split('-');
            string fst = split[0];
            var idx = split[0].IndexOf("English");
            if (idx != -1)
            {
                fst = split[0].Substring(0, idx);

            }
            if (split.Length > 1)
                return fst + "-" + split[split.Length - 1];
            return txt;
        }

        public void GetCategories(Action<List<RecipeMenu>> callback)
        {
            List<RecipeMenu> cat = new List<RecipeMenu>();
            // cat.Add(new RecipeMenu() { Title = "Newly Added" });
            cat.Add(new RecipeMenu() { Title = "Vegetarian" });
            cat.Add(new RecipeMenu() { Title = "Non-Vegetarian" });//
            cat.Add(new RecipeMenu() { Title = "Snacks" });
            cat.Add(new RecipeMenu() { Title = "Desserts" });
            cat.Add(new RecipeMenu() { Title = "Beverages" });
            cat.Add(new RecipeMenu() { Title = "Sweets" });
            callback(cat);
        }
        Random rnd = new Random();
        public async void GetYoutubeVideos(Action<List<YoutubeModel>> callback)
        {
            string[] keyword = new string[5] { "easyrecipes", "indian simple recipes","best snacks recipes","best desserts recipes","best non-vegetarian recipes" };

            int d = rnd.Next(0, 4);
            string keywrd = keyword[d];

            Task.Run(async () =>
                  {
                      try
                      {
                          List<YoutubeModel> vds = new List<YoutubeModel>();
                          string url = "https://www.googleapis.com/youtube/v3/search?part=snippet&q=" + keywrd + "&key=AIzaSyAkgdpxDgH9PtvbiLxhT2i5TA71n-eAUrc&maxResults=50";
                          var response = await GetResponse(url);
                          if (response != null && response.IsSuccess)
                          {
                              var dat = Newtonsoft.Json.JsonConvert.DeserializeObject<YoutubeSearchRoot>(response.Content);
                              if (dat != null && dat.items != null)
                              {
                                  foreach (var item in dat.items)
                                  {
                                      YoutubeModel dt = new YoutubeModel();
                                      dt.Title = item.snippet.title;
                                      dt.VideoId = item.id.videoId;
                                      dt.Summary = item.snippet.description;
                                      dt.ImageUrl = item.snippet.thumbnails.high.url;
                                      vds.Add(dt); //dt.EmbedHtmlFragment=
                                  }
                              }
                              callback(vds);
                          }
                          else
                          {
                              if (callback != null) { callback(null); }
                          }
                      }
                      catch (Exception)
                      {
                          callback(null);

                      }
                  });
        }
    }
}
