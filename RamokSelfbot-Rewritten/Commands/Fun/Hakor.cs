using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("hac", "Show informations on the console like tree - FUN")]
    class Hac : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {
                hak = true;
                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = "<@" + Message.Author.User.Id + "> is now:\n (use " + Client.CommandHandler.Prefix + "stophac to disable this)",
                    ImageUrl = "https://i.imgflip.com/3lqr77.jpg"
                };

                RamokSelfbot.Utils.SendEmbed(Message, embed);

                Random rnd = new Random();
                new Thread(new ThreadStart(hakv)).Start();
            }
        }

        public void hakv()
        {
            while (hak == true)
            {
                int spaces = new Random().Next(0, 15);
                switch (spaces)
                {
                    case 1:
                        Console.Write(" ");
                        break;
                    case 2:
                        Console.Write("  ");
                        break;
                    case 3:
                        Console.Write("   ");
                        break;
                    case 4:
                        Console.Write("    ");
                        break;
                    case 5:
                        Console.Write("     ");
                        break;
                    case 6:
                        Console.Write("      ");
                        break;
                    case 7:
                        Console.Write("       ");
                        break;
                    case 8:
                        Console.Write("        ");
                        break;
                    case 9:
                        Console.Write("         ");
                        break;
                    case 10:
                        Console.Write("          ");
                        break;
                    case 11:
                        Console.Write("           ");
                        break;
                    case 12:
                        Console.Write("            ");
                        break;
                    case 13:
                        Console.Write("             ");
                        break;
                    case 14:
                        Console.Write("              ");
                        break;
                    case 15:
                        Console.Write("               ");
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                Console.WriteLine(new String(stringChars));
            }

            Thread.CurrentThread.Abort();
        }


        public static bool hak;
    }
}
