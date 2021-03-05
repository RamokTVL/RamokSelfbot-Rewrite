using System.Net;
using System.IO;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Experimental
{
    [Command("github", "This command open the github repository in a Chromium tab - EXPERIMENTAL")]
    class Github : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                if (JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).experimentalcommands == true)
                {
                    Message.Delete();
                    Process.Start("https://github.com/RamokTVL/RamokSelfbot-Rewritten");
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
