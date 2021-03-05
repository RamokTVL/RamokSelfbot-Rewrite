using Discord.Commands;
using Discord;
using System;
using System.IO;

namespace RamokSelfbot.Commands.Backup
{
    [Command("backup-d", "Delete the backup provided - BACKUP")]
    class Backup_Delete : CommandBase
    {
        [Parameter("backupid")]
        public string backupid { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                if(Directory.Exists(RamokSelfbot.Utils.GetFileName() + "\\backup\\" + backupid))
                {
                    try
                    {
                        Directory.Delete(RamokSelfbot.Utils.GetFileName() + "\\backup\\" + backupid);

                        EmbedMaker embed = new EmbedMaker()
                        {
                            Description = $"The backup {backupid} has been deleted for ya!",
                            Color = RamokSelfbot.Utils.EmbedColor(),
                            Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                        };

                        RamokSelfbot.Utils.SendEmbed(Message, embed);
                    } catch(Exception ex)
                    {
                        Message.Edit(new MessageEditProperties()
                        {
                            Content = ":x: Error while deleting the backup.\n" + ex.Message
                        });
                    }
                } else
                {
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Description = "The backup provided doesn't exists!",
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                    };

                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                }
            }
        }
    }
}
