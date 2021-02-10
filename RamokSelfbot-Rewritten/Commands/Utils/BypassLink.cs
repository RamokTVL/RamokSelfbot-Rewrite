using Discord.Commands;
using System;
using Discord;
using System.Net;

namespace RamokSelfbot.Commands.Utils
{
    [Command("bypasslink", "Bypass a shorten link like bit.ly / linkvertise - UTILS")]
    class BypassLink : CommandBase
    {

        [Parameter("method", false)]
        public string method { get; set; }

        [Parameter("link", true)]
        public string link { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "Service created by IDRALOU#6966 (666755948244893716)"
                };
                embed.Footer.Text += "\nPowered by bypass-shorteners.herokuapp.com";

                switch(method.ToLower())
                {
                    case "help":
                        embed.AddField("Methods", "```bypass``` Bypass a link like linkvertise, adf.ly etc\n```unshort``` Unshort a link like bit.ly and any 301/302 redirection !");
                        break;
                    default:
                        embed.AddField("Method", method, true);
                        embed.AddField("Original Link", link, true);
                        embed.AddField("Bypassed Link", new WebClient().DownloadString("https://bypass-shorteners.herokuapp.com/" + method + "?url=" + link), true);
                        break;
                }
                RamokSelfbot.Utils.SendEmbed(Message, embed);



            }
        }
    }
}
