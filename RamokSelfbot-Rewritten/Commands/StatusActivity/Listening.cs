using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.StatusActivity
{
    [Command("listening", "Change ur status to \"Listeting\" - ACTIVITY")]
    class Listening : CommandBase
    {
        [Parameter("name", false)]
        public string name { get; set; }

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                    Message.Delete();
                    try
                    {

                        Client.SetActivity(new ActivityProperties()
                        {
                            Name = name,
                            Type = ActivityType.Listening
                        });



                    }
                    catch (Exception ex)
                    {
                        if (Program.Debug)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }


                }
            }
        }
    }

