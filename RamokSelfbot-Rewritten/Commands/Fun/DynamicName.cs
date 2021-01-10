using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Fun
{
    [Command("dynamicname", "Change ur nickname in the guild every 0,750s. - FUN")]
    class DynamicName : CommandBase
    {

        [Parameter("name", false)]
        public string Name { get; private set; }
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                Program.DynamicName = true;
                Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                thread.Start();
            }
        }

        private void WorkThreadFunction()
        {
            if (Program.DynamicName == true)
            {
                string currentNick = "";
                while (true)
                {
                    if (Program.DynamicName == true)
                    {
                        for (int i = 0; i < Name.Length; i++)
                        {
                            currentNick += Name[i];
                            if(Message.Author.Member.GetPermissions().Has(DiscordPermission.ChangeNickname))
                            {
                                Message.Guild.SetNickname(currentNick);
                            } else
                            {
                                RamokSelfbot.Utils.Print("U dont the permission \"DiscordPermission.ChangeNickname\" in the guild \"" + Client.GetCachedGuild(Message.Guild.Id).Name + "\", and this permission is needed for DynamicName command, disabling it!");
                                Program.DynamicName = false;
                            }
                            Thread.Sleep(750);
                        }

                        currentNick = "";
                    }
                    else
                    {
                        Thread.CurrentThread.Abort();
                    }
                }

            }
            else
            {
                Thread.CurrentThread.Abort();
            }

        }
    }
}
