using Discord.Commands;
using Discord;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace RamokSelfbot.Commands.Help
{
    [Command("help", "Help you to use the selfbot - HELPMENU")]
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
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                        Title = "Help menu",
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "This menu is made for help you. \nIf you dont understand it, you're a retard."
                    };

                    foreach (var cmd in Client.CommandHandler.Commands.Values)
                    {
                        StringBuilder args = new StringBuilder();
                        if (cmd.Description.Contains("- HELPMENU"))
                        {
                            args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 11, 11)}```");
                            embed.AddField($"{Client.CommandHandler.Prefix}**{cmd.Name}**", $"**{args}**");
                        }
                    }

              /*    embed.AddField($"ʜᴇʟᴘ ꜰᴜɴ **[{fun}]**", @"𝓢𝓱𝓸𝔀 𝓯𝓾𝓷 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ɪɴꜰᴏ **[{info}]**", @"𝓢𝓱𝓸𝔀 𝓲𝓷𝓯𝓸 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ʜᴀꜱʜ **[{hash}]**", @"𝓢𝓱𝓸𝔀 𝓱𝓪𝓼𝓱 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField("\u200b", "\u200b", false);
                    embed.AddField($"ʜᴇʟᴘ ɴꜱꜰᴡ **[{nsfw}]**", @"𝓢𝓱𝓸𝔀 𝓷𝓼𝓯𝔀 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ᴄᴏᴠɪᴅ **[{covid}]**", @"𝓢𝓱𝓸𝔀 𝓬𝓸𝓿𝓲𝓭 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ᴛᴏᴏʟꜱ **[{tools}]**", @"𝓢𝓱𝓸𝔀 𝓽𝓸𝓸𝓵𝓼 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField("\u200b", "\u200b", false);
                    embed.AddField($"ʜᴇʟᴘ ʀᴀɪᴅ **[{raid}]**", @"𝓢𝓱𝓸𝔀 𝓷𝓼𝓯𝔀 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ᴜᴛɪʟꜱ **[{utils}]**", @"𝓢𝓱𝓸𝔀 𝓾𝓽𝓲𝓵𝓼 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ᴍᴏᴅᴇʀᴀᴛɪᴏɴ **[{moderation}]**", @"𝓢𝓱𝓸𝔀 𝓶𝓸𝓭𝓮𝓻𝓪𝓽𝓲𝓸𝓷 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField("\u200b", "\u200b", false);
                    embed.AddField($"ʜᴇʟᴘ ᴇxᴘᴇʀɪᴍᴇɴᴛᴀʟ **[{experimental}]**", @"𝓢𝓱𝓸𝔀 𝓮𝔁𝓹𝓮𝓻𝓲𝓶𝓮𝓷𝓽𝓪𝓵𝓼 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ᴀᴄᴛɪᴠɪᴛʏ **[{activity}]**", @"𝓢𝓱𝓸𝔀 𝓪𝓬𝓽𝓲𝓿𝓲𝓽𝔂 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);
                    embed.AddField($"ʜᴇʟᴘ ᴏᴛʜᴇʀꜱ **[{others}]**", @"𝓢𝓱𝓸𝔀 𝓸𝓽𝓱𝓮𝓻𝓼 𝓬𝓸𝓶𝓶𝓪𝓷𝓭𝓼", true);*/
                    embed.ThumbnailUrl = "https://media1.tenor.com/images/1e158cadbc4e98e60d95fdff49b1ad25/tenor.gif";

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
