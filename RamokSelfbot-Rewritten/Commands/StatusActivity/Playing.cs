using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.StatusActivity
{
    [Command("playing", "Change ur status to \"playing\" - ACTIVITY")]
    class Playing : CommandBase
    {
        [Parameter("game", false)]
        public string game { get; set; }

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                    Message.Delete();
                    try
                    {


                    Client.SetActivity(new GameActivityProperties()
                    {
                        Name = game, 
                        Details = "RamokSelfbot on the flux",
                        Elapsed = Program.time.Elapsed,
                        State = "discord.gg/bPfPw5PASR"
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

