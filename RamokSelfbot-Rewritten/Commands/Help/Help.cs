using Discord.Commands;
using Discord;
using System;
using Discord.Gateway;
using Newtonsoft.Json;
using System.IO;

namespace RamokSelfbot.Commands.Help
{
    [Command("help", "Help you to use the selfbot")]
    class Help : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Title = "Help menu",
                    Description = "This menu is made for help you. \nIf you dont understand it, you are a retard." +
                    "\n\n" +
                    "**" + Client.CommandHandler.Prefix + "fun**\n```\nShow informations about the funs commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "info**\n```\nShow informations about the informatives commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "utils**\n```\nShow informations about the utils commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "moderation**\n```\nShow informations about the moderation commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "others**\n```\nShow informations about the commands that are not listed on the 4 others menus```",
                };

                if(Message.Author.User.Avatar.Url == null)
                {
                    embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3" };
                } else
                {
                    embed.Footer = new EmbedFooter() { Text = "Selfbot rewritten by Ramok with <3", IconUrl = Message.Author.User.Avatar.Url };
                    embed.ThumbnailUrl = Message.Author.User.Avatar.Url;
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
            if(Message.Author.User.Id == Program.id)
            {
                Colorful.Console.WriteLine(exception.Message, System.Drawing.Color.White);
            }
        }
    }
}
