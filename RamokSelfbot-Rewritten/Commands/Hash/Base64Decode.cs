using Discord.Commands;
using Discord;

namespace RamokSelfbot.Commands.Hash
{
    [Command("base64decode", "Decode a base64 string to plain text - HASH")]
    class Base64Decode : CommandBase
    {
        [Parameter("text")]
        public string text { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                string decoded = RamokSelfbot.Utils.Base64Decode(text);
                EmbedMaker embed = new EmbedMaker()
                {
                    Description = decoded,
                    Title = "Base 64 to Plain text",
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Color = RamokSelfbot.Utils.EmbedColor()
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
