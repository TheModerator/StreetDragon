using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace StreetDragon
{
    class User
    {
        public ulong userID;
        public string username;
        public int xp = 0;
        public int lvl = 0;
        public int expmax = 100;
        public int cutecoins = 0;

        public User(ulong userID, string username)
        {
            this.userID = userID;
            this.username = username;
        }

       

        public void gainCoins()
        {
            this.expmax = 100 * lvl;
            this.cutecoins += 15;
        }

    }
}
