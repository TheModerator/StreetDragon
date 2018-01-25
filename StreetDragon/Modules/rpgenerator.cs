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
    public class rp : ModuleBase<SocketCommandContext>
    {

        String[] sentences = new String[]{"a {personified_adjective} [species] [verb]s an {itemised_adjective} [obj] that lets [pronoun] [action_verb]",
                            "a {personified_adjective} [species] , a {personified_adjective} [species] , and a [personified_adjective] [species] [verb_transport] into [place]",
         "[patron], the {personified_adjective} [species], has a [itemised_adjective] [obj] that [patron] wants to [verb] "};

        String[] action_verb = new String[] { "sing" , "dance", "drink blood", "fight", "jump", "invade poland", "do open heart surgery", "find inner peace", "play overwatch", "play crysis on full" };
        String[] patron = new String[]{ "Leki", "Dree", "Pierre", "Cele", "Azu", "Mika", "Wern", "Nyra", "Our Dark Lord Saten Himself", "Ronix", "Monster", "Dave", "Jota", "Soul", "Mizzium", "Sean", "Soul", "Ender", "the one which is Ender but is pretending he's a guy called Kuroro" };
        String[] verb_transport = new String[] { "walk", "swim", "fly", "bounce", "crash", "drive", "walk", "fall" };
        String[] place = new String[] { "a bar", "a pokemon center", "your house", "a hospital", "a school", "Poland", "France", "the white house", "yo momma", "a jungle", "the lost city of atlantis"};
    String[] species = new String[] { "Latios", "Latios", "Dewott", "Dewott", "Avali", "Mew", "Blaziken", "Garchomp", "Dragon", "Mage", "Cat", "Dog", "Wolf", "Fox" };
    String[] verb = new String[] { "find", "lose", "eat", "punch", "kick", "slap", "call", "need", "want", "have" };
    String[] personified_adjective = new String[] { "vegetarian", "cute", "suspicious", "deadly", "friendly", "ironic", "magical", "enchanted", "talented", "small", "tall", "big", "fat", "thin", "exotic", "sly", "uncertain", "hostile" };
    String[] itemised_adjective = new String[] {  "cute", "suspicious", "deadly", "friendly", "small", "tall", "big", "fat", "thin", "exotic", "magical", "enchanted"};
    String[] pronoun = new String[] { "him", "her", "it", "them" };
        String[] obj = new String[] { "orb", "sword", "object", "pokeball", "mobile phone", "gun", "diamond", "camera" };

        String[] sizes = new String[] { "tiny", "small", "normal sized", "big", "huge", "massive", "enourmous", "gigantic", "monsterous" };
        String[] widths = new String[] { "thin", "thick" };

        String[] number = new String[] { "two", "three", "four", "five", "six", "ten", "eleven", "sixteen", "six hundred and twenty one", "twelve", "a lot of" };


        private String getFromKeyword(String input)
        {
            string Output = "";
            switch (input.ToLower())
            {
                case "action_verb":
                    Output = randomFromSet(action_verb);
                    break;
                case "personified_adjective":
                    Output = randomFromSet(personified_adjective);
                    break;
                case "patron":
                    Output = randomFromSet(patron);
                    break;
                case "verb_transport":
                    Output = randomFromSet(verb_transport);
                    break;
                case "place":
                    Output = randomFromSet(place);
                    break;
                case "species":
                    Output = randomFromSet(species);
                    break;
                case "verb":
                    Output = randomFromSet(verb);
                    break;
                case "itemised_adjective":
                    Output = randomFromSet(itemised_adjective);
                    break;
                case "pronoun":
                    Output = randomFromSet(pronoun);
                    break;
                case "obj":
                    Output = randomFromSet(obj);
                    break;
                case "sizes":
                    Output = randomFromSet(sizes);
                    break;
                case "widths":
                    Output = randomFromSet(widths);
                    break;

            }
            return Output;
        }

        [Command("rp")]       
        public async Task GenerateTopic()
        {
            double choice = Program.globalRandom.NextDouble() * (sentences.Length - 1);
            String sentence = sentences[Convert.ToInt16(choice)];

            int i = 0;
            try { 




            while(i < sentence.Length && i >= 0)
            {
                    i = sentence.IndexOfAny(new Char[] { '[', '{' },i);
                    if (i < 0) break;
                    int len = (sentence.IndexOfAny(new Char[] { ']', '}' },i)+1) - i;

                    string wrd = sentence.Substring(i, len);
                    string newWrd = executeWordReplace(wrd);
                    sentence = sentence.Remove(i, len);
                    sentence = sentence.Insert(i, newWrd);
                    i = i + newWrd.Length;
            }
           
        }catch (Exception ex) {
                await ReplyAsync(fixVouelRule("Nope"));
            }
            await ReplyAsync(fixVouelRule(sentence));
        }

        private String executeWordReplace(String input)
        {

            String output = input;
            try { 
            if (output.StartsWith("[") && output.EndsWith("]"))
            {
                output = getFromKeyword(input.TrimStart('[').TrimEnd(']'));
            }else if (output.StartsWith("{") && output.EndsWith("}"))
            {
                double choice = Program.globalRandom.NextDouble() * 2.0;
                if(choice > 0.5)
                {
                    output = getFromKeyword(input.TrimStart('{').TrimEnd('}'));
                }
                else
                {
                    output = "";
                }
            }

             }catch (Exception ex) {
                return "nope";
            }
            return output;
        }

      

        char[] vouls = new Char[] { 'a', 'e', 'i', 'o', 'u' };

        private String fixVouelRule(String input)
        {
            try
            {
                string Output = "";
            int i = 0;
            String[] splits = input.Split(' ');
            while (i < splits.Length) 
            {
                   
                        if (splits[i] == "a" || splits[i] == "an")
                        {
                        if (splits[i + 1].Length < 1)
                        {

                            Output += splits[i];
                        }
                        else
                        {
                            char firstLetterOfNextWord = splits[i + 1][0];
                            if (!vouls.Contains(firstLetterOfNextWord))
                            {
                                Output += "a";
                            }
                            else
                            {
                                Output += "an";
                            }
                        }
                        }
                        else
                        {
                            Output += splits[i];
                        }
                    
                
                Output += " ";

                i +=1;
                }
                return Output;
            } 
            catch (Exception ex)
            {
                return "";
            }
           
          }
     


        private String randomFromSet(String[] inputs)
        {
            try
            {
                double choice = (Program.globalRandom.NextDouble() * (inputs.Length - 1));
                return inputs[Convert.ToInt16(choice)];
            }catch (Exception ex) { 
                return "";
            }
        }

    }

}
