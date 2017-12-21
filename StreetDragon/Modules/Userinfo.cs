using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    class Userinfo : ModuleBase<SocketCommandContext>
    {

        [Command("userinfo"), Alias("Userinfo")]
        [Summary("Check your stats!")]
        public async Task showInfos()
        {
            

            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218)
            };

            builder.AddField(x =>
            {
                x.Name = Context.User.Username;
                x.Value = "";
                x.IsInline = false;
            });


            await ReplyAsync($"{Context.User.Mention}");


        }
    }
}
