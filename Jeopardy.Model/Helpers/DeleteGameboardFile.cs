using System.IO;
using Jeopardy.Model.Interfaces;

namespace Jeopardy.Model.Helpers
{
    public class DeleteGameboardFile
    {
        public static void DeleteGamebordFile(Gameboard gameboard)
        {
            string fileName = gameboard.UniqueIdentifier.ToString();

            string fileNameAndExtension =
                 Constants.APP_NAME + fileName + ".xml";

            var pathAndFile = Constants.APP_DATA_LOCATION + System.IO.Path.DirectorySeparatorChar + fileNameAndExtension;

            File.Delete(pathAndFile);
        }
    }
}
