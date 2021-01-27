using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Experimental
{
    [Command("experimental", "This command help you to use experimentals commands - EXPERIMENTAL")]
    class Experimental : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                if (JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).experimentalcommands == true)
                {
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Title = "Experimental help menu",
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


                        if (cmd.Description.Contains("- EXPERIMENTAL"))
                        {
                            args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 15, 15)}```");
                            embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                        }


                    }


                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                } else
                {
                    Message.Delete();
                    RamokSelfbot.Utils.Print("This command is a experimental command, change the \"experimentalcommands\" to true in config.json to use them!");
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
