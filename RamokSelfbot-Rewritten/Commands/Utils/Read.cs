using Discord.Commands;
using Discord;
using System;

namespace RamokSelfbot.Commands.Utils
{
    [Command("read", "Read all messages of ur account - UTILS")]
    class Read : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                int i = 0;
                int guildscount = Client.GetGuilds().Count;
                foreach(var guilds in Client.GetGuilds())
                {
                    
                    try
                    {
                        i++;
                        guilds.AcknowledgeMessagesAsync();
                        RamokSelfbot.Utils.Print("Readed " + i + "/" + guildscount + " guilds !");
                    } catch(Exception ex)
                    {
                        if(Program.Debug)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        RamokSelfbot.Utils.Print("Cannot read " + guilds.Name);
                    }

                }
            }
        }
    }
}
