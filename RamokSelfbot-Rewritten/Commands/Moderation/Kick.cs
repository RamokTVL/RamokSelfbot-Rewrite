using Discord.Commands;
using System;
using Discord;
using Discord.Gateway;
using Newtonsoft.Json;
using System.IO;

namespace RamokSelfbot.Commands.Moderation
{
    [Command("kick", "Kick user from the server. - MODERATION")]
    class Kick : CommandBase
    {
        [Parameter("id")]
        public string id { get; set; }

        [Parameter("reason")]
        public string reasonp { get; set; }

        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                if (Message.Guild != null)
                {
                    Message.Delete();

                    string reason;
                    if (reasonp == null)
                    {
                        reason = "No reason provided";
                    }
                    else
                    {
                        reason = reasonp;
                    }

                    DiscordUser banned = Client.GetUser(Program.id);
                    if (id == null)
                    {
                        ValidUser(1);
                        return;
                    }
                    else
                    {
                        if (id.Length == 18)
                        {
                            try
                            {
                                banned = Client.GetUser(ulong.Parse(id));
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
                        }
                        else if (Message.Mentions.Count > 1 || Message.Mentions.Count == 1)
                        {
                            try
                            {
                                banned = Client.GetUser(Message.Mentions[0].Id);
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
                        }
                        else
                        {
                            ValidUser(2);
                            return;
                        }



                        SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
                        Random rnd = new Random();

                        EmbedMaker sendembed = new EmbedMaker()
                        {
                            Title = $"You are kicked in {guild.Name}",
                            Color = System.Drawing.Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                            Footer = new EmbedFooter() { Text = "Selfbot made by Ramok with <3" },
                            ThumbnailUrl = guild.Icon.Url,
                        };

                        sendembed.AddField("Kicked by", Message.Author.Member.User.Username + "#" + Message.Author.Member.User.Discriminator);
                        sendembed.AddField("Reason", reason);
                        sendembed.AddField("At", DateTime.Now.ToString());

                        Client.CreateDM(banned.Id).SendMessage("", false, sendembed);

                        EmbedMaker logembed = new EmbedMaker()
                        {
                            Title = $"{guild.Name} is kicked.",
                            Color = System.Drawing.Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                            Footer = new EmbedFooter() { Text = "Selfbot made by Ramok with <3" },
                            ThumbnailUrl = guild.Icon.Url,
                        };

                        logembed.AddField("Reason", reason);
                        logembed.AddField("Kicked by", Message.Author.Member.User.Username + "#" + Message.Author.Member.User.Discriminator);
                        logembed.AddField("At", DateTime.Now.ToString());
                        DiscordChannel channel = Client.GetChannel(Message.Channel.Id);
                        logembed.AddField("In channel", channel.Name);

                        foreach (var channels in Message.Guild.GetChannels())
                        {
                            if (channels.Name.Contains("log"))
                            {
                                TextChannel logchannels = (TextChannel)Client.GetChannel(channels.Id);
                                logchannels.SendMessage("", false, logembed);
                            }
                        }

                        try
                        {
                            GuildMember toban = Message.Guild.GetMember(banned.Id);
                            if (toban.GetPermissions().Has(DiscordPermission.Administrator))
                            {
                                RamokSelfbot.Utils.Print($"I can't kick ${banned.Username} beacause he have \"Administrator\" permission.");
                                return;
                            }
                            if (Message.Author.Member.GetPermissions().Has(DiscordPermission.KickMembers))
                            {
                                toban.Kick();
                            }

                        }
                        catch (Exception ex)
                        {
                            Message.Channel.SendMessage($"I can't kick ${banned.Username}.\nError code : ${ex.Message}");
                        }
                    }
                }
            }

        }
        private void ValidUser(int a)
        {
            if (Program.Debug == true)
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

            if (Message.Guild == null)
            {
                Message.Edit(new Discord.MessageEditProperties()
                {
                    Content = "",
                    Embed = embed
                });
                return;
            }
            else
            {
                if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator)) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                    return;
                }
            }
        }

    }


}
