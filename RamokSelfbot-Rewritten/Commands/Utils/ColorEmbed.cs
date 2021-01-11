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

namespace RamokSelfbot.Commands.Utils
{
    [Command("colorembed", "Change the colors of the embed - UTILS")]
    class ColorEmbed : CommandBase
    {
        [Parameter("color(r)", false)]
        public string colorr { get; set; }

        [Parameter("color(g)", false)]
        public string colorg { get; set; }

        [Parameter("color(b)", false)]
        public string colorb { get; set; }
        public override void Execute()
        {
            RamokSelfbot.JSON config = JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json"));
            config.embedcolorr = int.Parse(colorr);
            config.embedcolorg = int.Parse(colorg);
            config.embedcolorb = int.Parse(colorb);
            string output = JsonConvert.SerializeObject(config, Formatting.Indented);

            File.WriteAllText("config.json", output);

            EmbedFooter footer = new EmbedFooter();
            if (Message.Author.User.Avatar.Url == null)
            {
                footer.IconUrl = Message.Author.User.Avatar.Url;
            }

            footer.Text = "Selfbot rewritten by Ramok with <3";

            EmbedMaker embed = new EmbedMaker()
            {
                Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                Description = "The color is in RGB format.",
                Footer = footer
            };

            embed.AddField("R", JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json")).embedcolorr.ToString(), true);
            embed.AddField("G", JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json")).embedcolorg.ToString(), true);
            embed.AddField("B", JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json")).embedcolorb.ToString(), true);

            if (Message.Guild == null)
            {
                Message.Edit(new Discord.MessageEditProperties()
                {
                    Content = "",
                    Embed = embed
                });
                return;
            }
            else
            {
                if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator)) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                    return;
                }
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
                      
                if (Message.Author.User.Id == Program.id)
                {
                if(providedValue == null)
                {
                    RamokSelfbot.Utils.Print("Please provide somes valid RGB strings.\nExemple : " + Client.CommandHandler.Prefix + "colorembed 0 142 255");
                }
                    Console.WriteLine("\nerror occured : " + exception.Message);
                }
         }
        
    }
}
