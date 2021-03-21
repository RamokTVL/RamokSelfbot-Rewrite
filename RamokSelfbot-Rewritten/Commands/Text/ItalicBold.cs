using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("italicbold", "Set your text on italic bold - TEXT")]
    class ItalicBold : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"***{text}***"
                });
            }
        }
    }
}
