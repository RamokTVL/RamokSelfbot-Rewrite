using Discord;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Management;

namespace RamokSelfbot
{
    public class Utils
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy/MM/dd HH:mm");
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static JSON DumpConfig()
        {
            if(File.Exists(GetFileName() + "\\config.json"))
            {
                JSON config = JsonConvert.DeserializeObject<JSON>(File.ReadAllText(GetFileName() + "\\config.json"));
                return config;
            } else
            {
                return null;
            }
        }

        public static string GetGuid()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            mbsList = mos.Get();
            string processorId = string.Empty;
            foreach (ManagementBaseObject mo in mbsList)
            {
                processorId = mo["ProcessorID"] as string;
            }

            mos = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");
            mbsList = mos.Get();
            string systemId = string.Empty;
            foreach (ManagementBaseObject mo in mbsList)
            {
                systemId = mo["UUID"] as string;
            }

            var compIdStr = $"{processorId}{systemId}";
            return compIdStr;
        }

        public static void Print(string text)
        {
            Console.Write("[");
            Colorful.Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second, Color.IndianRed);
            Console.Write("] ");
            Colorful.Console.WriteLine(text, Color.IndianRed);
        }

        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static EmbedFooter footer(DiscordUser user)
        {
            EmbedFooter footer = new EmbedFooter()
            {
                Text = RamokSelfbot.Footer.embed_text
            };

            if(user.Avatar != null)
            {
                footer.IconUrl = user.Avatar.Url;
            }
            return footer;
        }

        public static bool IsClient(DiscordMessage msg)
        {
            if(msg.Author.User.Id == Program.id)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static string GetFileName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().Location.Remove(System.Reflection.Assembly.GetEntryAssembly().Location.Length - 27, 27);
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
                if (Program.Debug)
                {
                    Console.WriteLine("embed sent");
                }
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
                    if (Program.Debug)
                    {
                        Console.WriteLine("embed sent");
                    }
                    return;
                } else
                {
                    Print("I cant send a embed !");
                    return;
                }
            }
        }

        public static ulong SendEmbedRsendIdget(DiscordMessage Message, DiscordEmbed embed)
        {
            if (Message.Guild == null)
            {
                var msg = Message.Channel.SendMessage("", false, embed);
                return msg.Id;
            }
            else
            {

                if (Program.client.GetCachedGuild(Message.Guild.Id).GetMember(Message.Author.User.Id).GetPermissions().Has(DiscordPermission.AttachFiles)) //CHECK DE PERMISSIONS
                {
                    var msg = Message.Channel.SendMessage("", false, embed);
                    return msg.Id;
                }
                else
                {
                    Print("I cant send a embed !");
                    return 00;
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

        public class Field
        {
            public string name { get; set; }
            public string value { get; set; }
            public bool inline { get; set; }
        }

        public class Embed
        {
            public string description { get; set; }
            public int color { get; set; }

            public Footer footer { get; set; }
            public List<Field> fields { get; set; }
        }

        public class Footer
        {
            public string text { get; set; }
            public string icon_url { get; set; }
        }

        public class Root
        {
            public string username { get; set; }
            public string avatar_url { get; set; }
            public List<Embed> embeds { get; set; }
        }
    }

    public class Footer
    {
        public static string embed_text = "Selfbot rewritten by Ramok with <3";
    }
}
