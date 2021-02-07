using Discord.Commands;
using Discord.Gateway;
using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RamokSelfbot.Commands.Experimental
{
    [Command("test", "Just a console log command")]
    class Test : CommandBase
    {

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
       


                
            }
        }

 
    }



}
