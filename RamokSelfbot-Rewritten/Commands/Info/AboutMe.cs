using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Info
{
    [Command("aboutme", "Show informations about the current version of the selfbot. - INFO")]
    class AboutMe : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Description = "Join our discord : https://discord.gg/GxvKJ5kgPR",
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Title = "About Me"
                };

                string version = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;

                embed.AddField("Name", "```\nRamokSelfbot```", true);
                embed.AddField("Version", "```\n" + version + "```", true);
                embed.AddField("Number of commands", "```\n" + Client.CommandHandler.Commands.Count.ToString() + "```", true);
                embed.AddField("Description", "```The selfbot made for you.\nIf you need a selfbot, get our selfbot.```", false);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
