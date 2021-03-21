using System.IO;
using System;
using System.Drawing;
using Discord;
using Discord.Gateway;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Leaf.xNet;
using RamokSelfbot.Commands.Info;
using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;

namespace RamokSelfbot
{
    class Program
    {
        public static DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig
        {
            ApiVersion = 6,
            RetryOnRateLimit = true
        });

        public static ulong id = 1;

        static void Main(string[] args)
        {

            if (args.Length > 1 || args.Length == 1)
            {
                foreach (var elements in args)
                {
                    Program.formattedargs += elements + " ";
                }
            }
            else
            {
                Program.formattedargs = "";
            }

            if (formattedargs.Contains("/?"))
            {
                Colorful.Console.Write("Project created by RamokTV\nNitro sniper credits : https://github.com/Stanley-GF/StanSniper by Stanley-GF\nBypass service credits : bypass-shorteners.herokuapp.com by IDRALOU#6966\n\n\n", Color.MediumPurple);
                Colorful.Console.Write("Avaliable arguments start function : \n\n", Color.BlueViolet);
                Colorful.Console.Write("/? - show this menu\n", Color.Gold);
                Colorful.Console.Write("/config - config the selfbot\n", Color.Gold);
                Colorful.Console.Write("/checkfiles - check the files of the selfbot, update, install ffmpeg etc\n", Color.Gold);
                Colorful.Console.Write("-showtoken - Show ur token when the selfbot is connected to discord\n", Color.Gold);
                Colorful.Console.Write("-skiplogin - Disable the \"Press any key to connect the selfbot to Discord\" event\n", Color.Gold);
                Colorful.Console.Write("-hidephone - Hide your phone number in the console !\n", Color.Gold);
                Colorful.Console.Write("-hideusername - Hide your username in the console !\n", Color.Gold);
                Colorful.Console.Write("-hidehypesquad - Hide your hypesquad in the console !\n", Color.Gold);
                Colorful.Console.Write("-hidenitro - Hide your nitro state in the console !\n", Color.Gold);
                Colorful.Console.Write("-hideguildcount - Hide the guild count in the console !\n", Color.Gold);
                Colorful.Console.Write("-hiderelationships - Hide the friends count in the console !\n", Color.Gold);
                Colorful.Console.Write("-hideemail - Hide your email in the console !\n", Color.Gold);
                Colorful.Console.Write("-hidedebug - Hide the debug state in the console !\n", Color.Gold);
                Colorful.Console.Write("-skipprevention - Skip the prevention message at the start of the selfbot\n", Color.Gold);

                System.Threading.Thread.Sleep(-1);
            }

            if (formattedargs.Contains("/config"))
            {
                Colorful.Console.Write("Prefix : ", Color.IndianRed);
                string prefix = Console.ReadLine();
                Colorful.Console.Write("Twitchlink : ", Color.IndianRed);
                string twitchlink = Console.ReadLine();
                Colorful.Console.Write("Message Logger Webhook : ", Color.IndianRed);
                string webhooklink = Console.ReadLine();
                Colorful.Console.Write("Youtube API Key : ", Color.IndianRed);
                string ytapikey = Console.ReadLine();
                Colorful.Console.Write("Embed Color (R) :", Color.IndianRed);
                string embedcolorr = Console.ReadLine();
                Colorful.Console.Write("Embed Color (G) :", Color.IndianRed);
                string embedcolorg = Console.ReadLine();
                Colorful.Console.Write("Embed Color (B) : ", Color.IndianRed);
                string embedcolorb = Console.ReadLine();
                Colorful.Console.Write("Nitro Sniper (true/false) : ", Color.IndianRed);
                string nitrosniper = Console.ReadLine();
                Colorful.Console.Write("Anti Everyone (true/false) : ", Color.IndianRed);
                string antieveryone = Console.ReadLine();
                Colorful.Console.Write("NSFW (true/false) : ", Color.IndianRed);
                string nsfw = Console.ReadLine();
                Colorful.Console.Write("Message Logger (true/false) : ", Color.IndianRed);
                string msglogger = Console.ReadLine();
                Colorful.Console.Write("Debug (true/false) : ", Color.IndianRed);
                string debug = Console.ReadLine();
                Colorful.Console.Write("Do Not Disturb (true/false) : ", Color.IndianRed);
                string dnd = Console.ReadLine();
                Colorful.Console.Write("Experimental Commands (true/false) : ", Color.IndianRed);
                string experimentalcommand = Console.ReadLine();

                JSON config = JsonConvert.DeserializeObject<JSON>(new WebClient().DownloadString("https://pastebin.com/raw/WhDt5kef"));
                config.prefix = prefix;
                config.twitchlink = twitchlink;
                config.youtubeapikey = ytapikey;
                config.embedcolorr = int.Parse(embedcolorr);
                config.embedcolorg = int.Parse(embedcolorg);
                config.embedcolorb = int.Parse(embedcolorb);
                config.messageloggerwebhook = webhooklink;
                config.messagelogger = msglogger.ToLower().Contains("true");
                config.nitrosniper = nitrosniper.ToLower().Contains("true");
                config.antieveryone = antieveryone.ToLower().Contains("true");
                config.nsfw = nsfw.ToLower().Contains("true");
                config.debug = debug.ToLower().Contains("true");
                config.dnd = dnd.ToLower().Contains("true");
                config.experimentalcommands = experimentalcommand.ToLower().Contains("true");

                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);

                try
                {
                    File.WriteAllText("config.json", configoutput);
                }
                catch { }

                Colorful.Console.WriteLine("\n\nApplied config !", Color.Gold);
                Thread.Sleep(-1);
            }

