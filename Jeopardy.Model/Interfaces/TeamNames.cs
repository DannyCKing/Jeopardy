using System;

namespace Jeopardy.Model.Interfaces
{
    public static class TeamNames
    {
        private static Random rand;

        private static string[] _adjectives = {"Big", "Grumpy", "Incompetent", "Incontinent", "Average", "Medicore",
                                        "Hot", "Smelly", "Whiny", "Darn", "Yellow", "Pink", "Mad"};

        private static string[] _nouns = { "Gorillas", "Monkeys", "People", "Lions", "Pansies", "Elephants", "Einstiens", 
                                    "Mamas", "Kittens", "Kardashians" , "Hillary Clinton Fans", "Penguins"};


        static TeamNames()
        {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public static string GetRandomTeamName()
        {
            string adjective = GetRandomAdjective();

            string noun = GetRandomNoun();

            return adjective + " " + noun;
        }

        private static string GetRandomNoun()
        {
            return GetRandomItem(_nouns);
        }

        private static string GetRandomAdjective()
        {
            return GetRandomItem(_adjectives);
        }

        private static string GetRandomItem(string[] list)
        {
            int r = rand.Next(list.Length);

            return list[r];
        }
    }
}
