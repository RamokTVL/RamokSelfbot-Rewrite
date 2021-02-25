using Discord;
using Discord.Commands;
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("unverify", "Disable the email verification of your account. - UTILS")]
    class Unverify : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                Message.Delete();
                HttpRequest request = new HttpRequest();
                request.AddHeader("Authorization", Client.Token);
                var guildid = Client.GetGuilds()[0].Id.ToString();
                request.Get("https://discordapp.com/api/v8/guilds/" + guildid + "/members");

                EmbedMaker embed = new EmbedMaker()
                {
                    Description = "Email & Phone verification should be disabled on this account !",
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                };
                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
