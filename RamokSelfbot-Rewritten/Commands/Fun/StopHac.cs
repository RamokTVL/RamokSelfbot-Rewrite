using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("stophac", "Stop the hac command - FUN")]
    class StopHac : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Hac.hak = false;
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Title = "<@" + Message.Author.User.Id + "> is no longer a hacker !",
                    Description = "Not stonks :("
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
