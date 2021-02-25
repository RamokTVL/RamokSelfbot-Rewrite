using Discord.Commands;
using System.Threading;

namespace RamokSelfbot.Commands.Utils
{
    [Command("hypesquad", "Allow you to change your hypesquad - UTILS")]
    class Hypesquad : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                    Message.Delete();
                    new Thread(new ThreadStart(OpenForm)).Start();
            }
        }

        public static void OpenForm()
        {
            new Forms.Hypesquad().ShowDialog();
        }
    }
}
