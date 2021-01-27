using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("ownerservers", "Show the servers that u are owner - UTILS")]
    class OwnerServers : CommandBase
    {
        public override void Execute()
        {
            try
            {
                if (Message.Author.User.Id == Program.id)
                {
                    EmbedMaker embede = new EmbedMaker();
                    RamokSelfbot.Utils.Print("Please wait for the command..., if you have a bad connexion, the command will be longer");
                    embede.Title = "Admin guilds";
                    embede.Color = RamokSelfbot.Utils.EmbedColor();
                    embede.Footer = RamokSelfbot.Utils.footer(Message.Author.User);
                    embede.Description = "The list of servers that you are owner.";
                    int i = 0;
                    int guildcount = Client.GetGuilds().Count;
                    string guildnames = "";
                    foreach (var guilds in Client.GetGuilds())
                    {
                        i++;
                        if (Program.Debug)
                        {
                            Console.WriteLine(i + "/" + guildcount);
                        }
                        SocketGuild guild = Client.GetCachedGuild(guilds.Id);
                        if(guild.OwnerId == Program.id)
                        {
                            guildnames += guild.Name + "\n";
                        }
                    }
                    if (Program.Debug)
                    {
                        Console.WriteLine(i);
                    }
                    embede.AddField("Guilds", "```\n" + guildnames + "```");

                    RamokSelfbot.Utils.SendEmbed(Message, embede);


                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
