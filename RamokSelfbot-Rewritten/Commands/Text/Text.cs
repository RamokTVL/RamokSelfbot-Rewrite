using Discord;
using Discord.Commands;
using System.Text;

namespace RamokSelfbot.Commands.Text
{
    [Command("text", "Help menu for texts commands - HELPMENU")]
    class Text : CommandBase
    {
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Title = "Text help menu",
                    Description = "Text commands list !",
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


                    if (cmd.Description.Contains("- TEXT"))
                    {
                        args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 7, 7)}```");
                        embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                    }


                }

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
