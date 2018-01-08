using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MarkovSharp.TokenisationStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class IAm : ModuleBase<SocketCommandContext>
    {
        [Command("im"), Alias("IAm")]
        [Summary("Does fancy science magic to turn into the specified customer!")]
        public async Task Immitate(SocketGuildUser user = null, String starter = "")
        {
           
            var model = new StringMarkov(2);
            //IEnumerable<IMessage> messages =  await Context.Channel.GetMessagesAsync(100000).Flatten();
            IAsyncEnumerable<IReadOnlyCollection<IMessage>> messages;
            if (!Program.MessageCache.ContainsKey(Context.Channel.Id))
            {
                messages = Context.Channel.GetMessagesAsync(50000, CacheMode.AllowDownload);
                Program.MessageCache.Add(Context.Channel.Id, new List<IMessage>());
            }
            else{
                messages = Context.Channel.GetMessagesAsync(Program.MessageCache[Context.Channel.Id].Last(),Direction.After,10000);
            }


            // Donwloaded going BACK in time


            List<IMessage> tempCache = new List<IMessage>();

            var evt = messages.ForEachAsync(async msglist =>
            {
                foreach (var msg in msglist)
                {
                    if (msg.Content.StartsWith("!im")) continue;
                    if (msg.Attachments.Count > 0) continue;
                    tempCache.Add(msg);
                    Console.WriteLine(tempCache.Count + ". " + msg);
                }
            });

   


            evt.ContinueWith(async actn =>
            {
                tempCache.Reverse();


                Program.MessageCache[Context.Channel.Id].AddRange(tempCache);
                List<String> messageContents = new List<String>();
                String nickname;
                if (user == null)
                {
                    IMessage[] pool = Program.MessageCache[Context.Channel.Id].ToArray();

                    nickname = "The Café";
                    foreach (var msg in pool)
                    {
                        messageContents.Add(msg.Content.Replace("_", "").Replace("*", "").Replace("-", "").Trim(' '));
                    }
                }
                else
                {
                    nickname = user.Nickname;
                    IMessage[] pool = Program.MessageCache[Context.Channel.Id].Where(msg => msg.Author.Id == user.Id).ToArray();
                    
                    foreach (var msg in pool.Where(msg => msg.Author.Id == user.Id))
                    {
                        messageContents.Add(msg.Content.Replace("_", "").Replace("*", "").Replace("-", "").Trim(' '));
                    }
                }
              
               
                RespondWithAnswer(messageContents.ToArray(), starter, nickname);


            });



            


        }


        internal async Task RespondWithAnswer(String[] dataset, String start = "", String user = "")
        {
            StringMarkov model = new StringMarkov(2);
            model.Learn(dataset);
            Random rnd1 = new Random();
            String sentence = "";
            int startSpawn = 0;
            Boolean acceptable = false;
            int totalAttempts = 0;
            while (sentence.Split('c').Length < 4 && !acceptable)
            {
                acceptable = false;
                sentence = start;
                int i = 0;
                startSpawn += 1;
                while (i < 30)
                {
                    try
                    {
                        String[] newWord = model.GetMatches(sentence.Trim()).ToArray();
                        Double len = Convert.ToDouble(newWord.Length - 1);
                        Double rnd = Convert.ToDouble(rnd1.NextDouble());
                        Double decision = Math.Floor(rnd * len);
                        String selectedWord = newWord[Convert.ToInt16(decision)];
                        if (selectedWord == null) break;
                        sentence = sentence + " " + selectedWord;
                        
                        i += 1;
                    }
                    catch (Exception exc)
                    {

                        i = 31;
                        totalAttempts += 1;
                        if(totalAttempts > 1000)
                        {
                            await ReplyAsync("Sorry, no can do - " + user + " doesn't talk much");
                            return;
                        }
                    }
                    totalAttempts += 1;
                    if (totalAttempts > 1000)
                    {
                        await ReplyAsync("Sorry, no can do - " + user + " doesn't talk much");
                        return;
                    }
                }
                totalAttempts += 1;
                if (totalAttempts > 1000)
                {
                    await ReplyAsync("Sorry, no can do - " + user + " doesn't talk much");
                    return;
                }
                if (!dataset.Contains(sentence.Trim(' '))) acceptable = true;
            }
            await ReplyAsync("```" + sentence + "``` - " + user);
        }

    }
}
