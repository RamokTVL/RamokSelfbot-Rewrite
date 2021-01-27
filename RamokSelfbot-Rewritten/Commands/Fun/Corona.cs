using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("coronadetector", "Show you if someone is infected by the coronavirus - FUN")]
    class Corona : CommandBase
    {

        [Parameter("id", true)]
        public string id { get; set; }
        public override void Execute()
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


            int corona = new Random().Next(1, 3);
            if(Program.Debug)
            {
                Console.WriteLine(corona);
            }

            EmbedMaker embed = new EmbedMaker()
            {
                Color = RamokSelfbot.Utils.EmbedColor(),
                Footer = RamokSelfbot.Utils.footer(Message.Author.User)
            };

            if(corona == 1)
            {
                embed.Description = "<@" + user.Id + "> is infected !";
            } else
            {
                embed.Description = "<@" + user.Id + "> is safe.";
            }

            RamokSelfbot.Utils.SendEmbed(Message, embed);
       }
    }
}
