using Discord.Commands;
using Discord;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("ghostping", "Ghost ping everyone lol - UTILS")]
    class GhostPing : CommandBase
    {
        [Parameter("amount", true)]
        public int amount { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                Message.Delete();
                int a = amount;
                if(a == 0)
                {
                    a = 1;
                }

                if(Message.Guild != null)
                {
                    if (Message.Author.Member.GetPermissions().Has(DiscordPermission.MentionEveryone))
                    {
                        for (int i = 0; i < a; i++)
                        {
                            try
                            {
                                var ulongmsg = SendMsgReturnId();
                                Client.DeleteMessage(Message.Channel.Id, ulongmsg);
                            }
                            catch { }
                        }
                    }
                } else
                {
                    RamokSelfbot.Utils.Print("You can ping everyone only in a guild !");
                }


            }
        }

        private ulong SendMsgReturnId()
        {
            return Client.SendMessage(Message.Channel.Id, "@everyone", false, null).Id;
        }
    }
}
