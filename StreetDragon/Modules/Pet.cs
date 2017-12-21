using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules 
{
    public class Pet : ModuleBase<SocketCommandContext>
    {
        [Command("pet"),Alias("Pet")]
        [Summary("Pet someone (defaults to yourself)!")]
        public async Task petting(SocketGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync($"*Pets {Context.User.Mention}~!*");
            }
            else
                await ReplyAsync($"*Pets {user.Mention}~!*");

        }


    }
}
