using System;
using Discord.Commands;
using Discord;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Fun
{
    [Command("fun", "This command is fun. - FUN")]
    class Fun : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Title = "Fun help menu",
                    Description = "A list of funs commands",
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


                    if (cmd.Description.Contains("- FUN"))
                    {
                        args.Append($"```\n{cmd.Description.Remove(cmd.Description.Length - 6, 6)}```");
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

                if (Message.Author.Member.GetPermissions().Has(DiscordPermission.AttachFiles) || Message.Author.Member.GetPermissions().Has(DiscordPermission.Administrator) || Client.GetCachedGuild(Message.Guild.Id).OwnerId == Program.id) //CHECK DE PERMISSIONS
                {
                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "",
                        Embed = embed
                    });
                }
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if(Message.Author.User.Id == Program.id)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
