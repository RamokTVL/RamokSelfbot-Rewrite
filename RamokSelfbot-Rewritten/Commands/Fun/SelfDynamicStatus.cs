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
    [Command("selfdynamicstatus", "Change ur status every 0,750s. - FUN")]
    class SelfDynamicStatus : CommandBase
    {

        [Parameter("emoji", false)]
        public string emoji { get; private set; }

        [Parameter("name", false)]
        public string Name { get; private set; }     


        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                Program.DynamicStatus = true;
                Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                thread.Start();
            }
        }

        private void WorkThreadFunction()
        {
            if (Program.DynamicStatus == true)
            {
                string currentNick = "";
                while (Program.DynamicStatus)
                {
                        for (int i = 0; i < Name.Length; i++)
                        {
                            currentNick += Name[i];
            
                    
                        Client.User.ChangeSettings(new UserSettingsProperties()
                        {
                            Theme = Client.UserSettings.Theme,
                            DeveloperMode = Client.UserSettings.DeveloperMode,
                            CustomStatus = new CustomStatus()
                            {
                                EmojiName = emoji,
                                Text = currentNick
                            }
                        });
                            
                            Thread.Sleep(500);
                        }

                        currentNick = "";
                    
                }
                Thread.CurrentThread.Abort();
            }
            else
            {
                Thread.CurrentThread.Abort();
            }

        }
    }
}
