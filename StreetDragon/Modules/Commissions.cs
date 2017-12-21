using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Commissions : ModuleBase<SocketCommandContext>
    {
        [Command("commissions"),Alias("commission")]
        [Summary("Displays Wern's commission prices")]
        public async Task Commission()
        {
            var channel = Context.Channel;
            await channel.TriggerTypingAsync();
            await channel.SendFileAsync("c:\\users\\gaby\\documents\\visual studio 2017\\Projects\\StreetDragon\\StreetDragon\\Images\\Commish\\werncomm.png");
            await ReplyAsync("http://fav.me/d9cgmwy");
        }
    }
}
