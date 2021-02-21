using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace RamokSelfbot.Commands.Others
{
    [Command("others", "Show commands that I didn't know which category to put in - HELPMENU")]
    class Others : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Title = "Others help menu",
                    Description = "A list of informatives commands",
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb)
                };

                foreach (var cmd in Client.CommandHandler.Commands.Values)
                {
                    StringBuilder args = new StringBuilder();
                    foreach (var arg in cmd.Parameters)
                    {
                        if (arg.Optional)
                            args.Append($" <{arg.Name}>");
                        else
                            args.Append($" [{arg.Name}]");
                    }


                    if (cmd.Description.Contains("- OTHERS"))
                    {
                        args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 9, 9)}```");
                        embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                    }


                }

                if (Message.Author.User.Avatar.Url == null)
                {
                    embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3" };
                }
                else
                {
                    embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3", IconUrl = Message.Author.User.Avatar.Url };
                }

                if (Message.Guild == null)
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                    return;
                }
                else
                {
                    if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator)) //CHECK DE PERMISSIONS
                    {
                        Message.Edit(new Discord.MessageEditProperties()
                        {
                            Content = "",
                            Embed = embed
                        });
                        return;
                    }
                }
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if (Message.Author.User.Id == Program.id)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
