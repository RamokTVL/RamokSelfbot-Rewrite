
using Discord.Commands;
using Discord.Gateway;

using System;


namespace RamokSelfbot.Commands.StatusActivity
{
    [Command("watching", "Set your status to \"Watching\" - ACTIVITY")]
    class Watching : CommandBase
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
                        Type = ActivityType.Watching
                    });

                  
                    
                } catch(Exception ex)
                {
                    if(Program.Debug)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


            }
        }
    }
}
