using Discord.Commands;
using Discord;
using System;
using Newtonsoft.Json;
using System.IO;

namespace RamokSelfbot.Commands.Help
{
    [Command("help", "Help you to use the selfbot - UTILS")]
    class Help : CommandBase
    {
        [Parameter("cmd", true)]
        public string cmd { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                if(cmd != null)
                {
                    string desc = null;
                    string name = null;
                    string param = "";
                    int paramcount = 0;
                    bool exsist = false;

                    try                      
                    {
                        var command = Client.CommandHandler.Commands[cmd];
                                                    exsist = true;
                        name = command.Name;
                            if(command.Description != null)
                        {
                            desc = command.Description;
                        }
                            if (command.Parameters != null)
                            {
                                paramcount = command.Parameters.Count;
                                for (int i = 0; i < command.Parameters.Count; i++)
                                {
                                param += "[" + command.Parameters[i].Name + "]\nOptional : " + command.Parameters[i].Optional.ToString() + "\n\n";
                                }
                            } else
                        {
                            paramcount = 0;
                        }
                    } catch {
                   
                        exsist = false;
                    }

                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Title = "Command Info - " + cmd
                    };

                    embed.AddField("Exist", exsist.ToString(), false);
                    embed.AddField("Name", name.ToString(), false);
                    embed.AddField("Description", desc.ToString(), false);
                    embed.AddField("Parameters [" + paramcount + "]", param.ToString(), false);


                    RamokSelfbot.Utils.SendEmbed(Message, embed);

                    
                } else
                {
                    int fun = 0;
                    int hash = 0;
                    int raid = 0;
                    int utils = 0;
                    int moderation = 0;
                    int activity = 0;
                    int others = 0;
                    int covid = 0;
                    int info = 0;
                    int nsfw = 0;
                    int experimental = 0;


                    foreach (var cmds in Client.CommandHandler.Commands.Values)
                    {
                        switch (cmds.Description)
                        {
                            case string a when a.Contains("- FUN"):
                                fun++;
                                break;
                            case string b when b.Contains("- HASH"):
                                hash++;
                                break;                     
                             case string c when c.Contains("- RAID"):
                                raid++;
                                break;               
                            case string d when d.Contains("- UTILS"):
                                utils++;
                                break;                     
                            case string e when e.Contains("- MODERATION"):
                                moderation++;
                                break;              
                            case string f when f.Contains("- ACTIVITY"):
                                activity++;
                                break;             
                            case string g when g.Contains("- OTHERS"):
                                others++;
                                break;              
                            case string h when h.Contains("- INFO"):
                                info++;
                                break;            
                            case string i when i.Contains("- EXPERIMENTAL"):
                                experimental++;
                                break;                
                            case string j when j.Contains("- COVID"):
                                covid++;
                                break;                
                            case string k when k.Contains("- NSFW"):
                                nsfw++;
                                break;
                        };


                    }
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                        Title = "Help menu",
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "This menu is made for help you. \nIf you dont understand it, you are a retard." +
                        "\n\n" +
                        "**" + Client.CommandHandler.Prefix + "fun [" + fun.ToString() + "]" + "**\n```\nShow informations about the funs commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "info [" + info.ToString() + "]" + "**\n```\nShow informations about the informatives commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "hash [" + hash.ToString() + "]" + "**\n```\nShow informations about the hashes commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "nsfw [" + nsfw.ToString() + "]" + "**\n```\nThis command display NSFWs commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "raid [" + raid.ToString() + "]" + "**\n```\nShow informations about the raids commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "utils [" + utils.ToString() + "]" + "**\n```\nShow informations about the utils commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "covid [" + covid.ToString() + "]" + "**\n```\nGet the commands for covid stats (france only)```" +
                        "\n**" + Client.CommandHandler.Prefix + "moderation [" + moderation.ToString() + "]" + "**\n```\nShow informations about the moderation commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "experimental [" + experimental.ToString() + "]" + "**\n```\nThis command help you to use experimentals commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "activity [" + activity.ToString() + "]" + "**\n```\nShow informations about the activities commands```" +
                        "\n**" + Client.CommandHandler.Prefix + "others [" + others.ToString() + "]" + "**\n```\nShow informations about the commands that are not listed on the 4 others menus```",
                        //
                    };


                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                }


            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if(Message.Author.User.Id == Program.id)
            {
                Colorful.Console.WriteLine(exception.Message, System.Drawing.Color.White);
            }
        }
    }
}
