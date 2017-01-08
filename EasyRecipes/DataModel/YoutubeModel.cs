using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRecipes.DataModel
{
    
    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }

    public class Id
    {
        public string kind { get; set; }
        public string videoId { get; set; }
        public string channelId { get; set; }
    }

    

    public class Url
    {
        public string url { get; set; }
    }

    public class Thumbnails
    {
        public Url @default { get; set; }
        public Url medium { get; set; }
        public Url high { get; set; }
    }

    public class Snippet
    {
        public string publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string channelTitle { get; set; }
        public string liveBroadcastContent { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public Id id { get; set; }
        public Snippet snippet { get; set; }
    }

    public class YoutubeSearchRoot
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<Item> items { get; set; }
    }
    public class YoutubeModel
    {
        public string EmbedHtmlFragment
        {
            get { return "https://www.youtube.com/embed/"+VideoId+"?autoplay=1"; }
        }
        public string ExternalUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Published { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string VideoId { get; set; }
        public string VideoUrl { get; set; }
    }
}
