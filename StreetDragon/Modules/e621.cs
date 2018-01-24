using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MarkovSharp.TokenisationStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;

namespace StreetDragon.Modules
{
    public class e621 : ModuleBase<SocketCommandContext>
    {
        [Command("e621"), Alias("e6")]
        [Summary("Retreives filthy animal porn. For degenerates only")]
        public async Task RetreivePorn(params string[] args)
        {
            string tags = String.Join(" ", args);
            Random rnd1 = new Random();
            if (!Context.Channel.IsNsfw) { tags = tags + " rating:safe"; }
            else { tags = tags + " rating:explicit"; }
            try
            {
                List<E6_SHOW_RESP> Results = SearchForPosts(tags);
                if (Results.Count > 0)
                {
                    Double idx = rnd1.NextDouble() * Results.Count;
                    E6_SHOW_RESP chosen = (Results[Convert.ToInt16(Math.Floor(idx))]);
                    var embed = new EmbedBuilder();
                    embed.Url = "https://e621.net/post/show/" + chosen.id + "/";
                    embed.ImageUrl = chosen.sample_url;

                    var author = new EmbedAuthorBuilder();
                    author.Url = "https://e621.net/post/search?tags=" + chosen.artist[0] + "/";
                    author.Name = chosen.artist[0];
                    embed.Author = author;
                    embed.Color = Color.DarkBlue;
                    await ReplyAsync("", false, embed);
                }
                else
                {
                    if (Context.Channel.IsNsfw)
                    {
                        if (tags.Split(' ').Count() > 4)
                        {
                            await ReplyAsync("Lets face it, that's perhaps a bit TOO fetishy... I couldn't find anything");
                        }
                        else
                        {
                            await ReplyAsync("Kinky, but incorrectly spelt. Lets give that another try, shall we?");
                        }
                    }
                    else
                    {
                        await ReplyAsync("http://i.imgur.com/FgljrHU.jpg");
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    if (Context.Channel.IsNsfw)
                    {
                        await ReplyAsync("I cannot cope. https://static1.e621.net/data/bc/2a/bc2ac1ff9142544726e285c84ffd33fb.jpg");
                    }
                    else
                    {
                        await ReplyAsync("I know you wanted that image, but e621 is being too slow right now... https://static1.e621.net/data/sample/80/da/80dad8cdf18cd118c44780b7c6ab73e1.jpg");
                    }
                }
                else
                {
                        await ReplyAsync(e.Status.ToString() + " https://static1.e621.net/data/f9/1f/f91f92ac563c3bbe4e569d68a46dfafe.png");
                }
            }
           
            catch (Exception ex)
            {
                await ReplyAsync("``` \r\n FUCKED UP - " + ex.Message + "\r\n Stack: \r\n" + ex.StackTrace + "```");
            }
           
            





        }


        public class E6_SHOW_RESP
        {
            public string id { get; set; } /*
            public string author { get; set; }
            public string creator_id { get; set; }
            public string created_at { get; set; }
            public string status { get; set; }
            public string source { get; set; }
            public string sources { get; set; }
            public string tags { get; set; }
            */
            public string[] artist { get; set; }
            /*
            public string description { get; set; }
            public string fav_count { get; set; }
            public string score { get; set; }
            public string rating { get; set; }
            public string parent_id { get; set; }
            public string has_children { get; set; }
            public string children { get; set; }
            public string has_notes { get; set; }
            public string has_comments { get; set; }
            public string md5 { get; set; }
            public string file_url { get; set; }
            public string file_ext { get; set; }
            public string file_size { get; set; }
            public string width { get; set; }
            public string height { get; set; } */
            public string sample_url { get; set; } /*
            public string sample_width { get; set; }
            public string sample_height { get; set; }
            public string preview_url { get; set; }
            public string preview_width { get; set; }
            public string preview_height { get; set; }
            public string delreason { get; set; }*/
        }

        static List<E6_SHOW_RESP> SearchForPosts(String tags)
        {
            HttpClient client = new HttpClient();
            String url = "https://e621.net/post/index.json?tags=" + tags;
            
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<E6_SHOW_RESP>));
                request.UserAgent ="StreetDragon 0.666 (Discord Bot)";
                request.Timeout = 5000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                List<E6_SHOW_RESP> p2 = (List<E6_SHOW_RESP>)ser.ReadObject(stream);



                return p2;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
