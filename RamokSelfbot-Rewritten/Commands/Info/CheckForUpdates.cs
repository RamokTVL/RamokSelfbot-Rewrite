using System;
using System.Diagnostics;
using Discord;
using Discord.Commands;
using Leaf.xNet;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.Info
{
    [Command("checkforupdates", "Check if the bot is up to date - INFO")]
    class CheckForUpdates : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(RamokSelfbot.Utils.GetFileName() + "\\RamokSelfbot-Rewritten.exe");
                string version = versionInfo.FileVersion;
              

                HttpRequest request = new HttpRequest();
                request.AddHeader("ramokselfbot", "ramoklebg");
                string res = request.Get("https://ramok.herokuapp.com/api/checkforupdates?clientver=" + version.Replace(".", "")).ToString();


                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                };

                embed.Description = res;

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
