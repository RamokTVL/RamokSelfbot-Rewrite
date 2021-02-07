using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("longstatus", "Apply you a long custom status - FUN")]
    class LongStatus : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                throw new NotImplementedException("Sorry, this command will be added later");
                    return;
                Client.User.ChangeSettings(new Discord.UserSettingsProperties()
                {
                    CustomStatus = new Discord.CustomStatus()
                    {
                        Text = @"a"
                    }
                });
                Client.User.Update();
            }
        }
    }
}
