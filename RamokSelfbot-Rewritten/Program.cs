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
                Program.formattedargs = args[0];
            }
            else
            {
                Program.formattedargs = "no";
            }


            if(formattedargs.Contains("/?"))
            {
                Colorful.Console.Write("Project created by RamokTV\nNitro sniper credits : https://github.com/Stanley-GF/StanSniper by Stanley-GF\nBypass service credits : bypass-shorteners.herokuapp.com by IDRALOU#6966\n\n\n", Color.MediumPurple);
                Colorful.Console.Write("Avaliable arguments start function : \n\n", Color.BlueViolet);
                Colorful.Console.Write("/? - show this menu\n", Color.Gold);
                Colorful.Console.Write("/config - config the selfbot\n", Color.Gold);
                Colorful.Console.Write("/checkfiles - check the files of the selfbot, update, install ffmpeg etc\n", Color.Gold);
                Colorful.Console.Write("-showtoken - Show ur token when the selfbot is connected to discord", Color.Gold);

                System.Threading.Thread.Sleep(-1);
            }

            if(formattedargs.Contains("/config"))
            {
                Colorful.Console.Write("Prefix : ", Color.IndianRed);
                string prefix = Console.ReadLine();
                Colorful.Console.Write("Twitchlink : ", Color.IndianRed);
                string twitchlink = Console.ReadLine();
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
                Colorful.Console.Write("Debug (true/false) : ", Color.IndianRed);
                string debug = Console.ReadLine();
                Colorful.Console.Write("Experimental Commands (true/false) : ", Color.IndianRed);
                string experimentalcommand = Console.ReadLine();

                JSON config = JsonConvert.DeserializeObject<JSON>(new WebClient().DownloadString("https://pastebin.com/raw/WhDt5kef"));
                config.prefix = prefix;
                config.twitchlink = twitchlink;
                config.youtubeapikey = ytapikey;
                config.embedcolorr = int.Parse(embedcolorr);
                config.embedcolorg = int.Parse(embedcolorg);
                config.embedcolorb = int.Parse(embedcolorb);
                config.nitrosniper = nitrosniper.ToLower().Contains("true");
                config.antieveryone = antieveryone.ToLower().Contains("true");
                config.nsfw = nsfw.ToLower().Contains("true");
                config.debug = debug.ToLower().Contains("true");
                config.experimentalcommands = experimentalcommand.ToLower().Contains("true");

                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);

                try
                {
                    File.WriteAllText("config.json", configoutput);
                } catch { }

                Colorful.Console.WriteLine("\n\nApplied config !", Color.Gold);
                Thread.Sleep(-1);
            }

            if(formattedargs.Contains("/checkfiles"))
            {
                if(File.ReadAllText("token.txt").Length > 59 || File.ReadAllText("token.txt").Length == 59)
                {
                    Colorful.Console.WriteLine("Token detected ! (" + File.ReadAllText("token.txt") + ")", Color.LightGreen);
                };

                if(File.ReadAllLines("token.txt").Length != 1)
                {
                    Colorful.Console.WriteLine("Token invalid format detected !", Color.IndianRed);
                }

                bool ressourcesexists = false;

                Directory.CreateDirectory("ytdl");
                Directory.CreateDirectory("avatar");
                Directory.CreateDirectory("discordcrasher");
                if(Directory.Exists("ressources"))
                {
                    Colorful.Console.WriteLine("Directory ressources exists", Color.LightGreen);
                    ressourcesexists = true;
                } else
                {
                    Colorful.Console.WriteLine("Directory ressources doesn't exists", Color.IndianRed);
                    ressourcesexists = false;
                }

                if(ressourcesexists)
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

                if(File.Exists("config.json"))
                {
                    if(File.ReadAllLines("config.json").Length == 13)
                    {
                        Colorful.Console.WriteLine("config.json file lines seems good !", Color.LightGreen);
                    } else
                    {
                        Colorful.Console.WriteLine("config.json format is not good!", Color.IndianRed);
                    }
                } else
                {
                    Colorful.Console.WriteLine("config.json do not exists !", Color.IndianRed);
                }

                int ff = 0;

                if(File.Exists("ffmpeg.exe"))
                {
                    ff++;
                }

                if(File.Exists("ffplay.exe"))
                {
                    ff++;
                }

                if(File.Exists("ffprobe.exe"))
                {
                    ff++;
                }

                if(ff == 3)
                {
                    Colorful.Console.WriteLine("FFMPEG INSTALLED !", Color.LightGreen);
                } else
                {
                    Colorful.Console.WriteLine("FFMPEG NOT INSTALLED !", Color.IndianRed);
                    Colorful.Console.Write("Do you want to download FFMPEG ? (Y/N) : ", Color.IndianRed);
                    switch(Console.ReadLine().ToLower())
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

            if (Program.Debug)
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

            client.OnLoggedIn += Client_OnLoggedIn;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.CreateCommandHandler(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).prefix);



            do
               {
                Console.Clear();



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

          

                try
                {
                    Login();
                } catch
                {
                    Login();
                }
                System.Threading.Thread.Sleep(-1);
                verifiedtoken = true;
            } while (verifiedtoken == false);
            
        }

        private static void Login()
        {
      
                if(File.Exists("token.txt"))
                {
                    if(File.ReadAllText("token.txt").Length != 0)
                    {
                        try
                        {
                            Colorful.Console.WriteLine("Press any key to connect the selfbot to Discord !\n(Copy the content in token.txt in the token login)");
                            Console.ReadKey();
                        if(Program.Debug == true)
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
                    catch(Exception ex)
                        {
                            Console.WriteLine("\nEXCEPTION HANDLER : ERROR\n\nError message : " + ex.Message);
                        Console.WriteLine("Press any key to retry");
                        Console.ReadKey();
                        Console.Clear();
                        Login();
                        }
                    }
                }
       
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

            if(config.dnd)
            {          
               args.Message.AcknowledgeAsync();
                if(Program.Debug)
                {
                    Colorful.Console.WriteLine("Readed a message ! (dnd)", Color.Gold);
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
            MessageBox.Show("We've detected " + detection.ToString() + " errors in ressources files of the selfbot !\nPlease redownload the selfbot or you can get bugs", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // FIXME   new Thread(new ThreadStart(CheckInternet)).Start();
            if (Program.Debug == true)
            {
                Console.WriteLine("4");
            }
            new Thread(new ThreadStart(VerifyMD5)).Start();
            if (Program.Debug == true)
            {
                Console.WriteLine("5");
            }
            new Thread(new ThreadStart(time.Start)).Start();

            if (Program.Debug != true)
            {
                Console.Clear();
            }
            
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
        public static bool DynamicStatus = true;

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
        public bool dnd { get; set; }
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
