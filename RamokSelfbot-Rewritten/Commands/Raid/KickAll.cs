using System;
using Discord;
using Discord.Commands;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Raid
{
    [Command("kickall", "Kick all the members of the server - RAID")]
    class KickAll : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);


                bool finished = false;
                while (finished == false)
                {
                    for (int i = 0; i < guild.MemberCount; i++)
                    {
                        var user = guild.GetMembers()[i];
                        if (user.User.Id != Program.id)
                        {
                            try
                            {
                                user.Kick();
                                RamokSelfbot.Utils.Print("Kicked " + user.User.Username + "#" + user.User.Discriminator);
                                if (i == guild.MemberCount)
                                {
                                    finished = true;
                                }
                            }
                            catch
                            {
                                RamokSelfbot.Utils.Print("Cant kick " + user.User.Username + "#" + user.User.Discriminator);
                            }
                        }
                    }
                }
            }
        }
    }
}
