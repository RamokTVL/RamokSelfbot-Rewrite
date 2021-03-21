using Discord;
using Discord.Commands;
using Leaf.xNet;

namespace RamokSelfbot.Commands.Info
{
    [Command("otherownersprojects", "Show the others projects of the owner.. - INFO")]
    class OtherOwnerProjects : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                HttpRequest req = new HttpRequest();
                var res = req.Get("https://ramok.herokuapp.com/api/otherprojects").ToString();
                EmbedMaker embed = new EmbedMaker()
                {
                    Title = "Other owner projects",
                    Description = res,
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
