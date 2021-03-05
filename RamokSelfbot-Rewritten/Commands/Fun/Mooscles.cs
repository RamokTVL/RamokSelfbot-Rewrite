using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("mooscles", "Show the mooscles of someone - FUN")]
    class Mooscles : CommandBase
    {
        [Parameter("id")]
        public string id { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
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

                string[] moosclestype = new String[5];
                moosclestype[0] = "milligrams";
                moosclestype[1] = "grams";
                moosclestype[2] = "kilograms";
                moosclestype[3] = "tons";
                moosclestype[4] = "peta-Tons";

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "<@" + user.Id + "> has " + new Random().Next(0, 999) + " " + moosclestype[new Random().Next(moosclestype.Length - moosclestype.Length, moosclestype.Length)] + " of mooscles"
                };

                if(File.ReadAllText("ressources\\listhetero.selfbot").Contains(user.Id.ToString()))
                {
                    embed.Description = "<@" + user.Id + "> has ∞ peta-tons of mooscles";
                }




                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
