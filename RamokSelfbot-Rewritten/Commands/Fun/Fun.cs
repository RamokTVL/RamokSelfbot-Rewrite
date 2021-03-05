using System;
using Discord.Commands;
using Discord;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Fun
{
    [Command("fun", "Show fun commands. - HELPMENU")]
    class Fun : CommandBase
    {
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {


                EmbedMaker send1 = new EmbedMaker()
                {
                    Title = "Fun help menu",
                    Description = "A list of funs commands",
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                };

                EmbedMaker send2 = new EmbedMaker()
                {
                    Title = "Fun help menu",
                    Description = "A list of funs commands",
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                };

                int a = 0;

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


                    if (cmd.Description.Contains("- FUN"))
                    {
                        a++;
                        args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 6, 6)}```");
                        if (a < 20)
                        {
                            send1.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                        }
                        else
                        {
                            send2.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{args}");
                        }
                        args.Clear();
                    }




                }

                RamokSelfbot.Utils.SendEmbed(Message, send1);
                RamokSelfbot.Utils.SendEmbedRsendIdget(Message, send2);

                /*if(send2 != null) {
                  RamokSelfbot.Utils.SendEmbedRsendIdget(Message, send2);
                }*/

            }




            }


        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if(Message.Author.User.Id == Client.User.Id)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
