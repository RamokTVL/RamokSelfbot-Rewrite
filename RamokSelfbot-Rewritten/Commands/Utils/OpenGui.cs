using Discord.Commands;
using System.Threading;

namespace RamokSelfbot.Commands.Utils
{
    [Command("opengui", "Open the GUI for change settings - UTILS")]
    class OpenGui : CommandBase
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
            new Forms.GUI().ShowDialog();
        }
    }
}
