using Discord.Commands;
using Discord.Gateway;
using Leaf.xNet;
using System.Drawing;
using Colorful;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Discord;

namespace RamokSelfbot.Commands.Experimental
{
    [Command("geoip", "Get informations about the IP address provided - UTILS")]
    class GeoIP : CommandBase
    {
        [Parameter("IP")]
        public string ip { get; set; }
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {
                Root geo = JsonConvert.DeserializeObject<Root>(new HttpRequest().Get($"http://ip-api.com/json/{ip}?fields=message,country,regionName,city,org,mobile,proxy,hosting").ToString());
                RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()
                {
                    Color = RamokSelfbot.Utils.EmbedColor(),
                    Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                    Description = $"```\nIP           : {ip}\nOrganization : {geo.org}\nMobile ?     : {geo.mobile}\nProxy ?      : {geo.proxy}\nHosting ?    : {geo.hosting}\n\nGeo infos    : \n\nCountry : {geo.country}\nRegion  : {geo.regionName}\nCity    : {geo.city}```"
                });
            }
        }
    }

    public class Root
    {
        public string country { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string org { get; set; }
        public bool mobile { get; set; }
        public bool proxy { get; set; }
        public bool hosting { get; set; }
    }
}
