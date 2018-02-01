using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Coinflip : ModuleBase<SocketCommandContext>
    {
        [Command("coinflip"),Alias("cf")]
        [Summary("Use as follows : !coinflip choice currency nb \n choice = heads or tails \n currency = CC or cookies \n nb = quantity you wanna bet")]
        public async Task coinflip(string currency,int nb = 1)
        {
            if (nb < 1) nb = 1;
            User u = Program.UL[Context.User.Id];
            int limit = (int)((20*u.cutecoins)/100);

            if (currency == "cc" || currency == "cookies")
            {
                if (currency == "cc" && nb > limit)
                {
                    await Context.Channel.SendMessageAsync($"You can't bet more than 20% of your current money (which is {limit} CC)!");
                }
                else if (currency == "cookies" && nb > u.cookies)
                {
                    await Context.Channel.SendMessageAsync($"You don't have this many cookies!!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} flipped a coin!");

                    Random r = new Random();
                    int ran = r.Next(0, 101);
                    if (ran <= 50)
                    {
                        await Context.Channel.SendMessageAsync($"...and won! This means they win double!");
                        if(currency == "cc")
                            u.cutecoins += nb;
                        else
                            u.cookies += nb;
                    }
                    else
                    {
                        if (currency == "cc")
                        {
                            await Context.Channel.SendMessageAsync($"...and lost! This means they lose their bet CC...");
                            u.cutecoins -= nb;
                        }

                        else
                        {
                            await Context.Channel.SendMessageAsync($"...and lost! This means they lose their bet cookies...");
                            u.cookies -= nb;
                        }
                        
                    }
                }
                
            }
        }
    }
}
