using System;
using System.Collections.ObjectModel;
using System.Linq;
namespace Jeopardy.Model.Interfaces
{
    public static class TeamNames
    {
        private static Random rand;

        private static string[] _adjectives = {"Big", "Grumpy", "Incompetent", "Incontinent", "Average", "Medicore",
                                        "Hot", "Smelly", "Whiny", "Darn", "Yellow", "Pink", "Mad" , "Fire Breathing",
                                              "Tenacious", "Crazy" ,"Purple", "The Mighty Morphin"};

        private static string[] _nouns = { "Gorillas", "Monkeys", "People", "Lions", "Pansies", "Elephants", "Einstiens", 
                                    "Mamas", "Kittens", "Hillary Clinton Fans", "Penguins", "Daddys" , "Twilight Fans",
                                         "Chargers" , "Thunder" , "Dinosaurs" , "Rubber Duckies" , "Squirrels", "Llamas", "Goats" , 
                                         "Tuna Fish" , "Hobbits", "Dragons", "Jedi Masters" , "Warthogs", "Power Rangers" , 
                                         "A-Team" , "B-Team", "Cookie Monsters" , "Yellow Jackets" };


        static TeamNames()
        {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public static string GetRandomTeamName(ObservableCollection<Player> existingPlayers)
        {
            var returnName = "";
            do
            {
                string adjective = GetRandomAdjective();

                string noun = GetRandomNoun();

                returnName = adjective + " " + noun;

            } while (existingPlayers.Any(x => x.Name == returnName));

            return returnName;
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
