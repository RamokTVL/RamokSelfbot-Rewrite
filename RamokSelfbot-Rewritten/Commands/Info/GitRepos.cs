using Discord.Commands;
using System;
using Discord;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Leaf.xNet;

namespace RamokSelfbot.Commands.Info
{
    [Command("gitrepos", "Get informations about a git repos - INFO")]
    class GitRepos : CommandBase
    {
        [Parameter("linktotherepos", false)]
        public string linktotherepos { get; set; }
        public override void Execute()
        {
            if (RamokSelfbot.Utils.IsClient(Message))
            {        
                if(!Regex.Match(linktotherepos, "/github|https/").Success)    
                {
                    RamokSelfbot.Utils.SendEmbed(Message, new EmbedMaker()    
                    {            
                        Color = RamokSelfbot.Utils.EmbedColor(),                      
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),                    
                        Description = "Please set the full github link to the repository in https !\nEx : https://github.com/RamokTVL/RamokSelfbot-Rewritten"       
                    });    
                } else           
                {
                    string link = "";
                    link = linktotherepos;
                    link = link.Remove(0, 18);
                    link = "https://api.github.com/repos" + link;
                    
                    HttpRequest request = new HttpRequest();

                    request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.35 (KHTML, like Gecko) Chrome/87.0.4324.180 Safari/532.36");
                    string result = "";
                    try
                    {
                        result = request.Get(link).ToString();
                    } catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    
                    Root githubapi = JsonConvert.DeserializeObject<Root>(result);
                    EmbedMaker embed = new EmbedMaker()
                    {
                        Color = RamokSelfbot.Utils.EmbedColor(),
                        Footer = RamokSelfbot.Utils.footer(Message.Author.User),
                        Description = "Git repository : " + linktotherepos
                    };

                    embed.AddField("Name", githubapi.full_name, true);
                    embed.AddField("ID", githubapi.id.ToString(), true);
                    embed.AddField("Node ID", githubapi.node_id.ToString(), true);
                    embed.AddField("Watchers Count", githubapi.watchers_count.ToString(), true);
                    embed.AddField("Subscribers Count", githubapi.subscribers_count.ToString(), true);
                    embed.AddField("Forks Count", githubapi.forks.ToString(), true);
              
                    embed.AddField("Disabled?", githubapi.disabled.ToString(), true);
                    embed.AddField("Archived?", githubapi.archived.ToString(), true);
                    embed.AddField("Homepage", githubapi.homepage.ToString(), false);
                    embed.AddField("Main branch", githubapi.default_branch.ToString(), true);
                    embed.AddField("Language", githubapi.language.ToString(), true);

                    RamokSelfbot.Utils.SendEmbed(Message, embed);
                }          
            }    
        }


    }
    
    public class Owner
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }

        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class License
    {
        public string key { get; set; }
        public string name { get; set; }
        public string spdx_id { get; set; }
        public string url { get; set; }
        public string node_id { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public bool @private { get; set; }
        public Owner owner { get; set; }
        public string description { get; set; }
        public bool fork { get; set; }
        public string url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime pushed_at { get; set; }
        public string git_url { get; set; }
        public string homepage { get; set; }
        public int size { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
        public string language { get; set; }
        public bool has_issues { get; set; }
        public bool has_projects { get; set; }
        public bool has_downloads { get; set; }
        public bool has_wiki { get; set; }
        public bool has_pages { get; set; }
        public int forks_count { get; set; }
        public bool archived { get; set; }
        public bool disabled { get; set; }
        public int open_issues_count { get; set; }
        public License license { get; set; }
        public int forks { get; set; }
        public int open_issues { get; set; }
        public int watchers { get; set; }
        public string default_branch { get; set; }
        public object temp_clone_token { get; set; }
        public int network_count { get; set; }
        public int subscribers_count { get; set; }
    }
}
