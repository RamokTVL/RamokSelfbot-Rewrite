using Discord.Commands;
using Discord;
using System;
using System.Text;
namespace RamokSelfbot.Commands.Backup
{
    [Command("backup", "Show backups commands")]
    class Backup : CommandBase
    {
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Title = "Backup help menu",
                        Description = "A list of informatives commands",
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
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


                        if (cmd.Description.Contains("- BACKUP"))
                        {
                            args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 9, 9)}```");
                            embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                        }


                    }


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
