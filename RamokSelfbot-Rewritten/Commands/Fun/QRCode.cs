using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("qrcode", "Create a QRCode with the text specified. - FUN")]
    class QRCodecmd : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);


                /*  RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                  {
                      ImageUrl 
                      Color = RamokSelfbot.Utils.EmbedColor(),
                      Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                  });*/

                qrCodeImage.Save(RamokSelfbot.Utils.GetFileName() + "\\qrcode.png");

                if (Message.Guild == null)
                {
                    var msg = Message.Channel.SendFile(RamokSelfbot.Utils.GetFileName() + "\\qrcode.png", "scan ça fdp", false);
                }
                else
                {

                    if (Program.client.GetCachedGuild(Message.Guild.Id).GetMember(Message.Author.User.Id).GetPermissions().Has(DiscordPermission.AttachFiles)) //CHECK DE PERMISSIONS
                    {
                        var msg = Message.Channel.SendFile(RamokSelfbot.Utils.GetFileName() + "\\qrcode.png", "scan ça fdp", false);
                    }
                    else
                    {
                        RamokSelfbot.Utils.Print("I cant send an image !");
                       
                    }
                }

                File.Delete(RamokSelfbot.Utils.GetFileName() + "\\qrcode.png");



            }
        }
    }
}
