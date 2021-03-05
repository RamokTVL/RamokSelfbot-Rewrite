using Discord.Commands;
using Discord;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Discord.Gateway;
using System.Drawing;
using System.Net;
using Leaf.xNet;
/*
namespace RamokSelfbot.Commands.Backup
{
    [Command("backup-load", "Apply the backup provided to the current server (you must be the server owner) - BACKUP")]
    class Backup_Load : CommandBase
    {

        [Parameter("backupid")]
        public string backupid { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
                if(guild.OwnerId != Client.User.Id)
                {
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "You must be the owner of the guild to apply the backup!"
                    };

                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                    return;
                }

                Root backup = JsonConvert.DeserializeObject<Root>(File.ReadAllText($"{RamokSelfbot.Utils.GetFileName()}\\backup\\{backupid}\\{backupid}.json"));
                ChannelRoot backupchannels = JsonConvert.DeserializeObject<ChannelRoot>(File.ReadAllText($"{RamokSelfbot.Utils.GetFileName()}\\backup\\{backupid}\\{backupid}-channels.json"));
                var guildverif = GuildVerificationLevel.None;
                switch(backup.verification_level)
                {
                    case 0:
                        guildverif = GuildVerificationLevel.None;
                        break;
                    case 1:
                        guildverif = GuildVerificationLevel.Low;
                        break;        
                    case 2:
                        guildverif = GuildVerificationLevel.Medium;
                        break;    
                    case 3:
                        guildverif = GuildVerificationLevel.High;
                        break;              
                    case 4:
                        guildverif = GuildVerificationLevel.Highest;
                        break;
                    default:
                        guildverif = GuildVerificationLevel.None;
                        break;
                }
             

                if(string.IsNullOrEmpty(backup.icon))
                {

                    guild.Modify(new GuildProperties()
                    {
                        Name = backup.name,
                        Region = backup.region,
                        VerificationLevel = guildverif
                    });
                } else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead($"https://cdn.discordapp.com/icons/{backup.id}/{backup.icon}");


                    guild.Modify(new GuildProperties()
                    {
                        Name = backup.name,
                        Region = backup.region,
                        VerificationLevel = guildverif,
                        Icon = Bitmap.FromStream(stream)
                    });

                    stream.Flush();
                    stream.Close();
                    client.Dispose();
                }

                foreach (var role in backup.roles)
                {
                   var newrole = guild.CreateRole();
                    HttpRequest request = new HttpRequest();
                    request.AddHeader("Authorization", Client.Token);
                    CustomRole rolecustom = JsonConvert.DeserializeObject<CustomRole>(File.ReadAllText($"{RamokSelfbot.Utils.GetFileName()}\\ressources\\backupmodifyrole.exemple"));
                    rolecustom.name = role.name;
                    rolecustom.color = role.color;
                    rolecustom.hoist = role.hoist;
                    rolecustom.permissions = int.Parse(role.permissions);
                    rolecustom.mentionnable = role.mentionable;
                    request.Patch("https://discordapp.com/api/v8/guilds/" + guild.Id + "/roles/" + newrole.Id.ToString());
                }

                foreach (var channels in guild.GetChannels())
                {
                    channels.Delete();
                }

                string[] parent;

                for (int i = 0; i < backupchannels.id.Length; i++)
                {
                    if(backupchannels.type == 4)
                    {
                   //     parent += 
                    }
                }
            }
        }
    }

    public class PermissionOverwrite
    {
        public string id { get; set; }
        public int type { get; set; }
        public string allow { get; set; }
        public string deny { get; set; }
    }

    public class ChannelRoot
    {
        public string id { get; set; }
        public string last_message_id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public string parent_id { get; set; }
        public string topic { get; set; }
        public string guild_id { get; set; }
        public List<PermissionOverwrite> permission_overwrites { get; set; }
        public bool nsfw { get; set; }
        public int rate_limit_per_user { get; set; }
        public int? bitrate { get; set; }
        public int? user_limit { get; set; }
    }

    public class CustomRole
    {
        public string name { get; set; }
        public int permissions { get; set; }
        public int color { get; set; }
        public bool hoist { get; set; }
        public bool mentionnable { get; set; }
    }

    public class Role
    {
        public string name { get; set; }
        public string permissions { get; set; }
        public int position { get; set; }
        public int color { get; set; }
        public bool hoist { get; set; }
        public bool mentionable { get; set; }
    }

    public class Emoji
    {
        public string name { get; set; }
        public List<object> roles { get; set; }
        public bool require_colons { get; set; }
        public bool managed { get; set; }
        public bool animated { get; set; }
        public bool available { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public string splash { get; set; }
        public string discovery_splash { get; set; }
        public List<string> features { get; set; }
        public List<Emoji> emojis { get; set; }
        public string banner { get; set; }
        public string owner_id { get; set; }
        public string region { get; set; }
        public int afk_timeout { get; set; }
        public bool widget_enabled { get; set; }
        public int verification_level { get; set; }
        public List<Role> roles { get; set; }
        public int default_message_notifications { get; set; }
        public int mfa_level { get; set; }
        public int explicit_content_filter { get; set; }
        public int max_video_channel_users { get; set; }
        public object vanity_url_code { get; set; }
        public int system_channel_flags { get; set; }
        public string preferred_locale { get; set; }
    }

} 






ADDING THIS LATERR..
*/