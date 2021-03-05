using Discord.Commands;
using System.Threading;

namespace RamokSelfbot.Commands.Utils
{
    [Command("hypesquad", "Allow you to change your hypesquad - UTILS")]
    class Hypesquad : CommandBase
    {
        [Parameter("hypesquad")]
        public string hype { get; set; }
        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                switch(hype.ToLower())
                {
                    case "none":
                        Client.User.SetHypesquad(Discord.Hypesquad.None);
                        Client.User.Update();
                        break;
                    case "balance":
                        Client.User.SetHypesquad(Discord.Hypesquad.Balance);
                        Client.User.Update();
                        break;  
                    case "bravery":
                        Client.User.SetHypesquad(Discord.Hypesquad.Bravery);
                        Client.User.Update();
                        break;     
                    case "brilliance":
                        Client.User.SetHypesquad(Discord.Hypesquad.Brilliance);
                        Client.User.Update();
                        break;
                }

                Message.Edit(new Discord.MessageEditProperties()
                {
                    Content = "Set hypesquad to " + Client.User.Hypesquad.ToString() + " :white_check_mark:"
                });
            }
        }


    }
}
