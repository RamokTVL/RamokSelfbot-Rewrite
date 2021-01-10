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
    [Command("qrcode")]
    class QRCode : CommandBase
    {
        [Parameter("text")]
        public string text { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                string url = "https://api.qrserver.com/v1/create-qr-code/?size=512x512&data=" + text.Replace(" ", "%20");
                EmbedFooter footer = new EmbedFooter();
                if (Message.Author.User.Avatar.Url != null)
                {
                    footer.IconUrl = Message.Author.User.Avatar.Url;
                }

                footer.Text = "Selfbot rewritten by Ramok with <3";

                if(Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator) || Client.GetCachedGuild(Message.Guild.Id).OwnerId == Program.id) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new MessageEditProperties()
                    {
                        Content = "",
                        Embed = new EmbedMaker()
                        {
                            ImageUrl = url,
                            Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                            Footer = footer
                        },
                    });
                }

            }
        }
    }
}
