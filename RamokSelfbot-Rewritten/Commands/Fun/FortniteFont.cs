using Discord;
using Discord.Commands;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace RamokSelfbot.Commands.Fun
{
    [Command("fortnitefont", "Create a text with the fortnite font. - FUN")]
    class FortniteFont : CommandBase
    {
        [Parameter("hexcolor", false)]
        public string color { get; private set; }

        [Parameter("size", false)]
        public string size { get; private set; }

        [Parameter("text", false)]
        public string text { get; private set; }

        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {
                string url = "http://fortnitefontgenerator.com/img.php?fontsize=" + size + "&textcolor=" + color + "&text=" + text.Replace(" ", "%20");



                if (color.Contains("#"))
                {
                    EmbedMaker syntaxerror = new EmbedMaker()
                    {
                        Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                        Description = "If you cant find ur error, try !!fun to get the informations.\nThe hexcolor is without \"#\".\n\nThe FortniteFont command cant be executed due to a syntaxerror, please retry with the good syntax.",
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User)
                    };



                    RamokSelfbot.Utils.SendEmbed(Message, syntaxerror);
                }
                else
                {
                    if (new WebClient().DownloadString(url) == "")
                    {
                        EmbedMaker syntaxerror = new EmbedMaker()
                        {
                            Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                            Description = "If you cant find ur error, try !!fun to get the informations.\nThe hexcolor is without \"#\".\n\nThe FortniteFont command cant be executed due to a syntaxerror, please retry with the good syntax.",
                            Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        };

                        RamokSelfbot.Utils.SendEmbed(Message, syntaxerror);
                    }


                    EmbedMaker embed = new EmbedMaker()
                    {
                        ImageUrl = url,
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    };


                    RamokSelfbot.Utils.SendEmbed(Message, embed);



                }
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
