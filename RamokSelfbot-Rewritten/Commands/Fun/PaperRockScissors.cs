using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Fun
{
    [Command("rps", "Play rock paper scissors ! - FUN")]
    class PaperRockScissors : CommandBase
    {
        [Parameter("id", true)]
        public string id { get; set; }

        public DiscordUser fighter;

        public override void Execute()
        {
            if (Message.Author.User.Id == Program.id)
            {

                Random rnd = new Random();
                int number = rnd.Next(1, 4);

                int number2 = rnd.Next(1, 4);

                fighter = Client.GetUser(Program.id);
                if (id == null)
                {
                    ValidUser(1);
                    return;
                }
                else
                {
                    if (id.Length == 18)
                    {
                        try
                        {
                            fighter = Client.GetUser(ulong.Parse(id));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Unknown User"))
                            {
                                ValidUser(3);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else if (Message.Mentions.Count > 1 || Message.Mentions.Count == 1)
                    {
                        try
                        {
                            fighter = Client.GetUser(Message.Mentions[0].Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            if (ex.Message.Contains("Unknown User"))
                            {
                                ValidUser(3);
                                return;
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        ValidUser(2);
                        return;
                    }


                    EmbedMaker embed = new EmbedMaker();
                    if (number == number2)
                    {
                        string e = "You both used";

                        if (number == 1)
                        {
                            e = e + " stone";
                        }
                        if (number == 2)
                        {
                            e = e + " leaf";
                        }
                        if (number == 3)
                        {
                            e = e + " scissors";
                        }
                        embed.AddField("Fixed equality", e);
                    }
                    else
                    {
                        if (number == 1)
                        {
                            if (number2 == 2)
                            {
                                embed.AddField(fighter.Username + " winned !", "You used stone against leaf !");
                            }
                            else if (number2 == 3)
                            {
                                embed.AddField(Message.Author.User.Username + " winned !", "You used stone against scissors !");
                            }
                        }
                        else if (number == 2)
                        {
                            if (number2 == 1)
                            {
                                embed.AddField(Message.Author.User.Username + " winned !", "You used sheet against stone !");
                            }
                            else if (number2 == 3)
                            {
                                embed.AddField(fighter.Username + " winned !", "You used leaf against scissors !");
                            }
                        }
                        else if (number == 3)
                        {
                            if (number2 == 1)
                            {
                                embed.AddField(fighter.Username + " winned !", "You used scissors against stone !");
                            }
                            else if (number2 == 2)
                            {
                                embed.AddField(Message.Author.User.Username + " winned !", "You used scissors against leaf !");
                            }
                        }
                    }




                    RamokSelfbot.Utils.SendEmbed(Message, embed);

                }

            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            base.HandleError(parameterName, providedValue, exception);
                        

                if (Message.Author.User.Id == Program.id)
                {
                    Console.WriteLine("error occured : " + exception.Message);
                }
            }

        private void ValidUser(int a)
        {
            if (Program.Debug == true)
            {
                Console.WriteLine(a.ToString());
            }

            EmbedMaker embed = new EmbedMaker()
            {
                Color = System.Drawing.Color.FromArgb(JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorr, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorg, JsonConvert.DeserializeObject<JSON>(File.ReadAllText("config.json")).embedcolorb),
                Description = "<@" + Message.Author.User.Id.ToString() + ">, please mention a valid user !"
            };


            embed.Footer = RamokSelfbot.Utils.footer(Message.Author.User);


            RamokSelfbot.Utils.SendEmbed(Message, embed);
        }

    }


    }

