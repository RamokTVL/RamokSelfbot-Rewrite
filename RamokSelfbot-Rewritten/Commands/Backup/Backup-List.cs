using Discord.Commands;
using Discord;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RamokSelfbot.Commands.Backup
{
    [Command("backup-list", "Send you the list of the backups - BACKUP")]
    class Backup_List : CommandBase
    {
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
          

                    //On prend le nom des subdossiers dans le dossier "backup"
                    string[] arrayFolders = Directory.GetDirectories(RamokSelfbot.Utils.GetFileName() + "\\backup");

                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                    };

                    var desc = "";
                    foreach (var folder in arrayFolders)
                    {
                        string fileName = Path.GetFileName(folder); //=backup


                        if (Program.Debug)
                        {
                            Console.WriteLine(fileName);
                        }
          
                        if (File.Exists($"{RamokSelfbot.Utils.GetFileName()}\\backuplist\\{fileName}.json"))
                        {
                         //   Console.WriteLine($"{RamokSelfbot.Utils.GetFileName()}\\backuplist\\{fileName}.json");
                            Exemple metadata = JsonConvert.DeserializeObject<Exemple>(File.ReadAllText($"{RamokSelfbot.Utils.GetFileName()}\\backuplist\\{fileName}.json"));
                            
                            desc += $"Backup code : **{fileName}**\nCreated At : {metadata.created_at}\n\n";
                        }
                        else
                        {
                            if (Program.Debug)
                            {
                                Console.WriteLine($"{RamokSelfbot.Utils.GetFileName()}\\backuplist\\{fileName}.json");
                            }
                        }
                    }

                    if(string.IsNullOrEmpty(desc))
                    {
                        embed.Description = "No backup found!";
                    } else
                    {
                        embed.Description = desc;
                    }


                    RamokSelfbot.Utils.SendEmbed(Message, embed);


            }
        }
    }



}
