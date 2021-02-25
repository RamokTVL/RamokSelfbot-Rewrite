using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Discord;

namespace RamokSelfbot.Commands.Fun
{
    [Command("hourenjoy", "Respond to the question \"What time is it ?\" (og work from http://quelleheureestilenjoy.com/) - FUN")]
    class Hour : CommandBase
    {
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                HttpRequest request = new HttpRequest();
                var date = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
                if(Program.Debug)
                {
                    Console.WriteLine(date);
                }

                request.AddHeader("hour", date);
                string link = request.Get("https://ramok.herokuapp.com/hourenjoy").ToString();
                /*   EmbedMaker embed = new EmbedMaker()
                   {
                       ImageUrl = link
                   };

                   RamokSelfbot.Utils.SendEmbed(Message, embed);*/

                Message.Edit(new MessageEditProperties()
                {
                    Content = link
                });
            }
        }
    }
}