            if (formattedargs.Contains("/checkfiles"))
            {
                if (File.ReadAllText("token.txt").Length > 59 || File.ReadAllText("token.txt").Length == 59)
                {
                    Colorful.Console.WriteLine("Token detected ! (" + File.ReadAllText("token.txt") + ")", Color.LightGreen);
                };

                if (File.ReadAllLines("token.txt").Length != 1)
                {
                    Colorful.Console.WriteLine("Token invalid format detected !", Color.IndianRed);
                }

                bool ressourcesexists = false;

                Directory.CreateDirectory("ytdl");
                Directory.CreateDirectory("avatar");
                Directory.CreateDirectory("discordcrasher");
                if (Directory.Exists("ressources"))
                {
                    Colorful.Console.WriteLine("Directory ressources exists", Color.LightGreen);
                    ressourcesexists = true;
                }
                else
                {
                    Colorful.Console.WriteLine("Directory ressources doesn't exists", Color.IndianRed);
                    ressourcesexists = false;
                }

                if (ressourcesexists)
                {
                    if (File.Exists("ressources\\channellocker.jpeg"))
                    {
                        if (RamokSelfbot.Utils.CalculateMD5("ressources\\channellocker.jpeg").ToLower() == "c102d9c4b4d24c0335e7cb38d64f4784")
                        {
                            Colorful.Console.WriteLine("Channel locker file seems good", Color.LightGreen);
                        }
                        else
                        {
                            Colorful.Console.WriteLine("Channel locker file edited", Color.IndianRed);
                        }
                    }
                    else
                    {
                        Colorful.Console.WriteLine("Unable to find file channel locker", Color.IndianRed);
                    }

                    if (File.Exists("ressources\\discord-logo.jpg"))
                    {
                        if (RamokSelfbot.Utils.CalculateMD5("ressources\\discord-logo.jpg").ToLower() == "aa9f817a0a7f8c5b4ea03cf5261b4c5b")
                        {
                            Colorful.Console.WriteLine("Discord Logo file seems good", Color.LightGreen);
                        }
                        else
                        {
                            Colorful.Console.WriteLine("Discord Logo file edited", Color.IndianRed);
                        }
                    }
                    else
                    {
                        Colorful.Console.WriteLine("Unable to find file discord logo", Color.IndianRed);
                    }

                    if (File.Exists("ressources\\listhetero.selfbot"))
                    {
                        Colorful.Console.WriteLine("Listhetero.selfbot exists !", Color.LightGreen);
                    }
                    else
                    {
                        Colorful.Console.WriteLine("Listhetero.selfbot do not exists!", Color.IndianRed);
                    }

                    if (File.Exists("ressources\\restarted.ramokselfbot.exemple"))
                    {
                        if (RamokSelfbot.Utils.CalculateMD5("ressources\\restarted.ramokselfbot.exemple").ToLower() == "b0a8efe3829852b460c9052f7d04d447")
                        {
                            Colorful.Console.WriteLine("restarted.ramokselfbot.exemple file seems good", Color.LightGreen);
                        }
                        else
                        {
                            Colorful.Console.WriteLine("restarted.ramokselfbot.exemple file edited (potencielly broken)", Color.IndianRed);
                        }
                    }
                    else
                    {
                        Colorful.Console.WriteLine("restarted.ramokselfbot.exemple do not exists!", Color.IndianRed);
                    }
                }

                int ff = 0;

                if (File.Exists("ffmpeg.exe"))
                {
                    ff++;
                }

                if (File.Exists("ffplay.exe"))
                {
                    ff++;
                }

                if (File.Exists("ffprobe.exe"))
                {
                    ff++;
                }

                if (ff == 3)
                {
                    Colorful.Console.WriteLine("FFMPEG INSTALLED !", Color.LightGreen);
                }
                else
                {
                    Colorful.Console.WriteLine("FFMPEG NOT INSTALLED !", Color.IndianRed);
                    Colorful.Console.Write("Do you want to download FFMPEG ? (Y/N) : ", Color.IndianRed);
                    switch (Console.ReadLine().ToLower())
                    {
                        case "y":
                            Process.Start("https://www.gyan.dev/ffmpeg/builds/packages/ffmpeg-4.3.1-2021-01-26-full_build.7z");
                            break;
                        case "yes":
                            Process.Start("https://www.gyan.dev/ffmpeg/builds/packages/ffmpeg-4.3.1-2021-01-26-full_build.7z");
                            break;
                        case "n":
                            Colorful.Console.WriteLine("Discord Crasher command will not work!");
                            break;
                        case "no":
                            Colorful.Console.WriteLine("Discord Crasher command will not work!");
                            break;
                    }
                }

                Details json = JsonConvert.DeserializeObject<Details>(new System.Net.WebClient().DownloadString("https://ramokselfbot.netlify.app/api/v1/ramokselfbot/details.json"));
                var versionInfo = FileVersionInfo.GetVersionInfo(RamokSelfbot.Utils.GetFileName() + "\\RamokSelfbot-Rewritten.exe");
                string version = versionInfo.FileVersion;
                bool updated = false;
                if (json.version == version)
                    updated = true;

                if (updated == false)
                {
                    Colorful.Console.WriteLine("You are not up to date.\n\nLatest update : " + json.version + "\nYour current file version : " + version + "\n\nUpdate link : " + json.link, Color.Gold);
                }
                else
                {
                    Colorful.Console.WriteLine("You are up to date ", Color.Gold);

                }

                Thread.Sleep(-1);
            }

