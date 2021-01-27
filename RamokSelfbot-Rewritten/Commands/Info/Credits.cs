using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net;

namespace RamokSelfbot.Commands.Info
{
    [Command("credits", "Show the credits of the selfbot - INFO")]
    class Credits : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "This still updated !"
                };

                CreditsJson credits = JsonConvert.DeserializeObject<CreditsJson>(new WebClient().DownloadString("https://pastebin.com/raw/rk0i4EiT"));

                embed.AddField("Twitter", "```\n" + credits.twitter + "```", false);
                embed.AddField("Youtube", "```\n" + credits.youtube + "```", false);
                embed.AddField("Github", "```\n" + credits.git + "```", false);
                embed.AddField("Telegram", "```\n" + credits.telegram + "```", false);
                embed.AddField("Discord", "```\n" + credits.Discord + "```", false);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }

    }
    class CreditsJson
    {
        public string twitter { get; set; }
        public string youtube { get; set; }
        public string git { get; set; }
        public string telegram { get; set; }
        public string Discord { get; set; }
    }
}
