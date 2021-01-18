using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Info
{
    [Command("roleinfo", "Get info on a role - INFO")]
    class RoleInfo : CommandBase
    {
        [Parameter("id", false)]
        public string id { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor()
                };

                
                ulong roleid;

                if(Message.MentionedRoles.Count == 1)
                {
                    roleid = Message.MentionedRoles[0];
                } else if(id.Length == 18)
                {
                    roleid = ulong.Parse(id);
                }
                else
                {
                    RamokSelfbot.Utils.Print("<@" + Message.Author.User.Id + ">, please mention only one role !");
                    return;
                }

                DiscordRole roles = Client.GetGuildRole(roleid);

                embed.Title = "Roles informations of " + roles.Name;
                embed.AddField("Name", "```\n" + roles.Name.ToString() + "```", true);
                embed.AddField("ID", "```\n" + roles.Id.ToString() + "```", true);
                string colorembed = "```\n" + roles.Color.R.ToString() + ", " + roles.Color.G.ToString() + ", " + roles.Color.B.ToString() + "(RGB)```";
                embed.AddField("Color", colorembed, true);
                embed.AddField("Mentionable ?", "```\n" + roles.Mentionable.ToString() + "```");

                perms = "```\n";
                plength = perms.Length;

                HasPermission(roleid, DiscordPermission.Administrator);
                if (perms.Length != plength)
                {
                    perms = "```\n👑 Administrator (all permissions)";
                }
                else
                {
                    HasPermission(roleid, DiscordPermission.AddReactions);
                    HasPermission(roleid, DiscordPermission.AttachFiles);
                    HasPermission(roleid, DiscordPermission.BanMembers);
                    HasPermission(roleid, DiscordPermission.ChangeNickname);
                    HasPermission(roleid, DiscordPermission.ConnectToVC);
                    HasPermission(roleid, DiscordPermission.CreateInstantInvite);
                    HasPermission(roleid, DiscordPermission.DeafenVCMembers);
                    HasPermission(roleid, DiscordPermission.EmbedLinks);
                    HasPermission(roleid, DiscordPermission.ForcePushToTalk);
                    HasPermission(roleid, DiscordPermission.KickMembers);
                    HasPermission(roleid, DiscordPermission.ManageChannels);
                    HasPermission(roleid, DiscordPermission.ManageEmojis);
                    HasPermission(roleid, DiscordPermission.ManageGuild);
                    HasPermission(roleid, DiscordPermission.ManageMessages);
                    HasPermission(roleid, DiscordPermission.ManageNicknames);
                    HasPermission(roleid, DiscordPermission.ManageRoles);
                    HasPermission(roleid, DiscordPermission.ManageWebhook);
                    HasPermission(roleid, DiscordPermission.MentionEveryone);
                    HasPermission(roleid, DiscordPermission.MoveVCMembers);
                    HasPermission(roleid, DiscordPermission.MuteMembers);
                    HasPermission(roleid, DiscordPermission.None);
                    HasPermission(roleid, DiscordPermission.PrioritySpeaker);
                    HasPermission(roleid, DiscordPermission.ReadMessageHistory);
                    HasPermission(roleid, DiscordPermission.SendMessages);
                    HasPermission(roleid, DiscordPermission.SendTtsMessages);
                    HasPermission(roleid, DiscordPermission.SpeakInVC);
                    HasPermission(roleid, DiscordPermission.Stream);
                    HasPermission(roleid, DiscordPermission.UseExternalEmojis);
                    HasPermission(roleid, DiscordPermission.ViewAuditLog);
                    HasPermission(roleid, DiscordPermission.ViewGuildInsights);
                    HasPermission(roleid, DiscordPermission.ViewChannel);
                }

                perms += "```";
                embed.AddField("Permissions", perms);

                embed.AddField("Role position", "```\n" + roles.Position.ToString() + "```", false);
            }
        }

        private bool HasPermission(ulong id, DiscordPermission permission)
        {
            if (Client.GetGuildRole(id).Permissions.Has(permission))
            {
                if (permission == DiscordPermission.ViewChannel)
                {
                    perms += " " + permission.ToString();
                }
                else
                {
                    if (perms.Length == plength)
                    {
                        perms += permission.ToString() + ",";
                    }
                    else
                    {
                        perms += " " + permission.ToString() + ",";
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int plength;
        public string perms;
    }
}
