using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("italic", "Set your text to italic - TEXT")]
    class Italic : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"*{text}*"
                });
            }
        }
    }
}

