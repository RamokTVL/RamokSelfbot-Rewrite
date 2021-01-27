using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Hash
{
    [Command("sha256", "Send the sha256 hash of the string. - HASH")]
    class SHA256Cmd : CommandBase
    {
        [Parameter("text")]
        public string text { get; set; }

        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Description = "Do not use SHA256 for password encryption",
                };

                embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);
                embed.AddField("Original text", text, false);
                embed.AddField("SHA256 HASH", ComputeSha256Hash(text), false);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }
    }

}
