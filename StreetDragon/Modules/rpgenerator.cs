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


        [Command("rp"), Alias("Rp")]
        public async Task generateRP()
        {
            try
            {
               
                await ReplyAsync("`" + GenerateTopic() + "`");
            }
            catch (Exception ex)
            {
                await ReplyAsync("``` \r\n FUCKED UP - " + ex.Message + "\r\n Stack: \r\n" + ex.StackTrace + "```");
            }

        }




        String[] sentences = new String[]{"a {personified_adjective} [species] [verb]s an {itemised_adjective} [obj] that lets [pronoun] [action_verb]",
                            "a {personified_adjective} [species] , a {personified_adjective} [species] , and a [personified_adjective] [species] [verb_transport] into [place]",
         "[patron], the {personified_adjective} [species], has a [itemised_adjective] [obj] that [patron] wants to [verb] "};

        String[] action_verb = new String[] { "sing", "dance", "drink blood", "fight", "jump", "invade poland", "do open heart surgery", "find inner peace", "play overwatch", "play crysis on full" };
        String[] patron = new String[] { "Leki", "Dree", "Pierre", "Cele", "Azu", "Mika", "Wern", "Nyra", "Deamon From The Deepest Depths Of Hell", "Ronix", "Monster", "Dave", "Jota", "Soul", "Mizzium", "Sean", "Soul", "Ender", "the one which is Ender but is pretending he's a guy called Kuroro" };
        String[] verb_transport = new String[] { "walk", "swim", "fly", "bounce", "crash", "drive", "walk", "fall" };
        String[] place = new String[] { "a bar", "a pokemon center", "your house", "a hospital", "a school", "Poland", "France", "the white house", "yo momma", "a jungle", "the lost city of atlantis" };
        String[] species = new String[] { "Latios", "Latios", "Dewott", "Dewott", "Avali", "Mew", "Blaziken", "Garchomp", "Dragon", "Mage", "Cat", "Dog", "Wolf", "Fox", "Yveltal", "wild slav" };
        String[] verb = new String[] { "find", "lose", "eat", "punch", "kick", "slap", "call", "need", "want", "have" };
        String[] personified_adjective = new String[] { "vegetarian", "cute", "suspicious", "deadly", "friendly", "ironic", "magical", "enchanted", "talented", "small", "tall", "big", "fat", "thin", "exotic", "sly", "uncertain", "hostile" };
        String[] itemised_adjective = new String[] { "cute", "suspicious", "deadly", "friendly", "small", "tall", "big", "fat", "thin", "exotic", "magical", "enchanted" };
        String[] pronoun = new String[] { "him", "her", "it", "them" };
        String[] obj = new String[] { "orb", "sword", "object", "pokeball", "mobile phone", "gun", "diamond", "camera" };

        String[] size = new String[] { "tiny", "small", "normal sized", "big", "huge", "massive", "enourmous", "gigantic", "monsterous" };
        String[] width = new String[] { "girthy", "meaty", "thick" };
        String[] large_modifier = new String[] { "really", "very", "excessively", "incredibly", "desperately" };

        String[] number = new String[] { "two", "three", "four", "five", "six", "ten", "eleven", "sixteen", "six hundred and twenty one", "twelve", "a lot of" };


        String[] cock_type = new String[] { "pointed", "knotted", "spiney", "prehensile", "horse", "canine", "hyper" };
        String[] cock = new String[] { "shaft", "prick", "penis", "dick", "cock", "meatpole", "spicy boii" };

        String[] boob_type = new String[] { "soft", "perky", "lushious", "nice", "cute", "plump" };
        String[] boobs = new String[] { "boobs", "tits", "breasts", "love pillows", "milkbags" };

        String[] nsfw_adjective = new String[] { "pent-up", "slutty", "lewd", "dominating", "submissive", "sexy", "hot", "buff", "cute", "lusty" };
        String[] nsfw_emotion = new String[] { "pent-up", "horny", "desperate", "timid", "brave", "curious", "lusty" };
        String[] fluid = new String[] { "air", "cum", "helium", "water", "love nectar" };
        String[] cock_action = new String[] { "throb", "cum", "pulse", "grow", "swell" };

        String[] furry_emote = new String[] { "owo", "OwO", "^_^", "^^", "O.o", "ovo", "xD" };


        String[] nsfw_sentences = new String[]{ "a [nsfw_adjective] [species] [wants|needs] to be [fucked|sucked|mounted|raped|taken] by a {submissive|dominating} [species] with a [size] {width} [cock_type] [cock].",
                                                "whilst exploring [place], a {large_modifier} [nsfw_adjective] [species] encounters [number] [species]s that are more than willing to use their [cock_type] [cock]s to help out.",
                                                "just how [size] can a [species]'s [cock] get? [patron] finds out.",
                                                "[notices|sees|wubbs|tickles|nuzzles] ur [bulge|fur|tail|chest|paws|feets|] *[furry_emote] [pounces|gasps|awooo|wuts this?]*",
                                                "a [itemised_adjective] [obj] inflates a {personified_adjective} [species] full of [fluid], and now his {size} [cock] is [cock_action]ing.",
                                                "a [species] with a [size] [cock] wakes up to find his {cock_type} [cock] is now a [size], [width] [cock]. He decides to try it out on a [nsfw_adjective] [species]",
                                                "[cock_type] [cock]s are well known to be made to [cock_action] just from being [touched|breathed on|licked|exposed|noticed] by a [species]... ",
                                                "a [nsfw_adjective] [species] with {size}, {boob_type} [boobs] {gently|roughly|forcefully} [sucks|fucks|rides|drains|inflates] a [size]-[cock]'d [species]",
                                                "a [species] with [size] [loads|cumshots|balls] [shoots|spurts|splurges|lets off|pumps out] [number] [size] {thick|goey} loads [[inside|on|into|, overflowing] a [nsfw_adjective] [species]'s [ass|pussy|throat|mouth]]",
                                                "a [size]-[tittied|breasted|chested] {and [nsfw_emotion]} [species] [rides|is taken from behind by] a {nsfw_emotion} [species] with a {large_modifier} [size] [cock].",
                                                "[impressed|amazed|awestruck|astounded|ovewhelemed|astonished|gawping|open-mouthed|shocked] at how [[size]|[width]] the [species]'s {cock_type} [cock] was, a [nsfw_adjective] [species] [tries|decides|begs|stops doing their taxes] to [deepthroat|suck|fuck] the entire thing.",
                                                "a [[nsfw_adjective]|[nsfw_emotion]] [species]'s {{width}|{size}} [cock] [can't|won't] stop [cock_action]ing, leaving him {(and his {[width],} [aching|engourged|twitching] [cock])} [exhausted|begging for release|[[horny|desperate] for a {nsfw_adjective} [species] [with|boasting|weilding] [a [[cock_type]|[size]|[width]] [cock]|two {boob_type} [boobs]] to [bury himself inside|fill|inflate|fuck {senseless|silly}]]]",
        };





        private String getFromKeyword(String input)
        {
            while (input.Contains("[") || input.Contains("{"))
            {
                input = handleBracket(input);
            }
            string Output = "";
            switch (input.ToLower())
            {
                case "nsfw_adjective":
                    Output = randomFromSet(nsfw_adjective);
                    break;
                case "boob_type":
                    Output = randomFromSet(boob_type);
                    break;
                case "boobs":
                    Output = randomFromSet(boobs);
                    break;
                case "nsfw_emotion":
                    Output = randomFromSet(nsfw_emotion);
                    break;
                case "fluid":
                    Output = randomFromSet(fluid);
                    break;
                case "cock_action":
                    Output = randomFromSet(cock_action);
                    break;
                case "large_modifier":
                    Output = randomFromSet(large_modifier);
                    break;
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
                case "size":
                    Output = randomFromSet(size);
                    break;
                case "width":
                    Output = randomFromSet(width);
                    break;
                case "number":
                    Output = randomFromSet(number);
                    break;
                case "cock_type":
                    Output = randomFromSet(cock_type);
                    break;
                case "cock":
                    Output = randomFromSet(cock);
                    break;

                default:
                    String[] set = input.Split('|');
                    Output = randomFromSet(set);
                    break;

            }
            return Output;
        }


        public String GenerateTopic()
        {

           
            String sentence;
            if (Context.Channel.IsNsfw)
            {
                double choice = Program.globalRandom.NextDouble() * (nsfw_sentences.Length - 1);
                sentence = nsfw_sentences[Convert.ToInt16(choice)];
            }else
            {
                double choice = Program.globalRandom.NextDouble() * (sentences.Length - 1);
                sentence = sentences[Convert.ToInt16(choice)];
            }


            try
            {


                sentence = handleBracket(sentence);



            }
            catch (Exception ex)
            {
                return "";
            }
            return fixFindReplaceRule(fixVouelRule(sentence));
        }


        private String handleBracket(String segment)
        {
            int i = 0;

            while (i < segment.Length && i >= 0)
            {
                i = segment.IndexOfAny(new Char[] { '[', '{' }, i);
                if (i < 0) break;

                int len;
                int j = i;
                int brkts = 0;
                List<int> splits = new List<int>();
                splits.Add(0);
                foreach (char letter in segment.Substring(i))
                {
                    if (letter == '{' || letter == '[') brkts += 1;
                    if (letter == '}' || letter == ']') brkts -= 1;
                    if (brkts == 1 && letter == '|') splits.Add(j - i);
                    j += 1;
                    if (brkts == 0)
                    {
                        splits.Add((j - i) - 1);
                        break;
                    }
                }


                len = j - i;
                string wrd = segment.Substring(i, len);

                if (splits.Count > 2)
                {
                    string selectedSegment = wrd;
                    char openBracket = selectedSegment[0];
                    char closeBracket = selectedSegment[selectedSegment.Length - 1];
                    selectedSegment = selectedSegment.Remove(0, 1).Remove(selectedSegment.Length - 2, 1);

                    string[] possibilities = new string[splits.Count - 1];

                    int si = 1;
                    while (si < splits.Count)
                    {
                        possibilities[si - 1] = selectedSegment.Substring(splits[si - 1], (splits[si] - splits[si - 1]) - 1);
                        si += 1;
                    }
                    selectedSegment = randomFromSet(possibilities);
                    wrd = openBracket + selectedSegment + closeBracket;
                }


                string newWrd = "";

                int brktCount = 0;
                foreach (char letter in wrd)
                {
                    if (letter == '{' || letter == '[') brktCount += 1;
                }
                if (brktCount > 1)
                {
                    if (wrd[0] == '{' && Program.globalRandom.NextDouble() < 0.7)
                    {
                        newWrd = "";
                    }
                    else
                    {
                        newWrd = handleBracket(wrd.Remove(0, 1).Remove(wrd.Length - 2, 1));
                    }
                }
                else
                {
                    newWrd = executeWordReplace(wrd);
                }


                if (newWrd == "")
                {
                    if (!(i + len >= segment.Length))
                    {
                        len += 1;
                    }

                }
                segment = segment.Remove(i, len);
                segment = segment.Insert(i, newWrd);
                i = i + newWrd.Length;
            }
            return segment;
        }
        private String executeWordReplace(String input)
        {

            String output = input;

            if (output.StartsWith("[") && output.EndsWith("]"))
            {
                output = getFromKeyword(input.Substring(1, input.Length - 2));
            }
            else if (output.StartsWith("{") && output.EndsWith("}"))
            {
                double choice = Program.globalRandom.NextDouble();
                if (choice < 0.7)
                {
                    output = getFromKeyword(input.Substring(1, input.Length - 2));
                }
                else
                {
                    output = "";
                }
            }
            else if (input.Contains("{") || input.Contains("["))
            {
                output = handleBracket(input);
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
                    i += 1;
                }

                return Output;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        private string replaceWordEnding(String word, String find, String replace)
        {
            return word.Replace(find + " ", replace + " ").Replace(find + ",", replace + ",");
        }
        private String fixFindReplaceRule(String input)
        {
            try
            {
                input = replaceWordEnding(input, "ming ", "mming ");
                input = replaceWordEnding(input, "bing ", "bbing ");
                input = replaceWordEnding(input, " being ", " be-ing ");
                input = replaceWordEnding(input, "eing ", "ing ");
                input = replaceWordEnding(input, " be-ing ", " being ");
                return input;
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
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
