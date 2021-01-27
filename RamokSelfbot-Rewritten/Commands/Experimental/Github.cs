using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                    new Thread(new ThreadStart(OpenForm)).Start();
                }
                else
                {
                    Message.Delete();
                    RamokSelfbot.Utils.Print("This command is a experimental command, change the \"experimentalcommands\" to true in config.json to use them!");
                }
            }
        }

        public static void OpenForm()
        {
            RamokSelfbot.Utils.Print("Just ignore the log from CEFSharp");
            new Forms.Github().ShowDialog();
        }
    }
}
