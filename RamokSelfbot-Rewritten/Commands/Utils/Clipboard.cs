using Discord.Commands;
using System.IO;
using Discord;
using System;

namespace RamokSelfbot.Commands.Utils
{
    [Command("clipboardsniper", "Send you the history of the clipboard while the bot is started - UTILS")]
    class Clipboard : CommandBase
    {
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                    string clip = "";

                clip = Program.clip.Count.ToString();

                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                    };

                    var date = DateTime.Now;
                    if(clip.Length > 1500)
                    {
                        string path = RamokSelfbot.Utils.GetFileName() + $"\\clipboard - {date.Day}/{date.Month} {date.Hour}:{date.Minute}.txt";
                        File.WriteAllText(path, clip);
                        embed.Description = "The clipboard history length was too long, so we writted it in a file on your computer!";
                        embed.AddField("📁 Path to the clipboard history", path);
                    } else
                    {
                        embed.Description = "```\n" + clip + "```";
                    }

                    RamokSelfbot.Utils.SendEmbed(Message, embed);

            /*    Message.Edit(new MessageEditProperties()
                {
                    Content = ":x: Adding this shit later"
                });
                return;*/
            }
        }


    }
}
