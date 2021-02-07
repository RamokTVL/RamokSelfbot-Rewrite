using Discord;
using Discord.Commands;

using System;
using System.IO;

namespace RamokSelfbot.Commands.Fun
{
    [Command("iq", "Get the iq of the person - FUN")]
    class IQ : CommandBase
    {
        [Parameter("id")]
        public string id { get; set; }

        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
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

                EmbedMaker embed = new EmbedMaker()
                {
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Color = RamokSelfbot.Utils.EmbedColor()
                };

                string IQ = new Random().Next(0, 200).ToString();

                if(File.ReadAllText("ressources\\listhetero.selfbot").Contains(user.Id.ToString()))
                {
                    IQ = "∞";
                    embed.ImageUrl = "https://media1.tenor.com/images/1e158cadbc4e98e60d95fdff49b1ad25/tenor.gif";
                }


                embed.Description = "<@" + user.Id + "> has " + IQ.ToString() + " iq";
               

                if(user.Avatar != null)
                {
                    embed.ThumbnailUrl = user.Avatar.Url;
                }

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
