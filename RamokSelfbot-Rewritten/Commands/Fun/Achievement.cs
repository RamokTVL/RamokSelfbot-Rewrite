using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("achievement", "Send a achievement minecraft here - FUN")]
    class Achievement : CommandBase
    {
        [Parameter("icon", false)]
        public string icon { get; set; }

        [Parameter("text", false)]
        public string text { get; set; }

        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                try
                {
                    int.Parse(icon);
                } catch
                {
                    Message.Edit(new MessageEditProperties()
                    {
                        Content = "Icon not valid"
                    });
                    return;
                };



                /*if (Regex.Match(text, "/[ 0-9a-z]/").Success)
                {
                    RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                    {
                        Description = "Please use non-HTML encoded characters!",
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                    });
                    return;
                }*/

                RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    ImageUrl = $"https://minecraftskinstealer.com/achievement/{icon}/Achievement+Get%21/{text.Replace(" ", "%20")}"
                });
            }
        }
    }
}
