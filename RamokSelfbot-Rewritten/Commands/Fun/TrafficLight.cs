using Discord.Commands;
using Discord;

namespace RamokSelfbot.Commands.Fun
{
    [Command("trafficlight", "Timeout in traffic light graphic - FUN")]
    class TrafficLight : CommandBase
    {
        [Parameter("secondstowait", false)]
        public int secondstowait { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = ":red_circle::red_circle::red_circle::red_circle::red_circle:\n:red_circle::red_circle::red_circle::red_circle::red_circle:\n:red_circle::red_circle::red_circle::red_circle::red_circle:",
                    Title = "On your marks !"
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
                System.Threading.Thread.Sleep(secondstowait * 1000 / 3);
                embed.Title = "Get set !";
                embed.Description = ":orange_circle::orange_circle::orange_circle::orange_circle::orange_circle:\n:orange_circle::orange_circle::orange_circle::orange_circle::orange_circle:\n:orange_circle::orange_circle::orange_circle::orange_circle::orange_circle: ";
                RamokSelfbot.Utils.SendEmbed(Message, embed);
                System.Threading.Thread.Sleep(secondstowait * 1000 / 3);
                embed.Title = "Go !";
                embed.Description = ":green_circle::green_circle::green_circle::green_circle::green_circle:\n:green_circle::green_circle::green_circle::green_circle::green_circle:\n:green_circle::green_circle::green_circle::green_circle::green_circle: ";
                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }
}
