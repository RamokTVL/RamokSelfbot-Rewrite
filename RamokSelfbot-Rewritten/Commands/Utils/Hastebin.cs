using Discord;
using Discord.Commands;
using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamokSelfbot.Commands.Utils
{
    [Command("hastebin", "Create a hastebin with ur text (private api) - UTILS")]
    class Hastebin : CommandBase
    {
        [Parameter("content")]
        public string bodyg { get; set; }
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                HttpRequest request = new HttpRequest();

                var body = "{\n \"text\": " + bodyg + "\n}";

                request.AddHeader("apikey", JsonConvert.DeserializeObject<JSON>(File.ReadAllText(RamokSelfbot.Utils.GetFileName() + "\\config.json")).apikeyself);

                var post = request.Post("http://ramok.herokuapp.com/pastebin/create", body, "application/json");

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "Hey ! This is a private pastebin server",
                    Title = "Hastebin generated.."
                };

                embed.AddField("Link", "http://ramok.herokuapp.com/pastebin/" + JsonConvert.DeserializeObject<HastebinJSON>(post.ToString()).code);

                RamokSelfbot.Utils.SendEmbed(Message, embed);

                
            }
        }


    }

    class HastebinJSON
    {
        public string text { get; set; }
        public string created_at { get; set; }
        public string code { get; set; }
    }
}
