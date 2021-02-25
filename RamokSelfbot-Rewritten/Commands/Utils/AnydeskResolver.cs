using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("anydeskresolver", "A classic Anydesk Resolver (use logs to get ip) - UTILS")]
    class AnydeskResolver : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\AnyDesk\\ad.trace";
                if(File.Exists(path))
                {
                    string ips = "";
                    foreach (var fileline in File.ReadAllLines(path))
                    {
                        if(fileline.Contains("anynet.any_socket - Logged in from"))
                        {
                            string date = "";
                            string ip = "";

                            ip = fileline.Substring(0, fileline.Length - 1);
                            ip = ip.Substring(0, ip.Length - 25).Substring(112);

                            date = fileline.Substring(0, fileline.Length - 1);
                            date = date.Substring(0, date.Length - 117).Substring(8);

                            date.Replace("-", "/");
                       //     date.Remove(date.Length - 7, date.Length);

                            if(!ips.Contains(ip))
                            {
                                ips +=   "`[" + ip + "]" + " " + date + "`" + "\n";
                            }
                        }
                    }

                    if(string.IsNullOrEmpty(ips))
                    {
                        EmbedMaker embed = new EmbedMaker()
                        {
                            Color = RamokSelfbot.Utils.EmbedColor(),
                            Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                            Description = "```\n" + "No ip found" + "```"
                        };

                        RamokSelfbot.Utils.SendEmbed(Message, embed);
                    } else
                    {
                        EmbedMaker embed = new EmbedMaker()
                        {
                            Color = RamokSelfbot.Utils.EmbedColor(),
                            Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                            Description = ips,
                            Title = "Anydesk Resolver results!"
                        };
                        embed.Footer.Text += "\nRamok is not responsable of any attack / not legal thing you can do w/ this\nThis command is made for educational purposes only";
                        RamokSelfbot.Utils.SendEmbed(Message, embed);
                    }
                } else
                {

                }
            }
        }
    }
}
