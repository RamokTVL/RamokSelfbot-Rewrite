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


                if(HasPerm(DiscordPermission.Administrator, roles))
                {
                    perms += HasPermission(DiscordPermission.Administrator, roles, "👑 Administrator (all permissions)");
                }
                else
                {
                    perms = "```\n";

                    perms += HasPermission(DiscordPermission.CreateInstantInvite, roles, "Create Invite");
                    perms += HasPermission(DiscordPermission.SendMessages, roles, "Send Message");
                    perms += HasPermission(DiscordPermission.ReadMessageHistory, roles, "Read Message History");
                    perms += HasPermission(DiscordPermission.UseExternalEmojis, roles, "Use External Emojis");
                    perms += HasPermission(DiscordPermission.ConnectToVC, roles, "Connect");
                    perms += HasPermission(DiscordPermission.SpeakInVC, roles, "Speak");
                    perms += HasPermission(DiscordPermission.MuteMembers, roles, "Mute members");
                    perms += HasPermission(DiscordPermission.DeafenVCMembers, roles, "Deafen members");
                    perms += HasPermission(DiscordPermission.MoveVCMembers, roles, "Move members");
                    perms += HasPermission(DiscordPermission.PrioritySpeaker, roles, "Priority Speaker");
                    
                }
                perms += "```";
                embed.AddField("Permissions", perms);

                embed.AddField("Role position", "```\n" + roles.Position.ToString() + "```", false);

                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }


        private string HasPermission(DiscordPermission permission, DiscordRole role, string NamePerm)
        {
            if(role.Permissions.Has(permission))
            {
                return "✅ " + NamePerm + "\n";
            }
            else
            {
                return "❌ " + NamePerm + "\n";
            }
        }  
        private bool HasPerm(DiscordPermission permission, DiscordRole role)
        {
            if(role.Permissions.Has(permission))
            {
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
