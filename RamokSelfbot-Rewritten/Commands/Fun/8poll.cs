using Discord.Commands;
using Discord;
using System;

namespace RamokSelfbot.Commands.Fun
{
    [Command("8poll", "Answers all your questions.")]
    class _8poll : CommandBase
    {
        [Parameter("question")]
        public string question { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                string[] responses = new String[11];
                responses[0] = "I don't know";
                responses[1] = "Yes";
                responses[2] = "No.";
                responses[3] = "Sorry I veski";
                responses[4] = "It doesn't matter";
                responses[5] = "I don't mind";
                responses[6] = "I can't answer this question";
                responses[7] = "sorry but i think you're gay";
                responses[8] = "Yeah!";
                responses[9] = "I don't want to bless you so I will not respond";
                responses[10] = "You wouldn't like my answer";

                Message.Edit(new MessageEditProperties()
                {
                    Content = "> " + question + "\n- " + responses[new Random().Next(0, responses.Length)]
                });
            }
        }
    }
}
