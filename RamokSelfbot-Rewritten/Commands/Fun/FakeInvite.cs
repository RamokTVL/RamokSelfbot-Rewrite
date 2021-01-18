using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("fakeinvite", "Modify the message with an fake custom invite discord, but it works ! - FUN")]
    class FakeInvite : CommandBase
    {
        [Parameter("name")]
        public string name { get; set; }

        [Parameter("invitecodeoriginal")]
        public string invitecodeoriginal { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                if(invitecodeoriginal.Contains("discord.gg"))
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "syntax error please run this command like this : ```" + Client.CommandHandler.Prefix + "fakeinvite ramokselfbot tMJ4ScsfAS```"
                    });
                } else
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "discord.gg/" + name + "||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​||||​|| discord.gg/" + invitecodeoriginal
                    });
                }


            }
        }
    }
}
