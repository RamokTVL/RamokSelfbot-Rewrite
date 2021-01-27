using Discord.Commands;
using Discord;
using System;
using Discord.Gateway;
using System.IO;

namespace RamokSelfbot.Commands.Fun
{
    [Command("getguilduserspictures", "Download all profiles pictures of the server - FUN")]
    class getguilduserspictures : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                SocketGuild guild = Client.GetCachedGuild(Message.Guild.Id);
                Directory.CreateDirectory("avatar");

                while(finished != true)
                {
                    for (i = 0; i < guild.MemberCount; i++)
                    {
                        var user = guild.GetMember(guild.GetMembers()[i]);
                        try
                        {
                            if (user.User.Avatar != null)
                            {
                                new System.Net.WebClient().DownloadFile(user.User.Avatar.Url, "avatar\\" + user.User.Id + ".gif");
                            } else
                            {
                                File.Copy("ressources\\discord-logo.jpg", "avatar\\" + user.User.Id + ".jpg");
                            }

                            RamokSelfbot.Utils.Print("Downloaded " + i + "/" + guild.MemberCount + " profile pictures!");
                            if (i == guild.MemberCount - 1)
                            {
                                RamokSelfbot.Utils.Print("Finished work");
                                finished = true;
                            }

                        } catch { 
                        }


                    }
                }
            }
        }



        public int i = 0;
        public bool finished = false;
    }
}
