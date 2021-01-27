using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.NSFW
{
    [Command("nsfw", "This command display NSFWs commands. - NSFW")]
    class NSFW : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                if (JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json")).nsfw)
                {
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "This is only a help menu and commands need to be used in a NSFW channel."
                    };

                    foreach (var cmd in Client.CommandHandler.Commands.Values)
                    {
                        if (cmd.Description.Contains("- NSFW"))
                        {
                            embed.AddField(Client.CommandHandler.Prefix + cmd.Name, "```\nNSFW command without description```", false);
                        }
                    }

                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                } else
                {
                    RamokSelfbot.Utils.Print("This is a nsfw command ! U need to enable nsfw in config.json or GUI to use this command !");
                    Message.Delete();
                    return;
                }
            }
        }
    }

    public class NSFWOPTIONS
    {
        public int color { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public string version { get; set; }
    }
}
