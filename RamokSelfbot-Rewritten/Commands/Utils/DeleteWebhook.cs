using Discord.Commands;
using Leaf.xNet;
using Discord;

namespace RamokSelfbot.Commands.Utils
{
    [Command("deletewebhook", "Delete a webhook with a link only - UTILS")]
    class DeleteWebhook : CommandBase
    {
        [Parameter("link")]
        public string link { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                new HttpRequest().Delete(link);
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "get deteted ez",
                    Title = "Webhook Deleted"
                };

                embed.AddField("Webhook Deleted", "```\n" + link + "```", true);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
