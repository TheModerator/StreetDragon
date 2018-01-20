using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StreetDragon
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public static Dictionary<ulong,User> UL = new Dictionary<ulong,User>();
        public static Dictionary<ulong, List<IMessage>> MessageCache = new Dictionary<ulong, List<IMessage>>();
        public static Dictionary<ulong, SocketGuild> Servers = new Dictionary<ulong, SocketGuild>();
        //  static public IEnumerable<IMessage> MessageCache;
        public Timer timer = new Timer();
        

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            await _client.SetGameAsync("with himself");
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

           

            //event subscription
            _client.Log += Log;
            _client.MessageReceived += Exp;
            _client.UserJoined += AnnounceUserJoined;
            _client.UserLeft += AnnounceUserLeft;
            
            

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, Config.DISCORD_PRIVATE_KEY);

            await _client.StartAsync();

            //  MessageCache =  await _client.GetChannel.GetMessagesAsync(100000, Discord.CacheMode.AllowDownload).Flatten();
            foreach (var server in _client.Guilds)
            {
                Servers.Add(server.Id, server);
            }
            Load();


            timer.Interval = 1000 * 60 * 60 * 24;
            timer.Elapsed += birthday;

            await Task.Delay(-1);
        }

        private async void birthday(object sender, ElapsedEventArgs e)
        {
            foreach(var user in UL)
            {
                if (DateTime.UtcNow.Day == user.Value.birthday.Day && DateTime.UtcNow.Month == user.Value.birthday.Month)
                {
                    SocketGuild guild = Servers[user.Value.guild];
                    var channel = guild.DefaultChannel;
                    await channel.SendMessageAsync($"It is {user.Value.username}'s birthday today!");
                }
            }
        }

        private async Task Exp(SocketMessage arg)
        {
            Save();
            SocketGuildUser user = (SocketGuildUser)arg.Author;
            Boolean hasFound = false;
            foreach (var uID in UL.Keys)
            {
                if (user.Id == uID)
                {
                    hasFound = true;
                    Console.WriteLine(" ");
                    Console.WriteLine("hasFound = true");
                }
            }

            if (hasFound == false)
            {
                Console.WriteLine("HasFound = false");
                User usr = new User(user.Id, user.Username);
                var channel = arg.Channel as SocketGuildChannel;
                usr.guild = channel.Guild.Id;
                UL.Add(user.Id, usr);
                
            }

            User u = UL[arg.Author.Id];
            DateTimeOffset a = arg.Timestamp;
            TimeSpan ts = a.Subtract(u.ts);

            if (ts.Minutes>=1)
            {
                u.gainCoins();
                u.ts = a;
            }
            

            if (u.xp >= u.xpmax)
            {
                u.lvl += 1;
                var channel = arg.Channel;
                //await channel.SendMessageAsync($"{arg.Author.Mention} leveled up to level " + u.lvl + "!");
            }
        }

        private async Task AnnounceUserLeft(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"{user.Mention} left the café.");
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome to Mika's Mokka Lounge, {user.Mention}! This is the café of cuteness and art. We hope you'll enjoy your stay here!" );
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message == null || message.Author.IsBot) return;

            int argPos = 0;

            if(message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }

        public void Save()
        {
            string path = Config.SAVE;
            string path2 = Config.SAVE2;

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                

                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var user in UL)
                    {
                        sw.WriteLine(user.Key);
                        sw.WriteLine(user.Value.username);
                        sw.WriteLine(user.Value.lvl);
                        sw.WriteLine(user.Value.xp);
                        sw.WriteLine(user.Value.xpmax);
                        sw.WriteLine(user.Value.cutecoins);
                        sw.WriteLine(user.Value.cookies);
                        sw.WriteLine(user.Value.hasRequest);
                        sw.WriteLine(user.Value.guild);
                        sw.WriteLine(user.Value.birthday);
                        sw.WriteLine("");
                    }
                }

                /*if (File.Exists(path2))
                {
                    File.Delete(path2);
                }

                using (StreamWriter sw2 = new StreamWriter(path2))
                {
                    foreach (var user in UL)
                    {
                        sw2.WriteLine(user.Key);
                        sw2.WriteLine(user.Value.username);
                        sw2.WriteLine(user.Value.lvl);
                        sw2.WriteLine(user.Value.xp);
                        sw2.WriteLine(user.Value.xpmax);
                        sw2.WriteLine(user.Value.cutecoins);
                        sw2.WriteLine(user.Value.cookies);
                        sw2.WriteLine(user.Value.hasRequest);
                        sw.WriteLine(user.Value.guild);
                        sw.WriteLine(user.Value.birthday);
                        sw2.WriteLine("");
                    }
                }*/
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public void Load()
        {
            string path = Config.SAVE;
            string path2 = Config.SAVE2;
            Boolean success = true;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        ulong id = Convert.ToUInt64(sr.ReadLine());
                        string username = sr.ReadLine();
                        int lvl = Convert.ToInt32(sr.ReadLine());
                        int xp = Convert.ToInt32(sr.ReadLine());
                        int xpmax = Convert.ToInt32(sr.ReadLine());
                        int cutecoins = Convert.ToInt32(sr.ReadLine());
                        int cookies = Convert.ToInt32(sr.ReadLine());
                        Boolean request = Convert.ToBoolean(sr.ReadLine());
                        ulong server = Convert.ToUInt64(sr.ReadLine());
                        DateTime birthday = Convert.ToDateTime(sr.ReadLine());
                        string useless = sr.ReadLine();

                        User u = new User(id, username);
                        u.lvl = lvl;
                        u.xp = xp;
                        u.xpmax = xpmax;
                        u.cutecoins = cutecoins;
                        u.cookies = cookies;
                        u.hasRequest = request;
                        u.guild = server;
                        u.birthday = birthday;
                        
                        UL.Add(id, u);

                        if (UL.Count == 0) success = false;
                    }
                }
                 /*if (success == false)
                 {
                     using (StreamReader sr = new StreamReader(path2))
                     {
                         while (!sr.EndOfStream)
                         {
                            ulong id = Convert.ToUInt64(sr.ReadLine());
                            string username = sr.ReadLine();
                            int lvl = Convert.ToInt32(sr.ReadLine());
                            int xp = Convert.ToInt32(sr.ReadLine());
                            int xpmax = Convert.ToInt32(sr.ReadLine());
                            int cutecoins = Convert.ToInt32(sr.ReadLine());
                            int cookies = Convert.ToInt32(sr.ReadLine());
                            Boolean request = Convert.ToBoolean(sr.ReadLine());
                            DateTime birthday = Convert.ToDateTime(sr.ReadLine());
                            string useless = sr.ReadLine();

                            User u = new User(id, username);
                            u.lvl = lvl;
                            u.xp = xp;
                            u.xpmax = xpmax;
                            u.cutecoins = cutecoins;
                            u.cookies = cookies;
                            u.hasRequest = request;
                            u.birthday = birthday;

                            UL.Add(id, u);
                         }
                     }
                 }*/
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }



    }



}
