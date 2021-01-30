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
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace RamokSelfbot
{
    class Program
    {
        public static DiscordSocketClient client = new DiscordSocketClient();
        public static ulong id = 1;
        static void Main(string[] args)
        {
            if(Program.Debug)
            {
                Console.WriteLine(RamokSelfbot.Utils.GetFileName());
            }
            /* 
             * Coding started at : 01/09/21
             * V1 of rewrite ended at : ??/??/21
             */

            /*
             * Errors codes 
             * 
             * 444 = retard
             * 999 = no internet
             */

            /*Credits
             * Nitro Sniper credits : https://github.com/Stanley-GF/StanSniper by Stanley-GF 
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
                client.OnMessageReceived += Client_OnMessageReceived;
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




        private static string removebefore(string text)
        {
            String orgText = text;
            int i = orgText.IndexOf("gift/");
            if (i != -1)
            {
                text = orgText.Remove(0, i);
            }
            return text;
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            JSON config = JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json"));

            if (args.Message.Author.User.Id == Program.id)
            {
                MSGSent++;
            } else
            {
                MSGRecieved++;
            }

            if (config.nitrosniper == true)
            {
                //NITRO SNIPER
                var message = args.Message.Content.ToString();
                if (message.Contains("gift/"))
                {
                    string gift = removebefore(message);
                    string finalgift = gift = gift.Split()[0];
                    finalgift = finalgift.Replace("gift/", "");

                    try
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        client.RedeemGift(finalgift);
                        sw.Stop();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("] ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Succesfully redeemed gift  - discord.gift/" + finalgift + " (" + sw.ElapsedMilliseconds + " ms)");
                    }
                    catch (Exception)
                    {
                        try
                        {
                            if (client.GetGift(finalgift).Consumed == true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("[");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("] ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Gift already claimed from - discord.gift/" + finalgift + Environment.NewLine);
                            }
                            else
                            {

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("[");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("] ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Error redeeming gift - discord.gift/" + finalgift + Environment.NewLine);

                            }
                        }
                        catch (Exception)
                        {


                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("] ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Unknow Gift - discord.gift/" + finalgift + Environment.NewLine);
                        }



                    }
                }
            }

            if(args.Message.MentionedEveryone)
            {
                if(config.antieveryone)
                {
                    args.Message.Acknowledge();
                }
            } 

         


        }
        private static void CheckInternet()
        {

            while(true)
            {
                System.Threading.Thread.Sleep(7500);
                if (PingHost("google.fr") == false)
                {
                    RamokSelfbot.Utils.Print("Connexion lost !");
                    RamokSelfbot.Utils.Print("Closing selfbot.");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(999);
                }
            }
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
                if(pingable)
                {
                    if (Program.Debug)
                    {
                        Console.WriteLine(reply.RoundtripTime.ToString() + "ms");
                    }
                } else
                {
                    if(Program.Debug)
                    {
                        Console.WriteLine("cannot ping");
                    }
                }
            }
            catch (PingException)
            {

            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        private static void ModificationDetected(int detection)
        {
            MessageBox.Show("We've detected " + detection.ToString() + " errors in ressources files of the selfbot !\nPlease redownload the selfbot or you can get bugs", "ERROR²", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Process.Start("https://github.com/RamokTVL/RamokSelfbot-Rewritten");
            Environment.Exit(444);
        }

        private static void VerifyMD5()
        {
            int detection = 0;
            if(File.Exists("ressources\\restarted.ramokselfbot.exemple"))
            {
                if (MD5File("ressources\\restarted.ramokselfbot.exemple").ToString().ToLower() != "b0a8efe3829852b460c9052f7d04d447")
                {
                    detection++;
                    Console.WriteLine("1 " + MD5File("ressources\\restarted.ramokselfbot.exemple").ToString().ToLower());
                }
            } else
            {
                detection++;
                Console.WriteLine("2");
            }

            if(File.Exists("ressources\\discord-logo.jpg"))
            {
                if(MD5File("ressources\\discord-logo.jpg").ToString().ToLower() != "aa9f817a0a7f8c5b4ea03cf5261b4c5b")
                {
                    detection++;
                    Console.WriteLine("3 " + MD5File("ressources\\discord-logo.jpg").ToString().ToLower());
                }
            } else
            {
                detection++;
                if(Debug)
                {
                    Console.WriteLine("4");
                }
            }

            if(detection != 0)
            {
                ModificationDetected(detection);
            }

            Thread.CurrentThread.Abort();
            
        }
        private static string MD5File(string filename)
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

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Program.Debug = JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).debug;

            new Thread(new ThreadStart(CheckInternet)).Start();
            new Thread(new ThreadStart(VerifyMD5)).Start();
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
            Colorful.Console.WriteLine($"╠--> Debug : {Program.Debug}", Color.MediumSlateBlue);
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
            Colorful.Console.WriteLine("Logs =>\n", Color.MediumSlateBlue);
            Restarted();
            if (JsonConvert.DeserializeObject<Details>(new WebClient().DownloadString("https://ramokselfbot.netlify.app/api/v1/ramokselfbot/details.json")).version != FileVersionInfo.GetVersionInfo(RamokSelfbot.Utils.GetFileName()).FileVersion)
            {
                DialogResult notuptodate = MessageBox.Show("Its looks like there is a new update, do you want to do it ?", "Auto Updator", MessageBoxButtons.YesNo);
                if(notuptodate == DialogResult.Yes)
                {
                    Process.Start(JsonConvert.DeserializeObject<Details>(new WebClient().DownloadString("https://ramokselfbot.netlify.app/api/v1/ramokselfbot/details.json")).link);
                } 
            }

            
        }

        private static void Restarted()
        {
         
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

             


                EmbedMaker embed = new EmbedMaker()
                   {
                       Color = RamokSelfbot.Utils.EmbedColor(),
                       Title = "Restarted !",
                       Footer = new EmbedFooter() { Text = "Restarted : " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() }
                   };


               

                if (json.guildid != 003)
                {
                    
                    client.EditMessage(channelid, msgid, new MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                    File.Delete("restarted.ramokselfbot");
                    return;
                }
                else
                {
                   
                    if (Program.client.GetCachedGuild(json.channelid).GetMember(client.User.Id).GetPermissions().Has(DiscordPermission.AttachFiles)) //CHECK DE PERMISSIONS
                    {
                        client.EditMessage(channelid, msgid, new MessageEditProperties()
                        {
                            Content = "",
                            Embed = embed
                        });
                        File.Delete("restarted.ramokselfbot");
                        return;
                    }
                    else
                    {
                        RamokSelfbot.Utils.Print("I cant send a embed !");
                        File.Delete("restarted.ramokselfbot");
                        return;
                    }
                }

            


            }
        
        }
        public static string formattedargs { get; set; }
        public static string token { get; set; }


        public static ulong MSGSent;
        public static ulong MSGRecieved;

        public static bool Debug = true;
        public static bool DynamicName = true;

        public static Stopwatch time = new Stopwatch();
    }

    public class JSON
    {
        public string prefix { get; set; }

        public bool experimentalcommands { get; set; }
        public bool nitrosniper { get; set; }
        public bool antieveryone { get; set; }
        public bool nsfw { get; set; }
        public bool debug { get; set; }
        public string twitchlink { get; set; }
        public string youtubeapikey { get; set; }
        public int embedcolorr { get; set; }

        public int embedcolorg { get; set; }
        public int embedcolorb { get; set; }

    }

    public class RestartJson
    {
        public ulong channelid { get; set; }
        public ulong msgid { get; set; }
        public ulong guildid { get; set; }
    }

    public class Details
    {
        public string version { get; set; }
        public string link { get; set; }
    }
}
