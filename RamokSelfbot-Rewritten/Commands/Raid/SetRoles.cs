using Discord.Commands;
using Discord;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Raid
{
    [Command("setroles", "Delete all the roles deletable of the server and create ~250 roles w/ the name specified - RAID")]
    class SetRoles : CommandBase
    {
        [Parameter("name of the role")]
        public string name { get; set; }
        public override async void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);

                if (Message.Guild != null)
                {
                    if (Message.Author.Member.GetPermissions().Has(DiscordPermission.ManageRoles))
                    {
                        int ignore = 0;
                        foreach (var roles in guild.GetRoles())
                        {
                            try
                            {
                                await roles.DeleteAsync();
                                RamokSelfbot.Utils.Print("Deleted " + roles.Name);
                            }
                            catch
                            {
                                RamokSelfbot.Utils.Print("Cannot delete " + roles.Name + ", ignoring...");
                                ignore++;
                            }
                        }

                        for (int i = 0; i < 499 - ignore; i++)
                        {
                            await guild.CreateRoleAsync(new RoleProperties()
                            {
                                Name = name,
                                Mentionable = false,
                                Permissions = DiscordPermission.None,
                                Seperated = false
                            });

                            RamokSelfbot.Utils.Print("Created channel ! (" + i.ToString() + "/" + (250 - ignore) + ")");
                        }

                        RamokSelfbot.Utils.Print("Finished work !");
                    }
                    else
                    {
                        RamokSelfbot.Utils.Print("Not enough permissions");
                    }
                }
                else
                {
                    RamokSelfbot.Utils.Print("How can you create roles if you are not in a guild ?");
                }
            }
        }
    }
}
