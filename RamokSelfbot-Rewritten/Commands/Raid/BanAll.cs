using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace RamokSelfbot.Commands.Raid
{
    [Command("banall", "Ban all members of the server. - RAID")]
    class BanAll : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
               

                bool finished = false;
                while(finished == false)
                {
                    for (int i = 0; i < guild.MemberCount; i++)
                    {
                        var user = guild.GetMembers()[i];
                        if(user.User.Id != Program.id)
                        {
                            try
                            {
                                user.Ban("Ramok Selfbot > all", 7);
                                RamokSelfbot.Utils.Print("Banned " + user.User.Username + "#" + user.User.Discriminator);
                                if(i == guild.MemberCount)
                                {
                                    finished = true;
                                }
                            } catch
                            {
                                RamokSelfbot.Utils.Print("Cant ban " + user.User.Username + "#" + user.User.Discriminator);
                            }
                        }
                    }
                }
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
                      

                if (Message.Author.User.Id == Program.id)
                {
                    Console.WriteLine("error occured : " + exception.Message);
                }
            }
        }


    }

