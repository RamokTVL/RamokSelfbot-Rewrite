using Discord.Commands;
using Discord;
using System;
using Discord.Gateway;
using Newtonsoft.Json;
using System.IO;

namespace RamokSelfbot.Commands.Help
{
    [Command("help", "Help you to use the selfbot - UTILS")]
    class Help : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                int fun = 0;
                int hash = 0;
                int raid = 0;
                int utils = 0;
                int moderation = 0;
                int activity = 0;
                int others = 0;
                int covid = 0;
                int info = 0;
                int nsfw = 0;
                int experimental = 0;


                foreach(var cmds in Client.CommandHandler.Commands.Values)
                {
                    if(cmds.Description.Contains("- FUN"))
                    {
                        fun++;
                    }
                    else if (cmds.Description.Contains("- HASH"))
                    {
                        hash++;
                    } else if(cmds.Description.Contains("- RAID"))
                    {
                        raid++;
                    } else if(cmds.Description.Contains("- UTILS"))
                    {
                        utils++;
                    }else if (cmds.Description.Contains("- MODERATION"))
                    {
                        moderation++;
                    }else if(cmds.Description.Contains("- ACTIVITY"))
                    {
                        activity++;
                    }else if(cmds.Description.Contains("- OTHERS"))
                    {
                        others++;
                    }else if(cmds.Description.Contains("- INFO"))
                    {
                        info++;
                    }else if(cmds.Description.Contains("- EXPERIMENTAL"))
                    {
                        experimental++;
                    }
                    else if(cmds.Description.Contains("- COVID"))
                    {
                        covid++;
                    }else if(cmds.Description.Contains("- NSFW"))
                    {
                        nsfw++;
                    }
                }
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                    Title = "Help menu",
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                Description = "This menu is made for help you. \nIf you dont understand it, you are a retard." +
                    "\n\n" +
                    "**" + Client.CommandHandler.Prefix + "fun [" + fun.ToString() + "]" + "**\n```\nShow informations about the funs commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "info [" + info.ToString() + "]" + "**\n```\nShow informations about the informatives commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "hash [" + hash.ToString() + "]" + "**\n```\nShow informations about the hashes commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "nsfw [" + nsfw.ToString() + "]" + "**\n```\nThis command display NSFWs commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "raid [" + raid.ToString() + "]" + "**\n```\nShow informations about the raids commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "utils [" + utils.ToString() + "]" + "**\n```\nShow informations about the utils commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "covid [" + covid.ToString() + "]" + "**\n```\nGet the commands for covid stats (france only)```" +
                    "\n**" + Client.CommandHandler.Prefix + "moderation [" + moderation.ToString() + "]" + "**\n```\nShow informations about the moderation commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "experimental [" + experimental.ToString() + "]" + "**\n```\nThis command help you to use experimentals commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "activity [" + activity.ToString() + "]" + "**\n```\nShow informations about the activities commands```" +
                    "\n**" + Client.CommandHandler.Prefix + "others [" + others.ToString() + "]" + "**\n```\nShow informations about the commands that are not listed on the 4 others menus```",
                    //
                };


                RamokSelfbot.Utils.SendEmbed(Message, embed);
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
