using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RamokSelfbot.Commands.Covid
{
    [Command("getdata", "Get data (covid) for the France - COVID")]
    class GetData : CommandBase
    {
        public override void Execute()
        {
            if(Message.Author.User.Id == Program.id)
            {


                string raw = new WebClient().DownloadString("https://disease.sh/v2/countries/France?yesterday=true&strict=true");
                string raw2 = new WebClient().DownloadString("https://coronavirusapi-france.now.sh/FranceLiveGlobalData");
                Root parsedrawgetdataglobal = JsonConvert.DeserializeObject<Root>(raw2);
                Root2 altapi = JsonConvert.DeserializeObject<Root2>(raw);


                EmbedMaker embed = new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Title = "Covid-19 STATS (FRANCE)"
                };



              //  embed.AddField("⌚ Date", parsedrawgetdataglobal.Date, false);
                embed.AddField("🦠 Cas confirmés", parsedrawgetdataglobal.FranceGlobalLiveData[0].casConfirmes.ToString(), true);
                embed.AddField("🏥 Hospitalisés", parsedrawgetdataglobal.FranceGlobalLiveData[0].hospitalises.ToString(), true);
                embed.AddField("💀 Decès", parsedrawgetdataglobal.FranceGlobalLiveData[0].deces.ToString(), true);
                embed.AddField("💀 Decès Ephad", parsedrawgetdataglobal.FranceGlobalLiveData[0].decesEhpad.ToString(), true);
                embed.AddField("🏃 Guéris", parsedrawgetdataglobal.FranceGlobalLiveData[0].gueris.ToString(), true);
             //   embed.AddField("😫 Réanimations", parsedrawgetdataglobal..ToString(), true);
                embed.AddField("Stats pour aujourd'hui", "\u200B", false);
                embed.AddField("😫 Nouvelles Réanimations", parsedrawgetdataglobal.FranceGlobalLiveData[0].nouvellesReanimations.ToString(), true);
                embed.AddField("🦠 Nouveaux cas", altapi.todayCases.ToString(), true);
                embed.AddField("💀 Morts", altapi.todayDeaths.ToString(), true);
                embed.AddField("🏃 Guéris aujourd'hui", altapi.todayRecovered.ToString(), true);
                embed.AddField("🏥 Nouvelles Hospitalisations", parsedrawgetdataglobal.FranceGlobalLiveData[0].nouvellesHospitalisations.ToString(), true);
                embed.AddField("\u200B", "\u200B", false);
                embed.AddField("Source(s)", "Ministère des Solidarités et de la Santé", false);

                RamokSelfbot.Utils.SendEmbed(Message, embed);
            }
        }


    }

    public class Source
    {
        public string nom { get; set; }
    }

    public class Root2
    {
        public int todayCases { get; set; }
        public int todayDeaths { get; set; }
        public int todayRecovered { get; set; }
        public int tests { get; set; }
        public int population { get; set; }
   
    }

    public class FranceGlobalLiveData
    {
        public string date { get; set; }
        public Source source { get; set; }
        public string sourceType { get; set; }
        public int casConfirmes { get; set; }
        public int deces { get; set; }
        public int decesEhpad { get; set; }
        public int hospitalises { get; set; }
        public int reanimation { get; set; }
        public int gueris { get; set; }
        public int casConfirmesEhpad { get; set; }
        public int nouvellesHospitalisations { get; set; }
        public int nouvellesReanimations { get; set; }
        public string nom { get; set; }
        public string code { get; set; }
    }

    public class Root
    {
        public List<FranceGlobalLiveData> FranceGlobalLiveData { get; set; }
    }






}
