using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.IO;

namespace RamokSelfbot.Commands.Info
{
    [Command("uptime", "GIVE THE UPTIME OF THE BOT - INFO")]
    class Uptime : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Description = "😎 Bot uptime since " + Program.time.Elapsed.Days + "days " + Program.time.Elapsed.Hours + "hours " + Program.time.Elapsed.Minutes + "minutes " + Program.time.Elapsed.Seconds + " secondes." 
                };

                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                RamokSelfbot.Utils.SendEmbed(Message, embed);

            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
                        {
                base.HandleError(parameterName, providedValue, exception);
                if (Message.Author.User.Id == Program.id)
                {
                    Console.WriteLine("error occured : " + exception.Message);
                }
            }
        }
    }
}
