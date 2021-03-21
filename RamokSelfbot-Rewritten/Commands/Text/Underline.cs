using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("underline", "Underline the text - TEXT")]
    class underline : CommandBase
    {
        [Parameter("text", false)]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"__{text}__"
                });
            }
        }
    }
}
