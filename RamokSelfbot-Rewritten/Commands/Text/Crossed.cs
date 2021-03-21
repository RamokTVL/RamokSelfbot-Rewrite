using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("crossed", "Set your text on crossed - TEXT")]
    class Crossed : CommandBase
    {
        [Parameter("text")]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"~~{text}~~"
                });
            }
        }
    }
}
