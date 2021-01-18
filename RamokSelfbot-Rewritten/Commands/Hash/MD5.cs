using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Hash
{
    [Command("md5", "Send the md5 hash of the string. - HASH")]
    class MD5 : CommandBase
    {
        [Parameter("input", false)]
        public string text { get; set; }

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Description = "Do not use MD5 for password encryption",
                };

                 embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);
                embed.AddField("Original text", text, false);
                embed.AddField("MD5 HASH", CreateMD5(text), false);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }

        private static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
