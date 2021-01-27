using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Music
{
    [Command("play", "Play music locally - MUSIC")]
    class Play : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {

            }
        }
    }
}
