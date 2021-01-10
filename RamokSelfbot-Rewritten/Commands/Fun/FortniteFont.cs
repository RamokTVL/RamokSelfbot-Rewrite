using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.IO;

namespace RamokSelfbot.Commands.Fun
{
    [Command("fortnitefont", "Create a text with the fortnite font. - FUN")]
    class FortniteFont : CommandBase
    {
        [Parameter("hexcolor")]
        public string color { get; private set; }

        [Parameter("size")]
        public string size { get; private set; }

        [Parameter("text")]
        public string text { get; private set; }

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                string url = "http://fortnitefontgenerator.com/img.php?fontsize=" + size + "&textcolor=" + color +"&text=" + text.Replace(" ", "%20");
                EmbedFooter footer = new EmbedFooter();
                if (Message.Author.User.Avatar.Url != null)
                {
                    footer.IconUrl = Message.Author.User.Avatar.Url;
                }

                footer.Text = "Selfbot rewritten by Ramok with <3";
                Message.Edit(new MessageEditProperties()
                {
                    Content = "",
                    Embed = new EmbedMaker()
                    {
                        ImageUrl = url,
                        Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                        Footer = footer
                    },
                });
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
                
            if (Message.Author.User.Id == Program.id)           
            {                 
                if(providedValue == null)            
                {          
                    RamokSelfbot.Utils.Print("Please provide a value for \"text\", FortniteFont command.");      
                } else        
                { 
                    Console.WriteLine("error occured : " + exception.Message);
                }
            }
        }
    }
}
