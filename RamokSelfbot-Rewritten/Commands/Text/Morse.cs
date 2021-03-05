using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Text
{
    [Command("morse", "Translates the given text into Morse code")]
    class Morse : CommandBase
    {
        [Parameter("text to translate")]
        public string text { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                if(!string.IsNullOrEmpty(text))
                {
                    string ogtext = text;
                    var translated = text;
                    translated = translated.Replace("a", ". - ");
                    translated = translated.Replace("b", "- . . . ");
                    translated = translated.Replace("c", "- . - . ");
                    translated = translated.Replace("d", "-. . ");
                    translated = translated.Replace("e", ". ");
                    translated = translated.Replace("f", ". . - . ");
                    translated = translated.Replace("g", "- - . ");
                    translated = translated.Replace("h", ". . . . ");
                    translated = translated.Replace("i", ". . ");
                    translated = translated.Replace("j", ". - - - ");
                    translated = translated.Replace("k", "- . - ");
                    translated = translated.Replace("l", ". - . . ");
                    translated = translated.Replace("m", "- - ");
                    translated = translated.Replace("n", "- . ");
                    translated = translated.Replace("o", "- - - ");
                    translated = translated.Replace("p", ". - - . ");
                    translated = translated.Replace("q", "- - . - ");
                    translated = translated.Replace("r", ". - . ");
                    translated = translated.Replace("s", ". . . ");
                    translated = translated.Replace("t", "- ");
                    translated = translated.Replace("u", ". . - ");
                    translated = translated.Replace("v", ". . . - ");
                    translated = translated.Replace("w", ". - - ");
                    translated = translated.Replace("x", "- . . - ");
                    translated = translated.Replace("y", "- . - - ");
                    translated = translated.Replace("z", "- - . . ");
                    translated = translated.Replace("1", ". - - - - ");
                    translated = translated.Replace("2", ". . - - - ");
                    translated = translated.Replace("3", ". . . - - ");
                    translated = translated.Replace("4", ". . . . - ");
                    translated = translated.Replace("5", ". . . . . ");
                    translated = translated.Replace("6", "- . . . . ");
                    translated = translated.Replace("7", "- - . . . ");
                    translated = translated.Replace("8", "- - - . . ");
                    translated = translated.Replace("9", "- - - - . ");
                    translated = translated.Replace("0", "- - - - - ");

                    Message.Edit(new Discord.MessageEditProperties()
                    {
                        Content = "> " + ogtext + "\n" + translated
                    });
                } else
                {
                    //PAS DE TEXTE
                    NoTextProvided();
                }
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                //PAS DE TEXTE
                NoTextProvided();
            }
        }

        private void NoTextProvided()
        {
            //TODO
        }
    }
}
