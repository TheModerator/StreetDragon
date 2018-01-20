using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class ConfirmRequest : ModuleBase<SocketCommandContext>
    {
        [Command("cr"),Alias("crequest")]
        async Task cRequest(SocketGuildUser user = null)
        {
            if (Context.User.Id == Config.ID)
            {
                if(user!= null)
                {
                    User u = Program.UL[user.Id];
                    if (u.hasRequest == true)
                    {
                        u.hasRequest = false;
                        await ReplyAsync($"Request for {u.username} was completed!");
                    }
                    else
                    {
                        await ReplyAsync($"But {u.username} doesn't have any request on hold!");
                    }
                }
                else
                {
                    await ReplyAsync("You have to pass a user as a parameter.");
                }  
            }
            else
            {
                await ReplyAsync("You can't use this command!");
            }
        }
    }
}
