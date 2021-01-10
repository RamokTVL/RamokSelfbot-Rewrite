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
                    ValidUser(1);
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
                                ValidUser(3);
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
                                ValidUser(3);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    } else
                    {
                        ValidUser(2);
                        return;
                    }
                }

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Description = "These informations are publics.\nThey cant injure <@" + user.Id + ">."
                };

                string avatarurl;
                if (Message.Author.User.Avatar.Url == null)
                {
                    embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3" };
                }
                else
                {
                    embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3", IconUrl = Message.Author.User.Avatar.Url };
                }



                embed.AddField("Username", $"```{user.Username}#{user.Discriminator}```", true);
                embed.AddField("ID", "```" + user.Id.ToString() + "```", true);
                if (user.Avatar.Url == null)
                {
                    embed.AddField("Avatar URL", "```No Avatar```", true);
                }
                else
                {
                    
                    embed.ThumbnailUrl = user.Avatar.Url;
                    avatarurl = new System.Net.WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + user.Avatar.Url);
                    embed.AddField("Avatar URL", "```" + avatarurl + "```", true);
                }

                if (Message.Author.User.Avatar.Url != null)
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


                    embed.AddField("\u200B", "\u200B‎", false);

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






                if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator) || Client.GetCachedGuild(Message.Guild.Id).OwnerId == Program.id) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                }

            }
        }

        /*private void HasPermission(DiscordPermission permission, DiscordUser user)
        {
            if(Message.Guild.GetMember(user.Id).GetPermissions().Has(permission))
            {
                permission = "✅ " + permission.ToString();
            }
        } todo*/


        private void ValidUser(int a)
        {
            if(Program.Debug == true)
            {
                Console.WriteLine(a.ToString());
            }

            EmbedMaker embed = new EmbedMaker()
            {
                Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                Description = "<@" + Message.Author.User.Id.ToString() + ">, please mention a valid user !"
            };

            if (Message.Author.User.Avatar.Url == null)
            {
                embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3" };
            }
            else
            {
                embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3", IconUrl = Message.Author.User.Avatar.Url };
            }

            if(Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator) || Client.GetCachedGuild(Message.Guild.Id).OwnerId == Program.id) //CHECK DE PERMISSIONS
            {
                Message.Edit(new MessageEditProperties()
                {
                    Content = "",
                    Embed = embed
                });
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
