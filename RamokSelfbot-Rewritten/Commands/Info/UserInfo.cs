using Discord;
using Discord.Commands;
using Discord.Gateway;
using Discord.Media;
using Discord.WebSockets;
using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace RamokSelfbot.Commands.Info
{
    [Command("userinfo", "Give public informations about the user - INFO")]
    class UserInfo : CommandBase
    {
        [Parameter("id", true)]
        public string id { get; set; }
        public override async void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                DiscordUser user = await Client.GetUserAsync(Program.id);
                if(id == null)
                {
                    RamokSelfbot.Utils.ValidUser(1, Message);
                    return;
                } else
                {
                    if(id.Length == 18)
                    {
                        try
                        {
                            user = await Client.GetUserAsync(ulong.Parse(id));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Unknown User"))
                            {
                                RamokSelfbot.Utils.ValidUser(2, Message);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    } else if(Message.Mentions.Count > 1 || Message.Mentions.Count == 1)
                    {
                        try
                        {
                            user = await Client.GetUserAsync(Message.Mentions[0].Id);
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
                    } else
                    {
                        RamokSelfbot.Utils.ValidUser(4, Message);
                        return;
                    }
                }

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Description = "These informations are publics.\nThey cant injure <@" + user.Id + ">.",
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
            };

                

                embed.AddField("Username", $"```{user.Username}#{user.Discriminator}```", true);
                embed.AddField("ID", "```" + user.Id.ToString() + "```", true);

                //AVATAR GOES HERE
                
                if(user.Avatar == null)
                {
                    embed.AddField("Avatar URL", "```\nNo Avatar```", true);
                } else
                {
                    try
                    {
                        HttpRequest request = new HttpRequest();
                        string content = request.Get(user.Avatar.Url + ".gif").StatusCode.ToString();
                        embed.AddField("Avatar URL", new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + user.Avatar.Url + ".gif"), true);
                        if (content.Contains("415"))
                        {
                            embed.ThumbnailUrl = user.Avatar.Url;
                            if (Program.Debug)
                            {
                                Console.WriteLine("415");
                                Console.WriteLine("415");
                                Console.WriteLine("415");
                                Console.WriteLine("415");
                                Console.WriteLine("415");
                                Console.WriteLine("415");
                            }
                            embed.AddField("Avatar URL", new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + user.Avatar.Url), true);
                        }
                        else
                        {
                            embed.ThumbnailUrl = user.Avatar.Url + ".gif";
                        }
                        /*    if()
                            {
                                */
                    }
                    catch (Exception ex)
                    {
                        if (Program.Debug)
                        {
                            if (ex.Message.Contains("415"))
                            {
                                embed.ThumbnailUrl = user.Avatar.Url;
                                if (Program.Debug)
                                {
                                    Console.WriteLine("415");
                                    Console.WriteLine("415");
                                    Console.WriteLine("415");
                                    Console.WriteLine("415");
                                    Console.WriteLine("415");
                                    Console.WriteLine("415");
                                }
                                embed.AddField("Avatar URL", new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + user.Avatar.Url), true);
                            }
                        }
                    }
                }

                bool intheserver = false;
                try
                {
                    if(Message.Guild != null)
                    {
                        GuildMember member = Message.Guild.GetMember(user.Id);
                        intheserver = true;
                    } else
                    {
                        intheserver = false;
                    }
                } catch(Exception)
                {
                    intheserver = false;
                }

                if(intheserver)
                {
                    GuildMember member = Message.Guild.GetMember(user.Id);
                    if (member.Nickname == null)
                    {
                        embed.AddField("Nickname", "```No Nickname```", true);
                    }
                    else
                    {
                        embed.AddField("Nickname", "```" + member.Nickname + "```", true);
                    }
                }


                embed.AddField("Hypesquad", "```\n" + user.Hypesquad.ToString() + "```", true);
                if (user.Type == DiscordUserType.Bot)
                {
                    embed.AddField("Is a bot", "```Yes```", true);
                }
                else
                {
                    embed.AddField("Is a bot", "```No```", true);
                }

                DiscordProfile profile = user.GetProfile();
                if (profile.NitroSince.ToString() == "")
                {
                    embed.AddField("Nitro", "```No```", true);
                }else
                {
                    embed.AddField("Nitro", "```Yes (since " + profile.NitroSince.ToString() + ")```", true);
                }

                


                if (user.Badges.ToString() != null)
                {
                    embed.AddField("Badges", "```\n" + user.Badges.ToString() + "```", false);
                }

                if(intheserver)
                {
                    GuildMember member = Message.Guild.GetMember(user.Id);
                    string roles = "```\n";
                    int rolesn = 0;
                    //embed.AddField("Nickname", "```" + )
                    int rolescount = member.Roles.Count;
                    if (rolescount < 15)
                    {
                        for (int i = 0; i < rolescount; i++)
                        {
                            rolesn++;
                            var roleidd = member.Roles[i];
                            DiscordRole role = Client.GetGuildRole(roleidd);
                            if (Program.Debug)
                            {
                                Console.WriteLine(roles);
                                Console.WriteLine(i.ToString());
                                Console.WriteLine(roleidd.ToString());
                                Console.WriteLine(role.Name);
                                Console.WriteLine(roles);
                            }
                            roles = roles + role.Name + "\n";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            rolesn++;
                            var roleidd = member.Roles[i];
                            DiscordRole role = Client.GetGuildRole(roleidd);
                            roles = roles + role.Name + "\n";
                        }
                    }

                    roles += "```";

                    if (rolesn == 0)
                    {
                        roles = "```No role !```";
                    }


                    embed.AddField("Roles [" + rolesn.ToString() + "] (shows up to 15 roles)", roles, false);

                    string perms = "```\n";

                    DiscordPermission memberperms = await member.GetPermissionsAsync();
                    perms += HasPermission(DiscordPermission.CreateInstantInvite, memberperms, "Create Invite");
                    perms += HasPermission(DiscordPermission.SendMessages, memberperms, "Send Message");
                    perms += HasPermission(DiscordPermission.ReadMessageHistory, memberperms, "Read Message History");
                    perms += HasPermission(DiscordPermission.UseExternalEmojis, memberperms, "Use External Emojis");
                    perms += HasPermission(DiscordPermission.ConnectToVC, memberperms, "Connect");
                    perms += HasPermission(DiscordPermission.SpeakInVC, memberperms, "Speak");
                    perms += HasPermission(DiscordPermission.MuteMembers, memberperms, "Mute members");
                    perms += HasPermission(DiscordPermission.DeafenVCMembers, memberperms, "Deafen members");
                    perms += HasPermission(DiscordPermission.MoveVCMembers, memberperms, "Move members");
                    perms += HasPermission(DiscordPermission.PrioritySpeaker, memberperms, "Priority Speaker");
                    perms += "```";

                    embed.AddField("Global Permissions", perms, false);
                }

                    int serversn = 0;
                    string servers = "```\n";
                int mutualguildcount = profile.MutualGuilds.Count;
                    if (mutualguildcount < 15)
                    {
                    
                        for (int i = 0; i < mutualguildcount; i++)
                        {
                            SocketGuild guild = Client.GetCachedGuild(profile.MutualGuilds[i].Id);
                            servers += guild.Name + "\n";
                        }
                    }
                    else
                    {
                    serversn = mutualguildcount;
                    for (int i = 0; i < 15; i++)
                        {                  
                            SocketGuild guild = Client.GetCachedGuild(profile.MutualGuilds[i].Id);
                        servers += guild.Name + "\n";
                    }
                    }

                servers += "```";

                embed.AddField("Mutual Guilds [" + serversn.ToString() + "] (shows up to 15 guilds)", servers, false);
                embed.AddField("Created at", "```\n" + user.CreatedAt.Day.ToString() + "/" + user.CreatedAt.Month + "/" + user.CreatedAt.Year + " " + user.CreatedAt.Hour + "hours " + user.CreatedAt.Minute + "min " + user.CreatedAt.Second + "seconds\n```", false);
                if(Message.Guild != null)
                {
                    if(intheserver)
                    {
                        GuildMember member = Message.Guild.GetMember(user.Id);
                        embed.AddField("Joined the server at", "```\n" + member.JoinedAt.ToString() + "```", false);
                    }
                }



                RamokSelfbot.Utils.SendEmbed(Message, embed);

            }
        }

        private string HasPermission(DiscordPermission permission, DiscordPermission member, string NamePerm)
        {
            if(member.Has(permission))
            {
                return "✅ " + NamePerm + "\n";
            } else
            {
                return "❌ " + NamePerm + "\n";
            }
        }



        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
              
                if (Message.Author.User.Id == Program.id)
                {
                    Console.WriteLine("error occured : " + exception.Message);
                }
        }



    }
}
