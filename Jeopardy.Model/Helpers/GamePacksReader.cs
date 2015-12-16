using System;
using System.Collections.ObjectModel;
using System.IO;
using Jeopardy.Model.Interfaces;

namespace Jeopardy.Model.Helpers
{
    public static class GamePacksReader
    {
        public static ObservableCollection<Gameboard> GetSavedGameBoards()
        {
            var gameboards = new ObservableCollection<Gameboard>();
            foreach (string file in Directory.EnumerateFiles(Constants.APP_DATA_LOCATION, "*.xml"))
            {
                try
                {
                    var gameBoard = GetGameboardFromFile.GetGameboard(file);
                    gameboards.Add(gameBoard);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(string.Format("{0} could not be read.  {1}", file, e.Message));
                }
            }

            return gameboards;
        }
    }
}
