﻿using System;
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
        public int xpmax = 5;
        public int cutecoins = 0;
        public DateTimeOffset ts;
        public int cookies = 0;
        public Boolean hasRequest = false;
        public DateTime birthday = DateTime.MinValue;
        public ulong guild = 0;
        public Boolean hasBirthday = false;

        public User(ulong userID, string username)
        {
            this.userID = userID;
            this.username = username;
        }

        public void gainCoins()
        {
            Random r = new Random();
            Random r2 = new Random();
            int Rd = r.Next(1+lvl,1+lvl*2);
            int Rd2 = r2.Next(1,2+(lvl*2));
            int temp = Rd-(xpmax-xp);

            xp += Rd;
            cutecoins += Rd2;
            Console.WriteLine(userID +" "+ username + " " + xp + "/" + xpmax + " " + cutecoins+"CC");

            if (xp >= xpmax)
            {
                lvl += 1;
                if (lvl == 1)
                    xpmax = 6;
                if (lvl > 1)
                    xpmax = lvl * lvl * lvl;
                xp = 0;
                xp += temp;
            }
        }

        public void gainXP(int nb)
        {
            int temp = nb - (xpmax - xp);
            xp += nb;
            Console.WriteLine(userID + " " + username + " " + xp + "/" + xpmax + " " + cutecoins + "CC");

            if (xp >= xpmax)
            {
                lvl += 1;
                if (lvl == 1)
                    xpmax = 6;
                if (lvl > 1)
                    xpmax = lvl * lvl * lvl;
                xp = 0;
                xp += temp;
            }
        }

    }
}
