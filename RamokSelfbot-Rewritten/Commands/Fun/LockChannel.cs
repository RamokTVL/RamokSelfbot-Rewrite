using Discord.Commands;
using Discord;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("lockchannel", "Lock the channel forever - FUN")]
    class LockChannel : CommandBase
    {
        public override void Execute()
        {
          if(Message.Author.User.Id == Program.id)
            {
                Message.Delete();


                if(Message.Guild == null)
                {
                    Message.Channel.SendFile("ressources\\channellocker.jpeg", "", false);
                } else if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles)) {
                    Message.Channel.SendFile("ressources\\channellocker.jpeg", "", false);
                } else
                {
                    RamokSelfbot.Utils.Print("Cannot block the channel forever :(");
                }
            }
        }
    }
}
