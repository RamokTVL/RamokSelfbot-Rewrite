using Discord;
using Discord.Commands;
using WMPLib;

namespace RamokSelfbot.Commands.Music
{
    [Command("loop", "Set the music state to loop - MUSIC")]
    class Loop : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Play.mainSound.settings.setMode("loop", !Play.mainSound.settings.getMode("loop"));
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "Loop mode set to : " + Play.mainSound.settings.getMode("loop")
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);
        }
        }
    }
}
    
        
  

