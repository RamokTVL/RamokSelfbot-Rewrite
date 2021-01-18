using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("say", "Say a message in a embed. - FUN")]
    class Say : CommandBase
    {
        [Parameter("message")]
        public string message { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Description = message,
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if(Message.Author.User.Id == Program.id)
            {
                if(providedValue == null)
                {
                    Message.Delete();
                    RamokSelfbot.Utils.Print("Please provide a text for say command !");
                }
            }
        }
    }
}
