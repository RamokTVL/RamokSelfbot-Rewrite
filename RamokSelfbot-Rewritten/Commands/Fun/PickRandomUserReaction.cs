using Discord.Commands;
using Discord.Gateway;
using Discord;
using Discord.Media;
using Discord.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("pickrandomusermessage", "Pick a random user in message reactions. - FUN")]
    class PickRandomUserReaction : CommandBase
    {
        [Parameter("channelid")]
        public ulong channelid { get; set; }

        [Parameter("msgid")]
        public ulong msgid { get; set; }   

        [Parameter("reaction")]
        public string reaction { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker errorembed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Title = "Error/.",
                    Description = "Please check the reaction or the others informations"
                };

                var message = Client.GetMessageReactions(channelid, msgid, reaction);
                Console.WriteLine(message.Count);
                if(message.Count == 0)
                {
                    RamokSelfbot.Utils.SendEmbed(Message, errorembed);
                    return;
                }

                DiscordUser winnneruser = message[new Random().Next(0, message.Count)];

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "<@" + winnneruser.Id + "> get picked !",
                    Title = "Results!"
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
