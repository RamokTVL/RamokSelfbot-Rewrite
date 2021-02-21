using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Utils
{
    [Command("invitebot", "Give you the link to send a bot. - UTILS")]
    class InviteBot : CommandBase
    {
        [Parameter("botid")]
        public ulong botid { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                if(Client.GetUser(botid).Type == DiscordUserType.Bot)
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = $"https://discord.com/oauth2/authorize?client_id={botid}&scope=bot&permissions=8"
                    });
                } else
                {
                    RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "This is not an bot user"
                    });
                }

            }
        }
    }
}
