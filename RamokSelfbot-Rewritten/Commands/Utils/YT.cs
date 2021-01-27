using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("yt", "Get statistics about a youtube channel - UTILS")]
    class YT : CommandBase
    {
        [Parameter("channelid")]
        public string channelid { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                try
                {
                    WebClient client = new WebClient();
                    JSON configfile = JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json"));
                    string raw = client.DownloadString("https://www.googleapis.com/youtube/v3/channels?part=statistics&id=" + channelid + "&key=" + configfile.youtubeapikey);
                    
                    Root test = JsonConvert.DeserializeObject<Root>(raw);
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "This command will be improved later"
                    };

                    embed.AddField("👨 Subscriber Count", test.Items[0].Statistics.SubscriberCount, false);
                    embed.AddField("👓 View count", test.Items[0].Statistics.ViewCount, false);
                    embed.AddField("📹 Video count", test.Items[0].Statistics.VideoCount, false);

                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                } catch(Exception es)
                {
                    Console.WriteLine(es.Message);
                }
            }
        }
    }



    public class PageInfo
    {
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        [JsonProperty("resultsPerPage")]
        public int ResultsPerPage { get; set; }
    }

    public class Statistics
    {
        [JsonProperty("viewCount")]
        public string ViewCount { get; set; }

        [JsonProperty("subscriberCount")]
        public string SubscriberCount { get; set; }

        [JsonProperty("hiddenSubscriberCount")]
        public bool HiddenSubscriberCount { get; set; }

        [JsonProperty("videoCount")]
        public string VideoCount { get; set; }
    }

    public class Item
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }
    }

    public class Root
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }

        public IReadOnlyList<Item> Items { get; set; }
    }

}
