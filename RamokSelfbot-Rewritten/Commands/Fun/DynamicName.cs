using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("dynamicname", "Change the name of the person specified every 0,750s with the name specified. - FUN")]
    class DynamicName : CommandBase
    {
        [Parameter("id", true)]
        public string ID { get; private set; }

        [Parameter("name", false)]
        public string Name { get; private set; }


        public static DiscordUser user;
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();
                user = Client.GetUser(Program.id);
                if (ID == null)
                {
                    ValidUser(1);
                    return;
                }
                else
                {
                    if (ID.Length == 18)
                    {
                        try
                        {
                            user = Client.GetUser(ulong.Parse(ID));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Unknown User"))
                            {
                                ValidUser(3);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else if (Message.Mentions.Count > 1 || Message.Mentions.Count == 1)
                    {
                        try
                        {
                            user = Client.GetUser(Message.Mentions[0].Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            if (ex.Message.Contains("Unknown User"))
                            {
                                ValidUser(3);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        ValidUser(2);
                        return;
                    }
                    Program.DynamicName = true;
                    Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                    thread.Start();
                }
            }




        }

        public void WorkThreadFunction()
        {
            if (Program.DynamicName == true)
            {
                string currentNick = "";
                while (Program.DynamicName)
                {
                    for (int i = 0; i < Name.Length; i++)
                    {
                        currentNick += Name[i];
                        if (Message.Author.Member.GetPermissions().Has(DiscordPermission.ManageNicknames))
                        {
                            Message.Guild.SetNickname(currentNick);
                        }
                        else
                        {
                            RamokSelfbot.Utils.Print("U dont the permission \"DiscordPermission.ManageNicknames\" in the guild \"" + Client.GetCachedGuild(Message.Guild.Id).Name + "\", and this permission is needed for DynamicName command, disabling it!");
                            Program.DynamicName = false;
                        }
                        Thread.Sleep(750);
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
        private void ValidUser(int a)
        {
            if (Program.Debug == true)
            {
                Console.WriteLine(a.ToString());
            }

            EmbedMaker embed = new EmbedMaker()
            {
                Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                Description = "<@" + Message.Author.User.Id.ToString() + ">, please mention a valid user !"
            };

            if (Message.Author.User.Avatar.Url == null)
            {
                embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3" };
            }
            else
            {
                embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3", IconUrl = Message.Author.User.Avatar.Url };
            }

            if (Message.Guild == null)
            {
                Message.Edit(new Discord.MessageEditProperties()
                {
                    Content = "",
                    Embed = embed
                });
                return;
            }
            else
            {
                if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator)) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                    return;
                }
            }
        }
    }
}
