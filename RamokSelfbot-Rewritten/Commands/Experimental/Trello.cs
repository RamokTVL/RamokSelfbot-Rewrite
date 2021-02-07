using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Experimental
{
    [Command("trello", "This command open the trello in a Chromium tab - EXPERIMENTAL")]
    class Trello : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                if (JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).experimentalcommands == true)
                {
                    Message.Delete();
                    Process.Start("https://trello.com/b/LMua8bEa/ramokselfbot");
                }
                else
                {
                    Message.Delete();
                    RamokSelfbot.Utils.Print("This command is a experimental command, change the \"experimentalcommands\" to true in config.json to use them!");
                }
            }
        }


    }
}
