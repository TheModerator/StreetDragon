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

    public class Hug : ModuleBase<SocketCommandContext>
    {
        [Command("hug"), Alias("Hug")]
        [Summary("Hug someone (defaults to yourself)!")]
        public async Task hug(SocketGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync($"*Hugs {Context.User.Mention}~!*");
            }
            else
                await ReplyAsync($"*Hugs {user.Mention}~!*");
        }

    }

    public class Cuddle : ModuleBase<SocketCommandContext>
    {
        [Command("cuddle"), Alias("Cuddle")]
        [Summary("Cuddle someone (defaults to yourself)!")]
        public async Task cuddle(SocketGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync($"*Cuddles {Context.User.Mention}~!*");
            }
            else
                await ReplyAsync($"*Cuddles {user.Mention}~!*");
        }

    }


}
