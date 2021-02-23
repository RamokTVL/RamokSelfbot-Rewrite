using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("perms", "Show you the perms of a user in the guild ! - UTILS")]
    class Perms : CommandBase
    {
        [Parameter("id")]
        public string id { get; set; }

        [Parameter("export? (will create a json file with the permissions of the user specified.)", true)]
        public string export { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                DiscordUser user = Client.GetUser(Program.id);
                if (id == null)
                {
                    RamokSelfbot.Utils.ValidUser(1, Message);
                    return;
                }
                else
                {
                    if (id.Length == 18)
                    {
                        try
                        {
                            user = Client.GetUser(ulong.Parse(id));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Unknown User"))
                            {
                                RamokSelfbot.Utils.ValidUser(3, Message);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else if (Message.Mentions.Count > 1 || Message.Mentions.Count == 1)
                    {
                        try
                        {
                            user = Client.GetUser(Message.Mentions[0].Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            if (ex.Message.Contains("Unknown User"))
                            {
                                RamokSelfbot.Utils.ValidUser(3, Message);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        RamokSelfbot.Utils.ValidUser(2, Message);
                        return;
                    }
                }


                Permsjson perms = JsonConvert.DeserializeObject<Permsjson>(File.ReadAllText(RamokSelfbot.Utils.GetFileName() + "\\ressources\\perms.exemple"));
                var permsuser = Client.GetGuildMember(Message.Guild.Id, user.Id);
                var permissionsofuser = permsuser.GetPermissions();
                perms.ADD_REACTIONS = HasPerms(DiscordPermission.AddReactions, permissionsofuser);
                perms.ADMINISTRATOR = HasPerms(DiscordPermission.Administrator, permissionsofuser);
                perms.ATTACH_FILES = HasPerms(DiscordPermission.AttachFiles, permissionsofuser);
                perms.BAN_MEMBERS = HasPerms(DiscordPermission.BanMembers, permissionsofuser);
                perms.CHANGE_NICKNAME = HasPerms(DiscordPermission.ChangeNickname, permissionsofuser);
                perms.CONNECT = HasPerms(DiscordPermission.ConnectToVC, permissionsofuser);
                perms.CREATE_INSTANT_INVITE = HasPerms(DiscordPermission.CreateInstantInvite, permissionsofuser);
                perms.DEAFEN_MEMBERS = HasPerms(DiscordPermission.DeafenVCMembers, permissionsofuser);
                perms.EMBED_LINKS = HasPerms(DiscordPermission.EmbedLinks, permissionsofuser);
                perms.KICK_MEMBERS = HasPerms(DiscordPermission.KickMembers, permissionsofuser);
                perms.MANAGE_CHANNELS = HasPerms(DiscordPermission.ManageChannels, permissionsofuser);
                perms.MANAGE_EMOJIS = HasPerms(DiscordPermission.ManageEmojis, permissionsofuser);
                perms.MANAGE_GUILD = HasPerms(DiscordPermission.ManageGuild, permissionsofuser);
                perms.MANAGE_MESSAGES = HasPerms(DiscordPermission.ManageMessages, permissionsofuser);
                perms.MANAGE_NICKNAMES = HasPerms(DiscordPermission.ManageNicknames, permissionsofuser);
                perms.MANAGE_ROLES = HasPerms(DiscordPermission.ManageRoles, permissionsofuser);
                perms.MANAGE_WEBHOOKS = HasPerms(DiscordPermission.ManageWebhook, permissionsofuser);
                perms.MENTION_EVERYONE = HasPerms(DiscordPermission.MentionEveryone, permissionsofuser);
                perms.MOVE_MEMBERS = HasPerms(DiscordPermission.MoveVCMembers, permissionsofuser);
                perms.MUTE_MEMBERS = HasPerms(DiscordPermission.MuteMembers, permissionsofuser);
                perms.PRIORITY_SPEAKER = HasPerms(DiscordPermission.PrioritySpeaker, permissionsofuser);
                perms.READ_MESSAGE_HISTORY = HasPerms(DiscordPermission.ReadMessageHistory, permissionsofuser);
                perms.SEND_MESSAGES = HasPerms(DiscordPermission.SendMessages, permissionsofuser);
                perms.SEND_TTS_MESSAGES = HasPerms(DiscordPermission.SendTtsMessages, permissionsofuser);
                perms.SPEAK = HasPerms(DiscordPermission.SpeakInVC, permissionsofuser);
                perms.STREAM = HasPerms(DiscordPermission.Stream, permissionsofuser);
                perms.USE_EXTERNAL_EMOJIS = HasPerms(DiscordPermission.UseExternalEmojis, permissionsofuser);
                perms.USE_VAD = HasPerms(DiscordPermission.ForcePushToTalk, permissionsofuser);
                perms.VIEW_AUDIT_LOG = HasPerms(DiscordPermission.ViewAuditLog, permissionsofuser);
                perms.VIEW_CHANNEL = HasPerms(DiscordPermission.ViewChannel, permissionsofuser);
                perms.VIEW_GUILD_INSIGHTS = HasPerms(DiscordPermission.ViewGuildInsights, permissionsofuser);

                var output = JsonConvert.SerializeObject(perms, Formatting.Indented);

                Message.Edit(new MessageEditProperties()
                {
                    Content = "```\n" + output + "```"
                });


                bool doexport = false;
                if(export != null)
                {
                    if (export.ToLower().ToString() == "true")
                    {
                        doexport = true;
                    }
                } else
                {
                    doexport = false;
                }

            }
        }

        private bool HasPerms(DiscordPermission perm, DiscordPermission permissionsofuser)
        {
            return permissionsofuser.Has(perm);
        }
    }

    public class Permsjson
    {
        public bool CREATE_INSTANT_INVITE { get; set; }
        public bool KICK_MEMBERS { get; set; }
        public bool BAN_MEMBERS { get; set; }
        public bool ADMINISTRATOR { get; set; }
        public bool MANAGE_CHANNELS { get; set; }
        public bool MANAGE_GUILD { get; set; }
        public bool ADD_REACTIONS { get; set; }
        public bool VIEW_AUDIT_LOG { get; set; }
        public bool PRIORITY_SPEAKER { get; set; }
        public bool STREAM { get; set; }
        public bool VIEW_CHANNEL { get; set; }
        public bool SEND_MESSAGES { get; set; }
        public bool SEND_TTS_MESSAGES { get; set; }
        public bool MANAGE_MESSAGES { get; set; }
        public bool EMBED_LINKS { get; set; }
        public bool ATTACH_FILES { get; set; }
        public bool READ_MESSAGE_HISTORY { get; set; }
        public bool MENTION_EVERYONE { get; set; }
        public bool USE_EXTERNAL_EMOJIS { get; set; }
        public bool VIEW_GUILD_INSIGHTS { get; set; }
        public bool CONNECT { get; set; }
        public bool SPEAK { get; set; }
        public bool MUTE_MEMBERS { get; set; }
        public bool DEAFEN_MEMBERS { get; set; }
        public bool MOVE_MEMBERS { get; set; }
        public bool USE_VAD { get; set; }
        public bool CHANGE_NICKNAME { get; set; }
        public bool MANAGE_NICKNAMES { get; set; }
        public bool MANAGE_ROLES { get; set; }
        public bool MANAGE_WEBHOOKS { get; set; }
        public bool MANAGE_EMOJIS { get; set; }
    }
}
