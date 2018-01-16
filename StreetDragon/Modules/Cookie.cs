using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Cookie : ModuleBase<SocketCommandContext>
    {
        [Command("cookie"), Alias("Cookie")]
        [Summary("Give a cookie to someone (defaults to eating one yourself)!")]
        public async Task Cookies(SocketGuildUser user = null)
        {
            User u = Program.UL[Context.User.Id];
            
            if (user == null)
            {
                if (u.cookies >= 1)
                {
                    u.cookies -= 1;
                    u.gainXP(3);
                    await ReplyAsync($"*{Context.User.Mention} ate a cookie~!*");
                }
                else
                {
                    await ReplyAsync($"But you don't have any cookie! Try getting one from the !shop .");
                } 
            }
            else
            {
                if (u.cookies >= 1)
                {
                    User u2 = Program.UL[user.Id];
                    u.cookies -= 1;
                    u2.cookies += 1;
                    await ReplyAsync($"*{Context.User.Mention} gives a cookie to {user.Mention}~!*");
                }
                else
                {
                    await ReplyAsync($"But you don't have any cookie! Try getting one from the !shop .");
                }
            }
        }
    }
}
