using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("tinyurl", "Short ur link - UTILS")]
    class Tinyurl : CommandBase
    {
        [Parameter("link")]
        public string link { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Title = "TinyURL Results !",
                    Description = "Your shorten link for " + link + " is " + new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + link) + " in " + sw.ElapsedMilliseconds.ToString() + " ms"
                };
                sw.Stop();
                if (Message.Author.User.Avatar.Url != null)
                {
                    embed.ThumbnailUrl = Message.Author.User.Avatar.Url;
                }

                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                embed.AddField("Original Link", link, true);
                embed.AddField("Shorten Link", new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + link), true);
                embed.AddField("Time elapsed", sw.ElapsedMilliseconds.ToString(), true);
                RamokSelfbot.Utils.SendEmbed(Message, embed);
                sw.Reset();
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if(Message.Author.User.Id == Program.id)
            {
                if(providedValue == null)
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "Please provide a link for this command."
                    });
                } else
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }

}
