using Discord;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;

namespace RamokSelfbot
{
    public class Utils
    {
        public static void Print(string text)
        {
            Console.Write("[");
            Colorful.Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second, Color.IndianRed);
            Console.Write("] ");
            Colorful.Console.WriteLine(text, Color.IndianRed);
        }

        public static EmbedFooter footer(DiscordUser user)
        {
            EmbedFooter footer = new EmbedFooter()
            {
                Text = "Selfbot rewritten by Ramok with <3"
            };

            if(user.Avatar != null)
            {
                footer.IconUrl = user.Avatar.Url;
            }
            return footer;
        }

        public static string GetFileName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().Location;
        }

        public static void ValidUser(int a, DiscordMessage Message)
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

            embed.Footer = footer(Message.Author.User);

            SendEmbed(Message, embed);
        }

        public static void SendEmbed(DiscordMessage Message, DiscordEmbed embed)
        {
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
                
                if (Program.client.GetCachedGuild(Message.Guild.Id).GetMember(Message.Author.User.Id).GetPermissions().Has(DiscordPermission.AttachFiles)) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                    return;
                } else
                {
                    Print("I cant send a embed !");
                    return;
                }
            }
        }


 /*       public static void EditEmbed(DiscordMessage Message, DiscordEmbed embed)
        {

        }*/

        public static Color EmbedColor()
        {
            JSON color = JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json"));
            return System.Drawing.Color.FromArgb(color.embedcolorr, color.embedcolorg, color.embedcolorb);
        }


    }
}
