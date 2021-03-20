using Discord.Commands;
using Discord.Gateway;
using Leaf.xNet;
using System.Drawing;
using Colorful;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Discord;

namespace RamokSelfbot.Commands.Experimental
{
    [Command("test", "Just a console log command")]
    class Test : CommandBase
    {

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                Console.WriteLine("test");
            }
        }

 
    }






}
