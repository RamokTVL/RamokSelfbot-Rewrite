using Discord.Commands;
using Discord.Gateway;
using Discord;
using System.Net;

namespace RamokSelfbot.Commands.Info
{
    [Command("guildinfo", "Get informations of a guild - INFO")]
    class GuildInfo : CommandBase
    {
        [Parameter("id", true)]
        public string id { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor()
                };


                if(id == null || id.Length != 18)
                {
                    id = Message.Guild.Id.ToString();
                };

                SocketGuild guild = Client.GetCachedGuild(ulong.Parse(id));
                DiscordUser owner = Client.GetUser(guild.OwnerId);
                embed.AddField("Name", "```\n" + guild.Name + "```", true);
                embed.AddField("Server Owner", "```\n" + owner.Username + "#" + owner.Discriminator + "```", true);
                embed.Footer = RamokSelfbot.Utils.footer(owner);
                if(embed.Footer == null)
                {
                    embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);
                };


                /*       int bots = 0;   -FIXME
                       foreach(var members in guild.GetMembers())
                       {
                           if(members.User.Type == DiscordUserType.Bot)
                           {
                               bots++;
                           }
                       };

                       embed.AddField("Server members [ " + guild.MemberCount + " ]", "```\nMembers: " + (guild.MemberCount - bots) + " | Bots : " + bots.ToString() + "```", false) ;*/
                embed.AddField("Server members [ " + guild.MemberCount + " ]", "```\nMembers: " + guild.MemberCount + "```", false);
                embed.AddField("Max members", "```\nMax members: " + guild.MaxMembers + "```", false);
                embed.AddField("Server ID", "```\n" + guild.Id + "```", true);
                embed.AddField("Server Region", "```\n" + guild.Region + "```", true);
             //   embed.AddField("\u200b", "\u200b", false);


                int animated = 0;
                int notanimated = 0;

                foreach(var emojis in guild.Emojis)
                {
                    if(emojis.Animated)
                    {
                        animated++;
                    } else
                    {
                        notanimated++;
                    }
                }

                embed.AddField("Server emojis [ " + guild.Emojis.Count + " ]", "```\nNormal: " + notanimated.ToString() + " | Animated: " + animated.ToString() + "```", false);


                string premiumtier = "Level 0";

                switch(guild.PremiumTier.ToString())
                {
                    case "Tier1":
                        premiumtier = "Level 1";
                        break;           
                    case "Tier2":
                        premiumtier = "Level 2";
                        break;         
                    case "Tier3":
                        premiumtier = "Level 3";
                        break;
                    default:
                        premiumtier = "Level 0";
                        break;
                }


                embed.AddField("Server boost level", "```\n" + premiumtier + "```", true);
                embed.AddField("Server boost amount", "```\n" + guild.NitroBoosts + "```", true);

                if(guild.Roles.Count > 30)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        roles += guild.Roles[i].Name + "\n";
                    }
                } else
                {
                    for (int i = 0; i < guild.Roles.Count; i++)
                    {
                        roles += guild.Roles[i].Name + "\n";
                    }
                }

                embed.AddField("Server roles [ " + guild.Roles.Count + " ] (shows up to 30 roles)", "```\n" + roles + "```", false);

                string systemchannel = "System Channel : ❌ doesn't exist";
                string ruleschannel = "Rules channel : ❌ doesn't exist";
                string publicupdateschannel = "Public Updates Channel : ❌ doesn't exist";
                if(guild.SystemChannel != null)
                {
                    systemchannel = "System Channel : " + Client.GetChannel(guild.SystemChannel.Channel.Id).Name;
                }

                if(guild.RulesChannel != null)
                {
                    ruleschannel = "Rules channel : " + Client.GetChannel(guild.RulesChannel.Id).Name;
                }

                if(guild.PublicUpdatesChannel != null)
                {
                    publicupdateschannel = "Public Updates Channel : " + Client.GetChannel(guild.PublicUpdatesChannel.Id).Name;
                }



                embed.AddField("Channels", "```\n" + systemchannel + "\n" + ruleschannel + "\n" + publicupdateschannel + "```", false);

                if (guild.Icon != null)
                {
                    try
                    {
                        embed.AddField("Icon Link", "```\n" + new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + guild.Icon.Url) + "```", true);
                    }
                    catch { }
                }
                else
                {
                    embed.AddField("Icon Link", "```\nNo Icon ```", true);
                }

                if (guild.Splash != null)
                {
                    try
                    {
                        embed.AddField("Splash Link", "```\n" + new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + guild.Splash.Url) + "```", true);
                    }
                    catch { }
                }
                else
                {
                    embed.AddField("Splash Link", "```\nNo Splash```", true);
                }



                // FIXME     embed.AddField("Server created on (DD/MM/YYYY)", "```\n" + guild.CreatedAt.Day + "/" + guild.CreatedAt.Month + "/" + guild.CreatedAt.Year + "```");


                RamokSelfbot.Utils.SendEmbed(Message, embed);
   
            }
        }

        public string roles;
    }


}
