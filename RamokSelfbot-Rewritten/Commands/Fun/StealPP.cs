using Discord;
using Discord.Commands;
using Discord.Media;
using Discord.WebSockets;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Drawing.Imaging;

namespace RamokSelfbot.Commands.Fun
{
    [Command("stealpp", "Give you the profile picture of a another member. - FUN")]
    class StealPP : CommandBase
    {
        [Parameter("id or link of a image")]
        public string id { get; set; }
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                Message.Delete();

                DiscordUser user = Client.GetUser(Program.id);
                if (id == null)
                {
                    RamokSelfbot.Utils.ValidUser(1, Message);
                    return;
                }
                else
                {
                    if (id.Length == 18)
                    {
                        try
                        {
                            user = Client.GetUser(ulong.Parse(id));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Unknown User"))
                            {
                                RamokSelfbot.Utils.ValidUser(3, Message);
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
                                RamokSelfbot.Utils.ValidUser(4, Message);
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

                        if(id.Contains("http"))
                        {
                            WebClient client = new WebClient();
                            Stream stream = client.OpenRead(id);

                            Client.User.ChangeProfile(new UserProfileUpdate()
                            {
                                Avatar = System.Drawing.Bitmap.FromStream(stream)
                            });

                            Client.User.Update();

                            stream.Flush();
                            stream.Close();
                            client.Dispose();
                        } else
                        {
                            RamokSelfbot.Utils.ValidUser(2, Message);
                            return;
                        }


                    }
                }

                Client.User.ChangeProfile(new UserProfileUpdate()
                {
                    Avatar = user.Avatar.Download(DiscordCDNImageFormat.Any)
                });

                Client.User.Update();

            }
        }
    }
}
