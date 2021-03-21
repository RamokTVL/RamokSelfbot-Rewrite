using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("italicunderlined", "Set your text to italic underlined - TEXT")]
    class ItalicUnderlined : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"__*{text}*__"
                });
            }
        }
    }
}
