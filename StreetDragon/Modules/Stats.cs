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
    public class Stats : ModuleBase<SocketCommandContext>
    {
        private User u;
        [Command("Stats"), Alias("stats")]
        [Summary("View your customer status!")]
        public async Task stats(SocketGuildUser user = null)
        {
            
            if (user == null)
            {
                u = Program.UL[Context.User.Id];
            }
            else
            {
                u = Program.UL[user.Id];
            }

            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
            };

            builder.AddField(x =>
            {
                x.Name = "Name :";
                x.Value = u.username;
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Lv :";
                x.Value = u.lvl;
                x.IsInline = true;
            });

            builder.AddField(x =>
            {
                x.Name = "XP :";
                x.Value = u.xp+"/"+u.xpmax;
                x.IsInline = true;
            });

            builder.AddField(x =>
            {
                x.Name = "CuteCoins :";
                x.Value = u.cutecoins;
                x.IsInline = true;
            });

            builder.AddField(x =>
            {
                x.Name = "Cookies :";
                x.Value = u.cookies;
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

    }
}
