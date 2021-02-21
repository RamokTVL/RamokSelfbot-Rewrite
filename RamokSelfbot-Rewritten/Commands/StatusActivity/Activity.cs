using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace RamokSelfbot.Commands.Raid
{
    [Command("activity", "Show activities commands - HELPMENU")]
    class Activity : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Title = "Activity help menu",
                    Description = "A list of informatives commands",
                    Color = RamokSelfbot.Utils.EmbedColor(),
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


                    if (cmd.Description.Contains("- ACTIVITY"))
                    {
                        args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 11, 11)}```");
                        embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                    }


                }

                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
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
