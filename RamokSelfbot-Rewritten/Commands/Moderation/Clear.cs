using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Moderation
{
    [Command("clear", "Delete the number of messages specified - MOD")]
    class Clear : CommandBase
    {
        [Parameter("number of messages to delete", true)]
        public int number { get; set; }
        public override void Execute()
        {
            
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                Message.Delete();
                int a = 0;
                try
                {
                    a = number;
                } catch { }

                if(a == 0)
                {
                    a = 25;
                }


                if(Message.Guild != null)
                {
                    if(Message.Author.Member.GetPermissions().Has(DiscordPermission.ManageMessages))
                    {
                        if(Message.Channel.GetMessages().Count < a)
                        {
                            for (int i = 0; i < Message.Channel.GetMessages().Count; i++)
                            {
                                try
                                {
                                    Client.DeleteMessage(Message.Channel.Id, Message.Channel.GetMessages().First().Id);
                                }
                                catch
                                {
                                    RamokSelfbot.Utils.Print("Error while deleting a message");
                                }
                            }
                        } else
                        {
                            for (int i = 0; i < a; i++)
                            {
                                try
                                {
                                    Client.DeleteMessage(Message.Channel.Id, Message.Channel.GetMessages().First().Id);
                                }
                                catch
                                {
                                    RamokSelfbot.Utils.Print("Error while deleting a message");
                                }
                            }
                        }

                    } else
                    {
                        RamokSelfbot.Utils.Print("Not enough permissions");
                        return;
                    }
                } else
                {
                    RamokSelfbot.Utils.Print("Command avaliable for guild only !");
                    return;
                }
            }
        }
    }
}
