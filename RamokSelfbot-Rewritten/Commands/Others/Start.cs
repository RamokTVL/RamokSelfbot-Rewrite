using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Others
{
    [Command("start", "Start the process specified. - OTHERS")]
    class Start : CommandBase
    {
        [Parameter("processname", true)]
        public string processname { get; set; }

        [Parameter("args", true)]
        public string args { get; set; }

        public override void Execute()
        {
           if(Message.Author.User.Id == Program.id)
            {
                new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = processname,
                        Arguments = args
                    }
                }.Start();
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
                        
                if (Message.Author.User.Id == Program.id)
                {
                    Console.WriteLine("error occured : " + exception.Message);
                }
            }
        }
    }
}
