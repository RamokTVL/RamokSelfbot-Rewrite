using System.IO;
using System.Text;
using System;
using System.Drawing;
using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace RamokSelfbot
{
    class Program
    {
        public static DiscordSocketClient client = new DiscordSocketClient();
        public static ulong id = 1;
        static void Main(string[] args)
        {
            /* 
             * Coding started at : 01/09/21
             * V1 of rewrite ended at : ??/??/21
             */

            /*
             * Errors codes 
             * 
             * 444 = retard
             */
            Console.Title = "RamokSelfbot.";
            Console.WriteLine("If you paid this, you got scammed !\nIf u have errors, contact Ramok on Discord for enable Debug mode.\nRamok is not responsable of any Discord bans / guild bans / FTNL Detections.");
            System.Threading.Thread.Sleep(500);
            // BIG CONSOLE : Console.SetWindowSize(129, 30);
            bool verifiedtoken = false;
            TokenLogin:
              do
               {
                Console.Clear();
                token = "";
                if(File.Exists("token.txt"))
                {
                    token = File.ReadAllText("token.txt");
                } else
                {
                    Colorful.Console.Write("Token : ", Color.IndianRed);
                    token = Console.ReadLine();
                    if (token == "")
                    {
                        goto TokenLogin;
                    }
                }

                if(args.Length == 1)
                {
                    Program.formattedargs = args[0];
                } else
                {
                    Program.formattedargs = "no";
                }

                try
                {
                    string prefix = "!!";
                    if (!File.Exists("config.json"))
                    {
                        bool jsoncreated = false;
                        do
                        {
                            Console.Clear();
                            Colorful.Console.WriteLine("Prefix : ", Color.IndianRed);
                            prefix = Console.ReadLine();
                            if (prefix == "")
                            {
                                Environment.Exit(444);
                            }
                            if(!File.Exists("config.json"))
                            {

                                JSON Config = JsonConvert.DeserializeObject<JSON>(new WebClient().DownloadString("https://pastebin.com/raw/wxj7SEgx"));
                                Config.prefix = prefix;
                                string configoutput = JsonConvert.SerializeObject(Config, Formatting.Indented);
                                File.AppendAllText("config.json", configoutput);
                            }
                        } while (jsoncreated);
                    }


                }
                catch { }

                client.OnLoggedIn += Client_OnLoggedIn;
                client.CreateCommandHandler(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).prefix);
                try
                {
                    client.Login(token);
                    if (!File.Exists("token.txt"))
                    {
                        File.WriteAllText("token.txt", token);
                    } else
                    {
                        if (File.ReadAllText("token.txt") == null)
                        {
                            File.WriteAllText("token.txt", token);
                        }
                    }
                } catch
                {
                    goto TokenLogin;
                }
                System.Threading.Thread.Sleep(-1);
                verifiedtoken = true;
            } while (verifiedtoken == false);
            
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {



            new Thread(new ThreadStart(time.Start)).Start();
            //Console.Clear();
            Colorful.Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            Colorful.Console.WriteLine($"╠--> Username : {args.User.Username}#{args.User.Discriminator} ({args.User.Id})", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            if(client.User.PhoneNumber == null)
            {
                Colorful.Console.WriteLine($"╠--> No phone number", Color.MediumSlateBlue);
            } else
            {
                Colorful.Console.WriteLine($"╠--> Phone number : {args.User.PhoneNumber}", Color.MediumSlateBlue);
            }
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            Colorful.Console.WriteLine($"╠--> Hypesquad : {args.User.Hypesquad}", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            Colorful.Console.WriteLine($"╠--> Nitro : {args.User.Nitro}", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            Colorful.Console.WriteLine($"╠--> Guilds : {args.Guilds.Count}", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            Colorful.Console.WriteLine($"╠--> Relationships : {args.Relationships.Count}", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            if(formattedargs.Contains("-showtoken"))
            {
                Colorful.Console.WriteLine($"╠--> Token : {token}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
                
            } 
            Colorful.Console.WriteLine($"╠--> Email : {args.User.Email} (Verified : {args.User.EmailVerified})", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            Colorful.Console.WriteLine($"╠--> Prefix : {JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).prefix}", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╘══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);

            id = args.User.Id;

            Console.WriteLine();
            Colorful.Console.WriteLine("Thanks for using RamokSelfbot !", Color.MediumSlateBlue);
            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("=>", Color.MediumSlateBlue);

            Restarted();
        }

        private static void Restarted()
        {
            throw new NotImplementedException("I WILL ADD IT LATER");
            return;
            if (File.Exists("restarted.ramokselfbot"))
            {
                RestartJson json = JsonConvert.DeserializeObject<RestartJson>(File.ReadAllText("restarted.ramokselfbot"));
                ulong channelid = json.channelid;
                ulong msgid = json.msgid;
                if (Program.Debug)
                {
                    Console.WriteLine(channelid);
                    Console.WriteLine(msgid);
                }
                DiscordMessage Message;
                try
                {
                    Message = client.CrosspostMessage(channelid, msgid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Title = "Restarted !",
                    Footer = new EmbedFooter() { Text = "Restarted : " + DateTime.Now.Day.ToString() + " / " + DateTime.Now.Month.ToString() + " / " + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() }
                };

                if (Message.Author.User.Avatar != null)
                {
                    embed.Footer = new EmbedFooter() { Text = embed.Footer.Text, IconUrl = Message.Author.User.Avatar.Url };
                }



                if (Message.Guild == null)
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
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
                    }
                    else
                    {
                        Message.Edit(new MessageEditProperties()
                        {
                            Content = "Restarted (not enough permissions for embed)"
                        });
                    }
                }

                File.Delete("restarted.ramokselfbot");
            }
        }
        public static string formattedargs { get; set; }
        public static string token { get; set; }

        public static bool Debug = true;
        public static bool DynamicName = true;

        public static Stopwatch time = new Stopwatch();
    }

    public class JSON
    {
        public string prefix { get; set; }
        public string tenorkey { get; set; }
        public string giphykey { get; set; }
        public int embedcolorr { get; set; }
        public int embedcolorb { get; set; }
        public int embedcolorg { get; set; }
    }

    public class RestartJson
    {
        public ulong channelid { get; set; }
        public ulong msgid { get; set; }
    }
}
