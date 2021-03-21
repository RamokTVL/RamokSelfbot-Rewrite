using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("bold", "Set the text to bold! - TEXT")]
    class Bold : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"**{text}**"
                });
            }
        }
    }
}
