using Discord;
using Discord.Commands;

namespace RamokSelfbot.Commands.Text
{
    [Command("boldunderlined", "Set your text to Bold Underlined - TEXT")]
    class boldcrossed : CommandBase
    {
        [Parameter("text")]
        public string text { get; set; }
        public override async void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                await Message.EditAsync(new MessageEditProperties()
                {
                    Content = $"__**{text}**__"
                });
            }
        }
    }
}
