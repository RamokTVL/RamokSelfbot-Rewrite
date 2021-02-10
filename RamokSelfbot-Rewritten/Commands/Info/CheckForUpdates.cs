using System;
using System.Diagnostics;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.Info
{
    [Command("checkforupdates", "Check if the bot is up to date - INFO")]
    class CheckForUpdates : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Details json = JsonConvert.DeserializeObject<Details>(new System.Net.WebClient().DownloadString("https://ramokselfbot.netlify.app/api/v1/ramokselfbot/details.json"));
                var versionInfo = FileVersionInfo.GetVersionInfo(RamokSelfbot.Utils.GetFileName() + "\\RamokSelfbot-Rewritten.exe");
                string version = versionInfo.FileVersion;
                bool updated = false;
                if (json.version == version)
                {
                    updated = true;
                }

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                };

                if(updated == false)
                {
                    embed.Description = "You are not up to date.\n\n```\nLatest update : " + json.version + "\nYour current file version : " + version + "\n\nUpdate link : " + json.link + "```";
                } else
                {
                    embed.Description = "You are up to date !";
                }

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
