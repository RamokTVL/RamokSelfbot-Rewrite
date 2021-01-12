using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.Others
{
    [Command("restart", "Restart the application. - OTHERS")]
    class Restart : CommandBase
    {

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
         /*       RestartJson restart = JsonConvert.DeserializeObject<RestartJson>(File.ReadAllText(@"ressources\restarted.ramokselfbot.exemple"));
                restart.msgid = Message.Id;
                restart.channelid = Message.Channel.Id;

                string output = JsonConvert.SerializeObject(restart, Formatting.Indented);

                File.WriteAllText("restarted.ramokselfbot", output);*/

                Message.Edit(new Discord.MessageEditProperties()
                {
                    Content = "Restarting.."
                });

                new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = "cmd.exe",
                        Arguments = "/C taskkill /f /im RamokSelfbot-Rewritten.exe&" + System.Windows.Forms.Application.ExecutablePath
                    }
                }.Start();
            }
        }
    }
}
