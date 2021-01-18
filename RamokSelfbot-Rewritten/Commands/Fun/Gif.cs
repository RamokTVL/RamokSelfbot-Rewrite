using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Discord;

namespace RamokSelfbot.Commands.Fun
{
    [Command("gif", "Search and send a gif - FUN")]
    class Gif : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Description = "This is a experimental command lol"
                };

                RamokSelfbot.Utils.Print("Comming soon.. (the code is not in source code, only on Ramok's PC lol)");
                return;
            }
        }
    }

    public class Images
    {
        public Original original { get; set; }
    }

    public class Original
    {
        public string height { get; set; }
        public string width { get; set; }
        public string size { get; set; }
        public string url { get; set; }
        public string mp4_size { get; set; }
        public string mp4 { get; set; }
        public string webp_size { get; set; }
        public string webp { get; set; }
        public string frames { get; set; }
        public string hash { get; set; }
    }
}
