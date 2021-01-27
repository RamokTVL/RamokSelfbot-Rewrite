using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net;

namespace RamokSelfbot.Commands.NSFW
{
    [Command("hkitsune", "Send a NSFW image - NSFW")]
    class Hkitsune : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                if (JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json")).nsfw)
                {
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "ooh",
                        ImageUrl = JsonConvert.DeserializeObject<NSFWOPTIONS>(new WebClient().DownloadString("https://nekobot.xyz/api/image?type=hkitsune")).message
                    };

                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                }
                else
                {
                    RamokSelfbot.Utils.Print("This is a nsfw command ! U need to enable nsfw in config.json or GUI to use this command !");
                    Message.Delete();
                    return;
                }
            }
        }
    }
}
