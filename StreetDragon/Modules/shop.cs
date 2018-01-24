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
    public class Shop : ModuleBase<SocketCommandContext>
    {
        [Command("shop"), Alias("Shop")]
        [Summary("Look at our counter and see if you'd like anything!")]
        public async Task shop(string choice = null, int nb = 1)
        {
            if(choice!=null)
            choice = choice.ToLower();
            if (nb < 1) nb = 1;
            User u = Program.UL[Context.User.Id];
            Random r = new Random();
            int r1 = r.Next(0, 100);
            DateTime DT = DateTime.UtcNow;
            int price = 0;
            int XP = 0;
            Boolean a = false;
            Boolean b = true;

            if (choice == null)
            {
                var builder = new EmbedBuilder()
                {
                    Color = new Color(114, 137, 218),
                    Description = "Please write '!shop' followed by the name of the article you'd like!\nAnd remove spaces in your order's name."
                };

                builder.AddField(x =>
                {
                    x.Name = "Cookie";
                    x.Value = "The best sign of friendship.\n" +
                    "5 CC | +3 XP";
                    x.IsInline = true;
                });

                switch (DT.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        builder.AddField(x =>
                        {
                            x.Name = "Monday";
                            x.Value = "To help brave the start of a new week.\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;

                    case DayOfWeek.Tuesday:
                        builder.AddField(x =>
                        {
                            x.Name = "Tuesday";
                            x.Value = "Because Tuesdays can also be tiring.\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;

                    case DayOfWeek.Wednesday:
                        builder.AddField(x =>
                        {
                            x.Name = "Wednesday";
                            x.Value = "To celebrate the middle of the work week.\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;

                    case DayOfWeek.Thursday:
                        builder.AddField(x =>
                        {
                            x.Name = "Thursday";
                            x.Value = "Because Thursdays can also be tiring.\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;

                    case DayOfWeek.Friday:
                        builder.AddField(x =>
                        {
                            x.Name = "Friday";
                            x.Value = "Hang in there, last day before weekend!\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;

                    case DayOfWeek.Saturday:
                        builder.AddField(x =>
                        {
                            x.Name = "Saturday";
                            x.Value = "A good coffee for a good weekend.\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;

                    case DayOfWeek.Sunday:
                        builder.AddField(x =>
                        {
                            x.Name = "Sunday";
                            x.Value = "A fine coffee for a fine weekend.\n" +
                            "25 CC | +10 XP";
                            x.IsInline = true;
                        });
                        break;
                }

                if (u.lvl >= 5)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Espeonno";
                        x.Value = "Gives a psychic feel.\n" +
                        "50 CC | +30 XP";
                        x.IsInline = true;
                    });
                }
                
                if (u.lvl >= 8)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Dreelicious cake";
                        x.Value = "A blue cake with shells ontop.\n" +
                        "75 CC | +50 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 9)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "JotaCola";
                        x.Value = "Energetic drink!\n" +
                        "80 CC | +60 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 10)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "CapuSeanno";
                        x.Value = "Known to have an artistic taste.\n" +
                        "100 CC | +70 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 12)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Tiramiju";
                        x.Value = "An adorable tiramisu.\n" +
                        "125 CC | +100 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 13)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Risen Cake";
                        x.Value = "A cake that got a lot of yeast.\n" +
                        "150 CC | +120 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 14)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Macaronix";
                        x.Value = "Big black macarons!\n" +
                        "175 CC | +140 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 15)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Sponge Rocher";
                        x.Value = "A nice .\n" +
                        "200 CC | +150 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 16)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Dr.Keeper";
                        x.Value = "A drink that make you wanna fly.\n" +
                        "210 CC | +170 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 17)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Moenster";
                        x.Value = "A big muenster.\n" +
                        "225 CC | +185 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 18)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Modka";
                        x.Value = "A beverage you will remember.\n" +
                        "230 CC | +195 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 19)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Kuroissant";
                        x.Value = "A black croissant.\n" +
                        "250 CC | +200 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 20)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Azure Latti";
                        x.Value = "Tonic like a flying dragon\n" +
                        "300 CC | +330 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 21)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Marbriatto";
                        x.Value = "A cute salami.\n" +
                        "325 CC | +340 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 22)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Soulami";
                        x.Value = "A cute salami.\n" +
                        "350 CC | +345 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 23)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Mizotto";
                        x.Value = "A badass risotto.\n" +
                        "375 CC | +365 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 25)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Ocake";
                        x.Value = "A simple, but charming cake that somehow makes you git gud.\n" +
                        "400 CC | +380 XP";
                        x.IsInline = true;
                    });
                }

                if (u.lvl >= 30)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Mika Mokka";
                        x.Value = "The ultimate beverage.\n" +
                        "500 CC | +500 XP";
                        x.IsInline = true;
                    });
                }

                await ReplyAsync("", false, builder.Build());
            }
            else
            {
                switch (choice)
                {
                    case "cookie":
                        price = 5 * nb;
                        if (u.cutecoins >= price)
                        {
                            b = false;
                            a = true;
                        }
                        else
                        {
                            b = false;
                            if (r1 <= 5)
                            {
                                await ReplyAsync($"Aww you don't have enough...Well whatever, here's one for free~");
                                u.cookies += 1;
                                await ReplyAsync($"{Context.User.Mention} got 1 cookie!");
                            }
                            else
                            {
                                await ReplyAsync($"Aww you don't have enough...");
                            }
                        }
                        break;

                    case "monday":
                        if (DT.DayOfWeek == DayOfWeek.Monday)
                        {
                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "tuesday":
                        if (DT.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "wednesday":
                        if (DT.DayOfWeek == DayOfWeek.Wednesday)
                        {

                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "thursday":
                        if (DT.DayOfWeek == DayOfWeek.Thursday)
                        {
                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "friday":
                        if (DT.DayOfWeek == DayOfWeek.Friday)
                        {
                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "saturday":
                        if (DT.DayOfWeek == DayOfWeek.Saturday)
                        {
                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "sunday":
                        if (DT.DayOfWeek == DayOfWeek.Sunday)
                        {
                            price = 25 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 10 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                        {
                            await ReplyAsync($"Sorry, we don't serve this today.");
                            b = false;
                        }
                        break;

                    case "espeonno":
                        if (u.lvl >= 5)
                        {
                            price = 50 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 30 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "dreeliciouscake":
                        if (u.lvl >= 8)
                        {
                            price = 75 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 50 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "jotacola":
                        if (u.lvl >= 9)
                        {
                            price = 80 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 60 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "capuseanno":
                        if (u.lvl >= 10)
                        {
                            price = 100 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 70 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "tiramiju":
                        if (u.lvl >= 12)
                        {
                            price = 125 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 100 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "risencake":
                        if (u.lvl >= 13)
                        {
                            price = 150 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 120 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "macaronix":
                        if (u.lvl >= 14)
                        {
                            price = 175 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 140 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "spongerocher":
                        if (u.lvl >= 15)
                        {
                            price = 200 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 150 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "Dr.Keeper":
                        if (u.lvl >= 16)
                        {
                            price = 210 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 170 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "moenster":
                        if (u.lvl >= 17)
                        {
                            price = 225 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 185 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "modka":
                        if (u.lvl >= 18)
                        {
                            price = 230 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 195 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "kuroissant":
                        if (u.lvl >= 19)
                        {
                            price = 250 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 200 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else b = false;
                        break;

                    case "azurelatti":
                        if (u.lvl >= 20)
                        {
                            price = 300 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 330 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }else
                        b = false;
                        break;

                    case "marbriatto":
                        if (u.lvl >= 21)
                        {
                            price = 325 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 340 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                            b = false;
                        break;

                    case "soulami":
                        if (u.lvl >= 22)
                        {
                            price = 350 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 345 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                            b = false;
                        break;

                    case "mizotto":
                        if (u.lvl >= 23)
                        {
                            price = 375 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 365 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                            b = false;
                        break;

                    case "ocake":
                        if (u.lvl >= 25)
                        {
                            price = 400 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 380 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        }
                        else
                            b = false;
                        break;

                    case "mika mokka":
                        if (u.lvl>=30)
                        {
                            price = 500 * nb;
                            if (u.cutecoins >= price)
                            {
                                XP = 500 * nb;
                            }
                            else
                            {
                                await ReplyAsync($"You need at least {price} CuteCoins to get that!");
                                b = false;
                            }
                        } else b = false;
                        break;

                    default:
                        b = false;
                        a = false;
                        await ReplyAsync("Sorry, we don't have that in store!");
                        break;
                }
                
                if (b == true)
                {
                    if (nb > 1) choice += "s";
                    u.cutecoins -= price;
                    u.gainXP(XP);
                    await ReplyAsync($"{Context.User.Mention} got {nb} {choice} for {price} CuteCoins and gained {XP} XP!");
                }

                if (choice == "cookie" && a==true)
                {
                    if (nb > 1) choice += "s";
                    u.cookies += nb;
                    u.cutecoins -= price;
                    await ReplyAsync($"{Context.User.Mention} got {nb} {choice}!");
                }
            }
        }
    }
}
