using Discord;
using Discord.Commands;
using Discord.Gateway;
using Discord.Media;
using Discord.WebSockets;
using Newtonsoft.Json;
using System;
using System.IO;

namespace RamokSelfbot.Commands.Info
{
    [Command("userinfo", "Give public informations about the user - INFO")]
    class UserInfo : CommandBase
    {
        [Parameter("id", true)]
        public string id { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                DiscordUser user = Client.GetUser(Program.id);
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
                            user = Client.GetUser(ulong.Parse(id));
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
                    } else
                    {
                        RamokSelfbot.Utils.ValidUser(4, Message);
                        return;
                    }
                }

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Description = "These informations are publics.\nThey cant injure <@" + user.Id + ">."
                };

                string avatarurl;
                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);



                embed.AddField("Username", $"```{user.Username}#{user.Discriminator}```", true);
                embed.AddField("ID", "```" + user.Id.ToString() + "```", true);


                try
                {
                    if (user.Avatar == null)
                    {
                        embed.AddField("Avatar URL", "```No Avatar```", true);
                    }
                    else
                    {
                        embed.ThumbnailUrl = user.Avatar.Url;
                        avatarurl = new System.Net.WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + user.Avatar.Url);
                        embed.AddField("Avatar URL", "```" + avatarurl + "```", true);
                    }
                } catch
                {
                    embed.AddField("Avatar URL", "```No Avatar```", true);
                }

                if (Message.Author.User.Avatar != null)
                {
                    if(embed.ThumbnailUrl == null)
                    {
                        embed.ThumbnailUrl = Message.Author.User.Avatar.Url;
                    }
                }

                if(Message.Guild != null)
                {

                    if(Message.Guild.GetMember(user.Id).Nickname == null)
                    {
                        embed.AddField("Nickname", "```No Nickname```", true);
                    } else
                    {
                        embed.AddField("Nickname", "```" + Message.Guild.GetMember(user.Id).Nickname + "```", true);
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

                if(user.GetProfile().NitroSince.ToString() == "")
                {
                    embed.AddField("Nitro", "```No```", true);
                }else
                {
                    embed.AddField("Nitro", "```Yes (since " + user.GetProfile().NitroSince.ToString() + ")```", true);
                }

                


                if (user.Badges.ToString() != null)
                {
                    embed.AddField("Badges", "```\n" + user.Badges.ToString() + "```", false);
                }


                if(Message.Guild != null)
                {
                    string roles = "```\n";
                    int rolesn = 0;
                    //embed.AddField("Nickname", "```" + )
                    if (Message.Guild.GetMember(user.Id).Roles.Count < 15)
                    {
                        for (int i = 0; i < Message.Guild.GetMember(user.Id).Roles.Count; i++)
                        {
                            rolesn++;
                            var roleidd = Message.Guild.GetMember(user.Id).Roles[i];
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
                            var roleidd = Message.Guild.GetMember(user.Id).Roles[i];
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

                    perms += HasPermission(DiscordPermission.CreateInstantInvite, user, "Create Invite");
                    perms += HasPermission(DiscordPermission.SendMessages, user, "Send Message");
                    perms += HasPermission(DiscordPermission.ReadMessageHistory, user, "Read Message History");
                    perms += HasPermission(DiscordPermission.UseExternalEmojis, user, "Use External Emojis");
                    perms += HasPermission(DiscordPermission.ConnectToVC, user, "Connect");
                    perms += HasPermission(DiscordPermission.SpeakInVC, user, "Speak");
                    perms += HasPermission(DiscordPermission.MuteMembers, user, "Mute members");
                    perms += HasPermission(DiscordPermission.DeafenVCMembers, user, "Deafen members");
                    perms += HasPermission(DiscordPermission.MoveVCMembers, user, "Move members");
                    perms += HasPermission(DiscordPermission.PrioritySpeaker, user, "Priority Speaker");
                    perms += "```";

                    embed.AddField("Global Permissions", perms, false);
                }

                    int serversn = 0;
                    string servers = "```\n";
                    if (user.GetProfile().MutualGuilds.Count < 15)
                    {
                        for (int i = 0; i < user.GetProfile().MutualGuilds.Count; i++)
                        {
                            serversn++;
                            var serveridd = user.GetProfile().MutualGuilds[i].Id;
                            SocketGuild guild = Client.GetCachedGuild(serveridd);
                            if (Program.Debug)
                            {
                                Console.WriteLine(servers);
                                Console.WriteLine(i.ToString());
                                Console.WriteLine(serveridd.ToString());
                                Console.WriteLine(guild.Name);
                                Console.WriteLine(servers);
                            }

                            servers = servers + guild.Name + "\n";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            serversn++;
                            var serveridd = user.GetProfile().MutualGuilds[i].Id;
                            SocketGuild guild = Client.GetCachedGuild(serveridd);
                            servers = servers + guild.Name + "\n";
                        }
                    }

                servers += "```";

                embed.AddField("Mutual Guilds [" + serversn.ToString() + "] (shows up to 15 guilds)", servers, false);
                embed.AddField("Created at", "```\n" + user.CreatedAt.Day.ToString() + "/" + user.CreatedAt.Month + "/" + user.CreatedAt.Year + " " + user.CreatedAt.Hour + "hours " + user.CreatedAt.Minute + "min " + user.CreatedAt.Second + "seconds\n```", false);
                if(Message.Guild != null)
                {
                    embed.AddField("Joined the server at", "```\n" + Client.GetCachedGuild(Message.Guild.Id).GetMember(user.Id).JoinedAt.ToString() + "```", false);
                }



                RamokSelfbot.Utils.SendEmbed(Message, embed);

            }
        }

        private string HasPermission(DiscordPermission permission, DiscordUser user, string NamePerm)
        {
            if(Message.Guild.GetMember(user.Id).GetPermissions().Has(permission))
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
