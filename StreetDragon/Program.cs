using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private List<User> UL = new List<User>();
        public static Dictionary<ulong, List<IMessage>> MessageCache = new Dictionary<ulong, List<IMessage>>();
     //  static public IEnumerable<IMessage> MessageCache;

        internal List<User> UL1 {
            get { return UL; }
            set { UL = value; }
            }

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            await _client.SetGameAsync("with himself");
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "MzkwOTcwNzQ3OTM5NzE3MTMx.DRSB1A.vYRYA1EuaCH4b2U3la4R7T6a9k4";

            //event subscription
            _client.Log += Log;
            _client.MessageReceived += Exp;
            _client.UserJoined += AnnounceUserJoined;
            _client.UserLeft += AnnounceUserLeft;
            
            

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

         //  MessageCache =  await _client.GetChannel.GetMessagesAsync(100000, Discord.CacheMode.AllowDownload).Flatten();

            await Task.Delay(-1);
        }

        private async Task Exp(SocketMessage arg)
        {
            Boolean hasFound = false;
            foreach(User user in UL1)
            {
                if(user.userID == arg.Author.Id)
                {
                    hasFound = true;
                    user.gainCoins();
                }
            }

            if(hasFound == false)
            {
                User u = new User(arg.Author.Id, arg.Author.Username);
                UL1.Add(u);
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
    }



}
