using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("qrcode", "Create a QRCode with the text specified. - FUN")]
    class QRCode : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                string url = "https://api.qrserver.com/v1/create-qr-code/?size=512x512&data=" + text.Replace(" ", "%20");
                RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                {
                    ImageUrl = url,
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                });



            }
        }
    }
}