            /* 
             * Coding started at : 01/09/21
             * V1 of rewrite ended at : ??/??/21 (j'ai plus la date zbi)
             * V2 end = 21/03/21 but not the final ver
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
            if (!formattedargs.Contains("-skipprevention"))
            {
                Console.WriteLine("If you paid this, you got scammed !\nIf u have errors, contact Ramok on Discord for enable Debug mode.\nRamok is not responsable of any Discord bans / guild bans / FTNL Detections.");
                System.Threading.Thread.Sleep(500);
            }
            // BIG CONSOLE : Console.SetWindowSize(129, 30);
            bool verifiedtoken = false;

            client.OnLoggedIn += Client_OnLoggedIn;
            SetupThings.AutomaticThings();

            do
            {
                Console.Clear();

                if (!File.Exists("config.json"))
                {
                    Colorful.Console.WriteLine("Please, start the selfbot with /config start argument.", Color.Gold);
                    System.Threading.Thread.Sleep(-1);
                }

                try
                {
                    Login();
                }
                catch
                {
                    Login();
                }
                System.Threading.Thread.Sleep(-1);
                verifiedtoken = true;
            } while (verifiedtoken == false);

        }

        private static void Login()
        {

            if (File.Exists("token.txt"))
            {
                if (File.ReadAllText("token.txt").Length != 0)
                {
                    try
                    {
                        if (!formattedargs.Contains("-skiplogin"))
                        {
                            Colorful.Console.WriteLine("Press any key to connect the selfbot to Discord !\n(Copy the content in token.txt in the token login)");
                            Console.ReadKey();
                        }

                        if (Program.Debug == true)
                        {
                            Console.WriteLine("1");
                        }

                        if (Program.Debug != true)
                        {
                            Console.Clear();
                        }

                        Program.token = File.ReadAllText("token.txt");
                        if (Program.Debug == true)
                        {
                            Console.WriteLine("2");
                        }

                        client.Login(token);

                        if (Program.Debug == true)
                        {
                            Console.WriteLine("3");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nEXCEPTION HANDLER : ERROR\n\nError message : " + ex.Message);
                        Console.WriteLine("Press any key to retry");
                        Console.ReadKey();
                        Console.Clear();
                        Login();
                    }
                }
            }
            else
            {
                Colorful.Console.Write("Token : ", Color.Gold);
                string token = Console.ReadLine();
                File.WriteAllText(RamokSelfbot.Utils.GetFileName() + "\\token.txt", token);
                throw new Exception("No token");
            }

        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Program.Debug = RamokSelfbot.Utils.DumpConfig().debug;
            new Thread(new ThreadStart(time.Start)).Start();
            if (Program.Debug != true)
            {
                Console.Clear();
            }

            Colorful.Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            if (!formattedargs.Contains("-hideusername"))
            {
                Colorful.Console.WriteLine($"╠--> Username : {args.User.Username}#{args.User.Discriminator} ({args.User.Id})", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }
            if (!formattedargs.Contains("-hidephone"))
            {
                if (client.User.PhoneNumber == null)
                {
                    Colorful.Console.WriteLine($"╠--> No phone number", Color.MediumSlateBlue);
                    Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
                }
                else
                {
                    Colorful.Console.WriteLine($"╠--> Phone number : {args.User.PhoneNumber}", Color.MediumSlateBlue);
                    Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
                }

            }
            if (!formattedargs.Contains("-hidehypesquad"))
            {
                Colorful.Console.WriteLine($"╠--> Hypesquad : {args.User.Hypesquad}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }

            if (!formattedargs.Contains("-hidenitro"))
            {
                Colorful.Console.WriteLine($"╠--> Nitro : {args.User.Nitro}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }
            if (!formattedargs.Contains("-hideguildcount"))
            {
                Colorful.Console.WriteLine($"╠--> Guilds : {args.Guilds.Count}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }
            if (!formattedargs.Contains("-hiderelationships"))
            {
                Colorful.Console.WriteLine($"╠--> Relationships : {args.Relationships.Count}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }
            if (!formattedargs.Contains("-hidedebug"))
            {
                Colorful.Console.WriteLine($"╠--> Debug : {Program.Debug}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }

            if (formattedargs.Contains("-showtoken"))
            {
                Colorful.Console.WriteLine($"╠--> Token : {token}", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);

            }
            if (!formattedargs.Contains("-hideemail"))
            {
                Colorful.Console.WriteLine($"╠--> Email : {args.User.Email} (Verified : {args.User.EmailVerified})", Color.MediumSlateBlue);
                Colorful.Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);
            }
            Colorful.Console.WriteLine($"╠--> Prefix : {RamokSelfbot.Utils.DumpConfig().prefix}", Color.MediumSlateBlue);
            Colorful.Console.WriteLine("╘══════════════════════════════════════════════════════════════════════════════", Color.MediumSlateBlue);

            id = args.User.Id;

            Console.WriteLine();
            Colorful.Console.WriteLine("Thanks for using RamokSelfbot !", Color.MediumSlateBlue);
            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("Logs =>\n", Color.MediumSlateBlue);
            Restarted();
            HttpRequest request = new HttpRequest();
            request.AddHeader("ramokselfbot", "ramoklebg");
            string res = request.Get("https://ramok.herokuapp.com/api/checkforupdates?clientver=" + FileVersionInfo.GetVersionInfo(RamokSelfbot.Utils.GetFileName() + "\\RamokSelfbot-Rewritten.exe").FileVersion.Replace(".", "")).ToString();

            if (res.Contains("Not up to date"))
            {
                DialogResult notuptodate = MessageBox.Show("Its looks like there is a new update, do you want to do it ?", "Auto Updator", MessageBoxButtons.YesNo);
                if (notuptodate == DialogResult.Yes)
                {
                    //TODO   / FIXME   Process.Start(RamokSelfbot.Utils.GetFileName() + "\\AutoUpdater.exe");
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
                    Footer = new EmbedFooter()
                    {
                        Text = "Restarted : " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()
                    }
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

        public static string formattedargs
        {
            get;
            set;
        }

        public static List<string> clip
        {
            get;
            set;
        }

        public static string token
        {
            get;
            set;
        }

        public static ulong MSGSent;
        public static ulong MSGRecieved;

        public static bool Debug = true;
        public static bool DynamicName = true;
        public static bool DynamicStatus = true;
        public static bool SkipLogin = false;

        public static Stopwatch time = new Stopwatch();
    }

}