using Discord.Commands;
using MediaToolkit;
using System.IO;
using System;
using System.Net;
using VideoLibrary;
using System.Diagnostics;
using Discord;

namespace RamokSelfbot.Commands.Others
{
    [Command("ytdl", "Download a video on youtube ! - OTHERS")]
    class YTDL : CommandBase
    {

        [Parameter("format")]
        public string format { get; set; }

        [Parameter("link")]
        public string link { get; set; }


        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                GetTitle();
                Directory.CreateDirectory("ytdl");
                

                var youtube = YouTube.Default;
                var video = youtube.GetVideo(link);
                File.WriteAllBytes("ytdl\\" + video.FullName, video.GetBytes());
                
                if (Program.Debug)
                    Console.WriteLine("1");

                var inputFile = new MediaToolkit.Model.MediaFile { Filename = "ytdl\\" + video.FullName };
                var outputFile = new MediaToolkit.Model.MediaFile { Filename = ("ytdl\\" + video.FullName).Remove(("ytdl\\" + video.FullName).Length - 4, 4) + ".mp3" };
                if (Program.Debug)
                    Console.WriteLine("2");
                using (var enging = new Engine())
                {
                    enging.GetMetadata(inputFile);
                    enging.Convert(inputFile, outputFile);
                }
                if (Program.Debug)
                    Console.WriteLine("3");

                switch(format.ToLower())
                {
                    case "mp3":
                        File.Delete("ytdl\\" + video.FullName);
                        break;

                    case "mp4":
                        File.Delete(("ytdl\\" + video.FullName).Remove(("ytdl\\" + video.FullName).Length - 4, 4) + ".mp3");
                        break;
                    default:
                        //i do not delete anything
                        break;
                }

               
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "If your connexion is bad, the download time is sweaty",
                    Title = "YouTube Download"
                };
                sw.Stop();
                embed.AddField("⌚ Time to download the video", sw.Elapsed.Minutes + " minutes and " + sw.Elapsed.Seconds + " seconds", false);
                embed.AddField("📁 Saved at", RamokSelfbot.Utils.GetFileName() + "\\ytdl\\" + video.FullName, false);
              
                RamokSelfbot.Utils.SendEmbed(Message, embed);

            }
        }

        void GetTitle()
        {
            WebRequest istek = HttpWebRequest.Create(link);
            WebResponse yanıt;
            yanıt = istek.GetResponse();
            StreamReader bilgiler = new StreamReader(yanıt.GetResponseStream());
            string gelen = bilgiler.ReadToEnd();
            int baslangic = gelen.IndexOf("<title>") + 7;
            int bitis = gelen.Substring(baslangic).IndexOf("</title>");
            string gelenbilgiler = gelen.Substring(baslangic, bitis);
            status = (gelenbilgiler);
            if (Program.Debug)
                Console.WriteLine("4");
        }



        public static string status;
    }
}
