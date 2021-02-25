using Discord.Commands;
using Discord;

namespace RamokSelfbot.Commands.Hash
{
    [Command("base64encode", "Encode a text to base64 - HASH")]
    class Base64Encode : CommandBase
    {
        [Parameter("text")]
        public string text { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                string encoded = RamokSelfbot.Utils.Base64Encode(text);
                EmbedMaker embed = new EmbedMaker()
                {
                    Description = encoded,
                    Title = "Plain text to Base 64",
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Color = RamokSelfbot.Utils.EmbedColor()
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
