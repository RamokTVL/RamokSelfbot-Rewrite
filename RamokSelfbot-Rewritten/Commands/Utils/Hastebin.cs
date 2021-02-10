using Discord;
using Discord.Commands;
using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamokSelfbot.Commands.Utils
{
    [Command("hastebin", "Create a hastebin with ur text - UTILS")]
    class Hastebin : CommandBase
    {
        [Parameter("content")]
        public string bodyg { get; set; }
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                HttpRequest request = new HttpRequest();

                var post = request.Post(hasteserver + "/documents", bodyg, "text/plain");

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "I use the haste server of powercord beacause the original is fucked..",
                    Title = "Hastebin generated.."
                };

                embed.AddField("Link", hasteserver + "/" + JsonConvert.DeserializeObject<HastebinJSON>(post.ToString()).key);

                RamokSelfbot.Utils.SendEmbed(Message, embed);

                
            }
        }

        public string hasteserver = "https://haste.powercord.dev";
    }

    class HastebinJSON
    {
        public string key { get; set; }
    }
}
