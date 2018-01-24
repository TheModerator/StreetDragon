using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Birthday : ModuleBase<SocketCommandContext>
    {
        [Command("birthday"),Alias("bd")]
        [Summary("Set your birthday!")]
        public async Task birthday(string birthd = null)
        {
            if(birthd == null)
            {
                await ReplyAsync("To set your birthday, type the following: '!birthday dd/mm/yyyy'");
            } else
            {
                try
                {
                    User u = Program.UL[Context.User.Id];
                    DateTime bd = new DateTime(Convert.ToInt32(birthd.Substring(6, 4)), Convert.ToInt32(birthd.Substring(3, 2)), Convert.ToInt32(birthd.Substring(0, 2)));
                    bd.ToString("dd/mm/yyyy");
                    u.birthday = bd;
                    await ReplyAsync("Your birthday has been registered!");
                    u.hasBirthday = true;
                }
                catch (Exception e)
                {
                    await ReplyAsync("Wrong data type, please enter your date as follows : dd/mm/yyyy");
                    Console.WriteLine(e.ToString());
                }
            }
           
            
        }
    }
}
