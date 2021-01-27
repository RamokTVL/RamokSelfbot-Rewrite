using Discord.Commands;
using Discord.Gateway;
using Discord;
using Newtonsoft.Json;
using System.IO;

namespace RamokSelfbot.Commands.StatusActivity
{
    [Command("streaming", "Set ur status to \"streaming\" on twitch. - ACTIVITY")]
    class Streaming : CommandBase
    {
        [Parameter("activity")]
        public string activity { get; set; }
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Client.UpdatePresence(new PresenceProperties()
                {
                    Status = UserStatus.DoNotDisturb,
                    Activity = new StreamActivityProperties() { Name = activity, Url = JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).twitchlink }
                });
            }
        }

    }
}
