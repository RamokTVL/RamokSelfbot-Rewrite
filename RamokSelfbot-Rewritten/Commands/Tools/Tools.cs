using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.Tools
{
    [Command("tools", "Show tools commands - HELPMENU")]
    class Tools : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Title = "Tools help menu",
                    Description = "A list of tools commands",
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

                    switch (cmd.Description)
                    {
                        case string a when a.Contains("- TOOLS"):
                            args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 8, 8)}```");
                            embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                            break;
                    }


                }

    

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
