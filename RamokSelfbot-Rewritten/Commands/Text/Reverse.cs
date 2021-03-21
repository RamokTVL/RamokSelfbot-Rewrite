using Discord.Commands;
using Discord;
using System;

//Found the method for reverse string here : https://www.dailyrazor.com/blog/csharp-reverse-string/#:~:text=To%20do%20this%2C%20we%20use,the%20letters%20in%20that%20string. (bcz mine was not optimized)

namespace RamokSelfbot.Commands.Text
{
    [Command("reverse", "Reverse a text - TEXT")]
    class Reverse : CommandBase
    {
        [Parameter("text to reverse")]
        public string text { get; set; }
        public override void Execute()
        {
            if(RamokSelfbot.Utils.IsClient(Message))
            {
                Message.Edit(new MessageEditProperties()
                {
                    Content = ReverseString(text)
                }); 
            }
        }



        private static string ReverseString(string Str)
        {
            char[] myArr = Str.ToCharArray();
            Array.Reverse(myArr);
            return new string(myArr);
        }
    }
}
