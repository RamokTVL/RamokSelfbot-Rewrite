using Discord.Commands;
using Discord.Gateway;
using System;

namespace RamokSelfbot.Commands.Raid
{
    [Command("deleteallroles", "Delete all roles in the guild - RAID")]
    class DeleteAllRoles : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
                foreach (var roles in Message.Guild.GetRoles())
                {
                    roles.DeleteAsync();
                    Console.WriteLine("Deleted role : " + roles.Name + " in " + guild.Name + " !");

                }
                RamokSelfbot.Utils.Print("Deleted all roles in : " + guild.Name + "!");
                //Message.Channel.SendMessage("Deleted all roles in : " + guild.Name + "!");
            }
        }
    }
}
