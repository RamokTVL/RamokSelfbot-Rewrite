using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("stickynote", "Make the message stil the latest message. - UTILS")]
    class StickyNotes : CommandBase
    {
        [Parameter("message")]
        public string message { get; set; }
        public bool stickynotesenabled = false;
        public EmbedMaker embed = new EmbedMaker();
        public ulong messageid;

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                if(stickynotesenabled)
                {
                    RamokSelfbot.Utils.Print("You can only put one stickynote by session.");
                    return;
                }
                embed.Color = RamokSelfbot.Utils.EmbedColor();
                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);
                embed.Description = message;

                stickynotesenabled = true;
                messageid = RamokSelfbot.Utils.SendEmbedRsendIdget(Message, embed);
                Client.OnMessageReceived += Client_OnMessageReceived;
            }
        }

        private void Client_OnMessageReceived(Discord.Gateway.DiscordSocketClient client, Discord.Gateway.MessageEventArgs args)
        {
            if (args.Message.Channel.Id != Message.Channel.Id)
            {
                return;
            } else
            {
                if (args.Message.Id != messageid)
                {
                    Client.DeleteMessage(Message.Channel.Id, messageid);
                    messageid = RamokSelfbot.Utils.SendEmbedRsendIdget(Message, embed);
                }
            }
        }

    }

    
}
