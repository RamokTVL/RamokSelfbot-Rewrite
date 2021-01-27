using Discord;
using Discord.Commands;
using System;
using System.IO;

namespace RamokSelfbot.Commands.Fun
{
    [Command("gaypercent", "Calculate the gay % of someone - FUN")]
    class GayPercent : CommandBase
    {
        [Parameter("id")]
        public string id { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                int gay = new Random().Next(0, 888);

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
                EmbedMaker embed = new EmbedMaker();

                embed.Color = RamokSelfbot.Utils.EmbedColor();
                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                if (File.ReadAllText("ressources\\listhetero.selfbot").Contains(user.Id.ToString()))
                {
                    embed.Description = "<@" + user.Id + "> is not gay";
                } else
                {
                    embed.Description = "<@" + user.Id + "> is gay for " + gay + "%";
                }

                if(user.Avatar != null)
                {
                    embed.ThumbnailUrl = user.Avatar.Url;
                }

                
                

                    
                

                RamokSelfbot.Utils.SendEmbed(Message, embed);

            }
        }
    }
}
