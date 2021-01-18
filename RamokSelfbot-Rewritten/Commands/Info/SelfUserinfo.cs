using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Info
{
    [Command("selfuserinfo", "DANGEROUS COMMAND ! (TOKEN LEAK) - INFO")]
    class SelfUserinfo : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Description = "Please delete it instant if the channel is not private.\nYour account can be stealed with theses informations.",
                    Title = "Self-informations"
                };

                embed.AddField("Username", $"```\n{Client.User.Username}#{Client.User.Discriminator}```", true);
                embed.AddField("ID", "```\n" + Client.User.Id.ToString() + "```", true);

                if (Message.Author.User.Avatar.Url != null)
                {
                    embed.ThumbnailUrl = Message.Author.User.Avatar.Url;
                    string avatarurl = new WebClient().DownloadString("https://tinyurl.com/api-create.php?url=" + Message.Author.User.Avatar.Url);
                    embed.AddField("Avatar URL", "```\n" + avatarurl + "```", true);
                }

                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                embed.AddField("Token", "```\n" + Program.token + "```", false);
                if (Client.User.PhoneNumber != null)
                {
                    embed.AddField("Phone number", "```\n" + Client.User.PhoneNumber + "```", false);
                }
                embed.AddField("Numbers of servers", "```\n" + Client.GetGuilds().Count.ToString() + "```", true);
                embed.AddField("Numbers of friends", "```\n" + Client.GetRelationships().Count.ToString() + "```", true);
                string nitro = "";
                if (Client.User.Nitro.ToString() == "None")
                {
                    nitro = "No nitro";
                }
                else if (Client.User.Nitro.ToString() == "Classic")
                {
                    nitro = "Nitro Classic";
                }
                else if (Client.User.Nitro.ToString() == "Nitro")
                {
                    nitro = "Nitro Boost";
                }
                else
                {
                    nitro = "Unknown";
                }

                embed.AddField("Nitro", "```\n" + nitro + "```", false);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if(Message.Author.User.Id == Program.id)
            {
                Console.WriteLine("error occured : " + exception.Message);
            }
        }
    }
}
