using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("italicboldunderlined", "Set your text on Itatic Bold Underlined - TEXT")]
    class italicboldunderlined : CommandBase
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
