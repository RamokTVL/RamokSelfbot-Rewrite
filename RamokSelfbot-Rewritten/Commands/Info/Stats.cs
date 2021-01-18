using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.Commands;
using Discord;

namespace RamokSelfbot.Commands.Info
{
    [Command("stats", "Send you the stats of ur account since the selfbot is launched - INFO")]
    class Stats : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                };

                embed.AddField("⌚ Uptime", Program.time.Elapsed.Days + "days " + Program.time.Elapsed.Hours + "hours " + Program.time.Elapsed.Minutes + "minutes " + Program.time.Elapsed.Seconds + " secondes.", false);
                embed.AddField("🎟️ Message Sent", Program.MSGSent.ToString(), true);
                embed.AddField("🎫 Message Received", Program.MSGRecieved.ToString(), true);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
