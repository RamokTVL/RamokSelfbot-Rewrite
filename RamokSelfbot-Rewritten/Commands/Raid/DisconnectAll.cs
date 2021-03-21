using Discord.Commands;
using Discord;
using Discord.Gateway;

namespace RamokSelfbot.Commands.Raid
{
    [Command("disconnectall", "Disconnect all member from a VC - RAID")]
    class DisconnectAll : CommandBase
    {
        [Parameter("id of the vc")]
        public ulong id { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {

                    if(Client.GetChannel(id) != null)
                    {
                        if(Client.GetChannel(id).IsVoice)
                        {
                        RamokSelfbot.Utils.Print("Will be added later");
                        return;
                        } else
                    {
                        RamokSelfbot.Utils.Print("not a vc !");
                    }
                    } else
                    {
                        RamokSelfbot.Utils.Print("Channel doesn't exists !");
                    }

            }
        }
    }
}
