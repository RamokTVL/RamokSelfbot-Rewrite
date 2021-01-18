using Discord.Commands;
using Discord.Gateway;
using System;
namespace RamokSelfbot.Commands.Raid
{
    [Command("deleteallchannels", "Delete all channels in the guild - RAID")]
    class DeleteAllChannels : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
                foreach (var channels in Message.Guild.GetChannels())
                {
                    channels.Delete();
                    RamokSelfbot.Utils.Print("Deleted channel : " + channels.Name + " in " + guild.Name + " !");
                }
                RamokSelfbot.Utils.Print("Deleted all channels in : " + guild.Name + " !");
            }
        }
    }
}
