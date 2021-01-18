using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Raid
{
    [Command("unbanall", "Unban all members of the guild - RAID")]
    class UnbanAll : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                id = Guid.NewGuid().ToString();
                RamokSelfbot.Utils.Print("Unbanall ID : " + id);
                foreach (var banned in Message.Guild.GetBans())
                {
                    Message.Guild.GetBan(banned.User.Id).Unban();
                    Log("User : " + banned.User.Username + "#" + banned.User.Discriminator);
                    Log("Reason of ban : " + banned.Reason);
                }
            }
        }

        private void Log(string log)
        {
            Colorful.Console.Write("\n\n[", Color.White);
            Colorful.Console.Write("UNBANALL (" + id + ")", Color.IndianRed);
            Colorful.Console.Write("] ", Color.White);
            Colorful.Console.WriteLine(log, Color.IndianRed);
        }

        public static string id;
    }
}
