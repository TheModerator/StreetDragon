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
        public async Task Immitate(SocketGuildUser user = null, SocketChannel selectedChannel = null, String starter = "")
        {
            try { 
            var model = new StringMarkov(2);
            //IEnumerable<IMessage> messages =  await Context.Channel.GetMessagesAsync(100000).Flatten();

            // --------------------------- UPDATE CACHE WITH ALL CHANNELS ---------------------------
            // Yeah I know we might not NEED all channels, but hey, at some point we will

            IAsyncEnumerable<IReadOnlyCollection<IMessage>> messages;
            Task[] taskList = new Task[Context.Guild.Channels.Count];

            Boolean[] dlDone = new Boolean[Context.Guild.Channels.Count ];
            int didx = 0;
            while(didx < Context.Guild.Channels.Count - 1)
            {
                dlDone[didx] = false;
                didx += 1;
            }
            List<IMessage>[] tempCache = new List<IMessage>[Context.Guild.Channels.Count ];
            int idx = 0;
            foreach (SocketGuildChannel CGC in Context.Guild.Channels) {

                ISocketMessageChannel Channel = CGC as ISocketMessageChannel;
                tempCache[idx] = new List<IMessage>();
                if (Channel == null)
                {
                    dlDone[idx] = true;
                    idx += 1;
                    continue;
                }

                Console.WriteLine("Downloading messages from " + Channel.Name);
                if (!Program.MessageCache.ContainsKey(Channel.Id)){
                    messages = Channel.GetMessagesAsync(50000, CacheMode.AllowDownload);
                    Program.MessageCache.Add(Channel.Id, new List<IMessage>());
                    Console.WriteLine("[" + Channel.Name + "] Fresh download");
                }
                else {
                    messages = Channel.GetMessagesAsync(Program.MessageCache[Channel.Id].Last(), Direction.After, 10000);
                    Console.WriteLine("[" + Channel.Name + "] Cache update");
                }
                Console.WriteLine("[" + Channel.Name + "] - Assigned IDX " + idx);
                
                int chIdx = idx;
                var evt = messages.ForEachAsync(async msglist =>
                {
                    Console.WriteLine("[" + Channel.Name + "] - " + chIdx + " Download Received");
                    foreach (var msg in msglist)
                    {
                        if (msg.Content.StartsWith("!")) continue;
                        if (msg.Attachments.Count > 0) continue;
                        tempCache[chIdx].Add(msg);
                       //  Console.WriteLine(tempCache[chIdx].Count + ". " + msg);
                    }
                });



                evt.ContinueWith(async atsk =>
                {
                    Console.WriteLine("[" + Channel.Name + "] - " + chIdx + " Download Finished");
                    tempCache[chIdx].Reverse();
                    Program.MessageCache[Channel.Id].AddRange(tempCache[chIdx]);
                    tempCache[chIdx].Clear();
                    dlDone[chIdx] = true;
                    bool allDone = true;
                    foreach (bool isDone in dlDone)
                    {
                       
                        if(!isDone)
                        {
                            allDone = false;
                        }
                    }
                    if (allDone)
                    {
                        Console.WriteLine("[" + Channel.Name + "] - " + chIdx + " All downloads done");
                        await selectMessages(user, selectedChannel, starter);
                    }
                });
                taskList[chIdx] = evt;
                idx += 1;
            }

        }catch (Exception ex)
            {
                await ReplyAsync("``` \r\n FUCKED UP - " + ex.Message + "\r\n Stack: \r\n" + ex.StackTrace + "```");
    }

}

        internal async Task selectMessages(SocketGuildUser user, SocketChannel Channel, String starter)
        {
            try { 
            List<String> messageContents = new List<String>();
            String nickname;
            if (user == null)
            {

                    IMessage[] pool;
                    if (Channel == null)
                    {
                        List<IMessage> lst = new List<IMessage>();

                        foreach (var chnl in Program.MessageCache)
                        {
                            lst.AddRange(chnl.Value.ToArray());
                        }


                        pool = lst.ToArray();
                    }
                    else
                    {
                        pool = Program.MessageCache[Channel.Id].ToArray();
                    }


                    nickname = "The Café";
                foreach (var msg in pool)
                {
                    messageContents.Add(msg.Content.Trim('_').Trim('-').Trim('~').Trim('*').Trim(' '));
                }
            }
            else
            {
                nickname = user.Username;

                    IMessage[] pool;
                    if (Channel == null)
                    {
                        List<IMessage> lst = new List<IMessage>();

                        foreach(var chnl in Program.MessageCache)
                        {
                            lst.AddRange(chnl.Value.Where(msg => msg.Author.Id == user.Id).ToArray());
                        }


                        pool =lst.ToArray();
                    }else
                    {
                        pool = Program.MessageCache[Channel.Id].Where(msg => msg.Author.Id == user.Id).ToArray();
                    }
                foreach (var msg in pool.Where(msg => msg.Author.Id == user.Id))
                {
                    messageContents.Add(msg.Content.Trim('_').Trim('-').Trim('~').Trim('*').Trim(' '));
                }
            }



            RespondWithAnswer(messageContents.ToArray(), starter, nickname);

        }catch (Exception ex)
            {
                await ReplyAsync("``` \r\n FUCKED UP - " + ex.Message + "\r\n Stack: \r\n" + ex.StackTrace + "```");
            }
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
