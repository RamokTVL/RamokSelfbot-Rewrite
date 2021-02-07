using Discord;
using Discord.Commands;
using System.Media;
using WMPLib;

namespace RamokSelfbot.Commands.Music
{
    [Command("play", "Play music locally - MUSIC")]
    class Play : CommandBase
    {
        [Parameter("path")]
        public string path { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                mainSound.URL = path;
                mainSound.controls.play();
                mainSound.controls.currentItem.getItemInfo(path);

                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "Beta command, sound can stop."
                };

                embed.AddField("File path", path, false);
                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }

        public static WindowsMediaPlayer mainSound = new WindowsMediaPlayer();
    }
}
