using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Gateway;
using Discord;


namespace RamokSelfbot.Commands.Raid
{
    [Command("renameall", "Rename all members in the server - RAID")]
    class RenameAll : CommandBase
    {
        [Parameter("name")]
        public string name { get; set; }

        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
                bool finished = false;
                while (finished == false)
                {
                    for (int i = 0; i < guild.MemberCount; i++)
                    {
                        var users = guild.GetMembers()[i];
                        if (users.User.Id != Program.id)
                        {
                            try
                            {
                                if (users.User.Id != Program.id)
                                {
                                    users.Modify(new GuildMemberProperties() { Nickname = name });
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Cant rename : " + users.User.Username + "#" + users.User.Discriminator);
                            }
                        }
                    }
                }
            }
        }
    }
}
