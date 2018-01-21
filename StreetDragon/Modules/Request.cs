using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Request : ModuleBase<SocketCommandContext>
    {
        [Command("request"),Alias("Request")]
        [Summary("Request a drawing (Costs 3000 CC each)")]
        public async Task request(string character = null, string pose = null)
        {
            User u = Program.UL[Context.User.Id];
            string path = Config.REQUEST_FILE;

            if (pose == null)
            {
                var builder = new EmbedBuilder()
                {
                    Color = new Color(255,242,0),
                    Description = "To get a request, type following this model :\n '!request character pose'\n Each request costs 3000 CC, and you can only have one request on hold at once."
                };

                await ReplyAsync("", false, builder.Build());

            }
            else
            {
                if (u.hasRequest == true)
                {
                    await ReplyAsync("But you already have a request on hold!");
                }
                else
                {
                    if (u.cutecoins >= 3000)
                    {
                        try
                        {
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                            u.hasRequest = true;
                            using (StreamWriter sw = new StreamWriter(path))
                            {
                                foreach (var user in Program.UL)
                                {
                                    if (user.Value.hasRequest == true)
                                    {
                                        sw.WriteLine(u.username);
                                        sw.WriteLine(u.userID);
                                        sw.WriteLine(character);
                                        sw.WriteLine(pose);
                                        sw.WriteLine("");
                                    }
                                }
                            }
                            u.cutecoins -= 3000;
                            await ReplyAsync("Your request got registered!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
                    }
                    else
                    {
                        await ReplyAsync("You don't have enough CuteCoins to get one :(");
                    }
                }
                
            }
            
        }

    }
}
