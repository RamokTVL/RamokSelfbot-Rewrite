using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.IO;
using Leaf.xNet;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.Backup
{
    [Command("backup-c", "Creates a backup of the current guild ! - BACKUP")]
    class Backup_C : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                if(Message.Guild != null)
                {
                    Directory.CreateDirectory("backup");
                    Directory.CreateDirectory("backuplist");
                    var code = RamokSelfbot.Utils.RandomString(10);
                    Directory.CreateDirectory("backup\\" + code.ToString());
                    try
                    {
                        HttpRequest request = new HttpRequest();
                        request.AddHeader("Authorization", Client.Token);
                        string channels = request.Get("https://discordapp.com/api/v8/guilds/" + Message.Guild.Id + "/channels").ToString();
                        File.WriteAllText(RamokSelfbot.Utils.GetFileName() + $"\\backup\\{code}\\" + code + "-channels.json", channels);
                    } catch(Exception ex)
                    {
                        if(Program.Debug)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    try
                    {
                        HttpRequest request2 = new HttpRequest();
                        request2.AddHeader("Authorization", Client.Token);
                        string guild = request2.Get("https://discordapp.com/api/v8/guilds/" + Message.Guild.Id.ToString()).ToString();
                        File.WriteAllText(RamokSelfbot.Utils.GetFileName() + $"\\backup\\{code}\\" + code + ".json", guild);
                    }
                    catch (Exception ex)
                    {
                        if (Program.Debug)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    EmbedMaker embed = new EmbedMaker()
                    {
                        Description = "To learn how to manage backups, use " + Client.CommandHandler.Prefix + "**backup**",
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                    };

                    if(File.Exists($"{RamokSelfbot.Utils.GetFileName()}\\ressources\\backupmetadata.exemple"))
                    {
                        Exemple metadata = JsonConvert.DeserializeObject<Exemple>(File.ReadAllText($"{RamokSelfbot.Utils.GetFileName()}\\ressources\\backupmetadata.exemple"));
                        metadata.name = code;
                        metadata.created_at = RamokSelfbot.Utils.GetTimestamp(DateTime.Now);
                        var output = JsonConvert.SerializeObject(metadata, Formatting.Indented);
                        File.WriteAllText($"{RamokSelfbot.Utils.GetFileName()}\\backuplist\\{code}.json", output);
                    }

                    embed.AddField("Config ID", code, false);
                    embed.AddField("Path to config", $"{RamokSelfbot.Utils.GetFileName()}\\backup\\{code}\\{code}.json & {RamokSelfbot.Utils.GetFileName()}\\backup\\{code}\\{code}-channels.json");

                    RamokSelfbot.Utils.SendEmbed(Message, embed);


                } else
                {
                    Message.Delete();
                    RamokSelfbot.Utils.Print("You cant backup a private channel !");
                }
            }
        }
    }

    public class Exemple
    {
        public string name { get; set; }
        public string created_at { get; set; }
    }



}
