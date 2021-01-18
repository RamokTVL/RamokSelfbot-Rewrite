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


            EmbedMaker embed = new EmbedMaker()
            {
                Color = RamokSelfbot.Utils.EmbedColor(),
                Description = "The color is in RGB format.",
                Footer = RamokSelfbot.Utils.footer(Message.Author.User)
            };

            embed.AddField("R", JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json")).embedcolorr.ToString(), true);
            embed.AddField("G", JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json")).embedcolorg.ToString(), true);
            embed.AddField("B", JsonConvert.DeserializeObject<RamokSelfbot.JSON>(File.ReadAllText("config.json")).embedcolorb.ToString(), true);

            RamokSelfbot.Utils.SendEmbed(Message, embed);
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
