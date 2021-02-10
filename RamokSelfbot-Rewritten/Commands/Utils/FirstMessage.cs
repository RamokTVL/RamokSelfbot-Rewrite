using Discord.Commands;
using Discord;
using System;
using System.Linq;

namespace RamokSelfbot.Commands.Utils
{
    [Command("firstmessage", "Get the first message of the channel - UTILS")]
    class FirstMessage : CommandBase
    {
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                {
                    Title = "First Message",
                    Description = "[Jump](http://discord.com/channels/" + Message.Guild.Id + "/" + Message.Channel.Id + "/" + Message.Channel.GetMessages().Reverse().First().Id + ")"
                });
            }
        }
    }
}
