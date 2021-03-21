using Discord;
using Discord.Gateway;
using Leaf.xNet;
using Newtonsoft.Json;
using RamokSelfbot.Commands.Info;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot
{

    public class SetupThings
    {
        public static void AutomaticThings()
        {
            Program.client.OnMessageReceived += Client_OnMessageReceived;
            Program.client.OnUserBanned += Client_OnUserBanned;
            Program.client.OnChannelCreated += Client_OnChannelCreated;
            Program.client.OnChannelDeleted += Client_OnChannelDeleted;
            Program.client.OnUserUnbanned += Client_OnUserUnbanned;
            Program.client.CreateCommandHandler(RamokSelfbot.Utils.DumpConfig().prefix);
        }

        private static void Client_OnMessageReceived(Discord.Gateway.DiscordSocketClient client, Discord.Gateway.MessageEventArgs args)
        {
            var config = RamokSelfbot.Utils.DumpConfig();
            if (config.messagelogger)
            {
                if (args.Message.Guild != null)
                {
                    if (File.ReadAllText("ressources\\messageloggerguilds.selfbot").Contains(args.Message.Guild.Id.ToString()))
                    {
                        if (args.Message.Author.User.Id != client.User.Id)
                        {

                            if (args.Message.Channel.Id.ToString() != JsonConvert.DeserializeObject<Webhook>(new WebClient().DownloadString(config.messageloggerwebhook)).channel_id)
                                if (args.Message.Content != "")
                                {
                                    if (config.messageloggerwebhook != null)
                                    {
                                        RamokSelfbot.Utils.Root webhook = JsonConvert.DeserializeObject<RamokSelfbot.Utils.Root>(File.ReadAllText("ressources\\webhookmessagelogger.webhookexemple"));

                                        if (config.messageloggerwebhook.Contains("webhook"))
                                        {
                                            if (args.Message.Author.User.Avatar != null)
                                            {
                                                webhook.avatar_url = args.Message.Author.User.Avatar.Url;
                                            }
                                            else
                                            {
                                                webhook.avatar_url = "";
                                            }

                                            var configcolor = JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json"));
                                            Color thecolor = Color.FromArgb(configcolor.embedcolorr, configcolor.embedcolorg, configcolor.embedcolorb);

                                            string hex = thecolor.R.ToString("X2") + thecolor.G.ToString("X2") + thecolor.B.ToString("X2");
                                            int colorhex = Convert.ToInt32(hex, 16);


                                            webhook.username = "RamokSelfbot - MessageLogger"; //USERNAME OF THE WEBHOOK
                                            webhook.embeds[0].description = "Best selfbot forever :sunglasses:";
                                            webhook.embeds[0].color = colorhex;
                                            //       webhook.embeds[0].footer
                                            webhook.embeds[0].fields[0].name = "Message Author";
                                            webhook.embeds[0].fields[0].value = args.Message.Author.User.Username + "#" + args.Message.Author.User.Discriminator;
                                            webhook.embeds[0].fields[0].inline = true;
                                            webhook.embeds[0].fields[1].inline = true;
                                            webhook.embeds[0].fields[1].value = args.Message.SentAt.Day + "/" + args.Message.SentAt.Month + "/" + args.Message.SentAt.Year + " " + args.Message.SentAt.Hour + ":" + args.Message.SentAt.Minute + ":" + args.Message.SentAt.Second;
                                            webhook.embeds[0].fields[1].name = "Send at";
                                            webhook.embeds[0].fields[2].name = "Guild Name";
                                            webhook.embeds[0].fields[2].value = client.GetCachedGuild(args.Message.Guild.Id).Name + " (" + args.Message.Guild.Id + ")";
                                            webhook.embeds[0].fields[2].inline = true;
                                            webhook.embeds[0].fields[3].inline = true;
                                            webhook.embeds[0].fields[3].name = "Channel Name";
                                            webhook.embeds[0].fields[3].value = client.GetChannel(args.Message.Channel.Id).Name + " (" + args.Message.Channel.Id + ")";
                                            webhook.embeds[0].fields[4].name = "Message Link";
                                            webhook.embeds[0].fields[4].inline = false;
                                            webhook.embeds[0].fields[4].value = "[Travel to](http://discord.com/channels/" + args.Message.Guild.Id + "/" + args.Message.Channel.Id + "/" + args.Message.Id + ")";
                                            webhook.embeds[0].fields[5].name = "Attachement";
                                            webhook.embeds[0].fields[5].inline = false;
                                            webhook.embeds[0].fields[6].inline = false;
                                            if (args.Message.Embed == null)
                                            {
                                                webhook.embeds[0].fields[6].value = "No embed";

                                            }
                                            else
                                            {
                                                webhook.embeds[0].fields[6].value = "The embed will be send in the message below";
                                            }
                                            webhook.embeds[0].fields[6].name = "Embed";
                                            webhook.embeds[0].fields[7].name = "Content";
                                            if (args.Message.Content == null)
                                            {
                                                webhook.embeds[0].fields[7].value = "```\n" + "no content" + "```";
                                            }
                                            else
                                            {
                                                webhook.embeds[0].fields[7].value = "```\n" + args.Message.Content + "```";
                                            }
                                            webhook.embeds[0].fields[6].inline = false;
                                            if (args.Message.Attachment != null)
                                            {
                                                webhook.embeds[0].fields[5].value = "```\nFile Name : " + args.Message.Attachment.FileName + "\nFile Size : " + args.Message.Attachment.FileSize + "\n" + "Attachement ID : " + args.Message.Attachment.Id + "\nFile Url : " + args.Message.Attachment.Url + "```";
                                            }
                                            else
                                            {
                                                webhook.embeds[0].fields[5].value = "No attachement.";
                                            }

                                            if (client.User.Avatar != null)
                                            {
                                                webhook.embeds[0].footer.icon_url = client.User.Avatar.Url;
                                            }

                                            webhook.embeds[0].footer.text = RamokSelfbot.Footer.embed_text;

                                            var a = JsonConvert.SerializeObject(webhook, Formatting.Indented);

                                            File.AppendAllText("test.aa", a);

                                            Console.WriteLine(new HttpRequest().Post(config.messageloggerwebhook, a, "application/json"));

                                            if (args.Message.Embed != null)
                                            {
                                                //     client.SendMessage();
                                            }
                                        };
                                    }
                                }

                        }
                    }
                }
                else
                {
                    RamokSelfbot.Utils.Print("Private messages will be supported soon...");
                }
            }


            if (config.alertword)
            {


                if (config.alertwordlist[0] != null)
                {
                    if (args.Message.Content.ToLower().Contains(config.alertwordlist[0].ToLower()))
                    {
                        RamokSelfbot.Utils.Print($"Alertword[0], triggered by {args.Message.Author.User.Username}#{args.Message.Author.User.Discriminator} :::: {args.Message.Content}");
                    }
                }

                if (config.alertwordlist[1] != null)
                {
                    if (args.Message.Content.ToLower().Contains(config.alertwordlist[1].ToLower()))
                    {
                        RamokSelfbot.Utils.Print($"Alertword[1], triggered by {args.Message.Author.User.Username}#{args.Message.Author.User.Discriminator} :::: {args.Message.Content}");
                    }
                }

                if (config.alertwordlist[2] != null)
                {

                    if (args.Message.Content.ToLower().Contains(config.alertwordlist[2].ToLower()))
                    {
                        RamokSelfbot.Utils.Print($"Alertword[2], triggered by {args.Message.Author.User.Username}#{args.Message.Author.User.Discriminator} :::: {args.Message.Content}");
                    }
                }


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
            if (args.Message.MentionedEveryone)
            {
                if (config.antieveryone)
                {
                    args.Message.Acknowledge();
                }
            }

            if (config.dnd)
            {
                args.Message.Acknowledge();
            }
            if (RamokSelfbot.Utils.IsClient(args.Message))
            {
                Program.MSGSent++;
            }
            else
            {
                Program.MSGRecieved++;
            }
        }

        private static void Client_OnUserUnbanned(DiscordSocketClient client, BanUpdateEventArgs args)
        {
            if (RamokSelfbot.Utils.DumpConfig().logs)
            {
                var date = DateTime.Now;
                RamokSelfbot.Utils.Print($"User unbanned ! --- GUILD : {(client.GetCachedGuild(args.Guild.Id).Name).Replace("?", "")}\nMEMBER BANNED : {args.User.Username}#{args.User.Discriminator}\nUnbanned at : {date.Day}/{date.Month}/{date.Year} {date.Hour}:{date.Minute}");
            }
        }

        private static void Client_OnChannelDeleted(DiscordSocketClient client, ChannelEventArgs args)
        {
            if (RamokSelfbot.Utils.DumpConfig().logs)
            {
                RamokSelfbot.Utils.Print($"New channel deleted : \nName : {args.Channel.Name}\nId : {args.Channel.Id.ToString()}\nType : {args.Channel.Type.ToString()}");
            }
        }

        private static void Client_OnChannelCreated(DiscordSocketClient client, ChannelEventArgs args)
        {
            if (RamokSelfbot.Utils.DumpConfig().logs)
            {
                RamokSelfbot.Utils.Print($"New channel created : \nName : {args.Channel.Name}\nId : {args.Channel.Id.ToString()}\nType : {args.Channel.Type.ToString()}");
            }
        }

        private static void Client_OnUserBanned(DiscordSocketClient client, BanUpdateEventArgs args)
        {
            if (RamokSelfbot.Utils.DumpConfig().logs)
            {
                var date = DateTime.Now;
                RamokSelfbot.Utils.Print($"User banned ! --- GUILD : {(client.GetCachedGuild(args.Guild.Id).Name).Replace("?", "")}\nMEMBER BANNED : {args.User.Username}#{args.User.Discriminator}\nBanned at : {date.Day}/{date.Month}/{date.Year} {date.Hour}:{date.Minute}");
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
    }
    public class JSON
    {
        public string prefix { get; set; }
        public bool experimentalcommands { get; set; }
        public bool nitrosniper { get; set; }
        public bool antieveryone { get; set; }
        public bool nsfw { get; set; }
        public bool logs { get; set; }
        public bool debug { get; set; }
        public bool messagelogger { get; set; }
        public bool alertword { get; set; }

        public List<string> alertwordlist;
        public bool dnd { get; set; }
        public string twitchlink { get; set; }
        public string youtubeapikey { get; set; }
        public string apikeyself { get; set; }

        public int embedcolorr { get; set; }

        public int embedcolorg { get; set; }
        public int embedcolorb { get; set; }

        public string messageloggerwebhook { get; set; }

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
