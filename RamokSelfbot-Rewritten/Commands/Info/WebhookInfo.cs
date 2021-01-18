using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Info
{
    [Command("webhookinfo", "Get informations about a token - INFO")]
    class WebHookInfo : CommandBase
    {
        [Parameter("id")]
        public string id { get; set; }

        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                WebClient client = new WebClient();
                if (id.Contains("http"))
                {
                    string json = client.DownloadString(id);
                    Webhook web = Newtonsoft.Json.JsonConvert.DeserializeObject<Webhook>(json);
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Title = "Webhook informations for " + web.id,
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Description = "Yay ! Selfbot made by Ramok with <3",
                    };

                    embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);

                    SocketGuild guild;
                    DiscordUser owner;
                    try
                    {
                        guild = Client.GetCachedGuild(ulong.Parse(web.guild_id));
                        owner = Client.GetUser(guild.OwnerId);
                    } catch 
                    {
                        RamokSelfbot.Utils.Print("Error while executing the webhookinfo command.");
                        return;
                    }
                    DiscordChannel channel = Client.GetChannel(ulong.Parse(web.channel_id));
                    embed.AddField("Guild", guild.Name, true);
                    embed.AddField("Guild owner", owner.Username, true);
                    embed.AddField("Channel", channel.Name, false);


                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                }
            }
        }
    }

    class Webhook
    {
        public string name { get; set; }
        public string avatar { get; set; }
        public string channel_id { get; set; }
        public string guild_id { get; set; }
        public string id { get; set; }
    }
}
