using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;

namespace RamokSelfbot.Commands.Utils
{
    [Command("myip", "Give you your public IP - UTILS")]
    class MyIP : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                WebClient client = new WebClient();

                EmbedMaker embed = new EmbedMaker()
                {
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Color = RamokSelfbot.Utils.EmbedColor(),
                };

                embed.AddField("IPV4", client.DownloadString("https://ipv4.lafibre.info/ip.php"), true);

                string ipv6 = null;
                bool ipv6e = false;
                try
                {
                    ipv6 = client.DownloadString("https://ipv6.lafibre.info/ip.php");
                    ipv6e = true;
                } catch(Exception ex)
                {
                    if(Program.Debug)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    ipv6e = false;
                }

                if(ipv6e == true)
                {
                    embed.AddField("IPV6", client.DownloadString("https://ipv6.lafibre.info/ip.php"), true);
                } else
                {
                    embed.AddField("IPV6", "Disabled", true);
                }

                string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                if(Program.Debug)
                    Console.WriteLine(hostName);

                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                embed.AddField("Local IP", myIP, true);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
