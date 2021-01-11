using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Others
{
    [Command("ping", "Ping the website specified, if there is no site specified, cdn.discordapp.com will be pinged - OTHERS")]
    class Ping : CommandBase
    {
        [Parameter("website", true)]
        public string website { get; set; }
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                PingReply r;
                string s;
                if (website != null)
                {
                    s = website;
                }
                else
                {
                    s = "cdn.discordapp.com";
                }
                r = p.Send(s);

                if (r.Status == IPStatus.Success)
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = ":ping_pong: Ping to " + s + " : " + r.RoundtripTime.ToString() + " ms"
                    });
                } else
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = ":ping_pong: Cannot ping : " + s
                    });
                }
            
                    
                }
            }
        }




    }

