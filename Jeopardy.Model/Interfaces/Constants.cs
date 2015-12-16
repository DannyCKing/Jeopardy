using System;

namespace Jeopardy.Model.Interfaces
{
    public static class Constants
    {
        public const string APP_NAME = "DANIEL_JEOPARDY";

        public const string IMAGES_FOLDER_NAME = "images";

        public static string APP_DATA_LOCATION
        {
            get
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string fullPath = appDataPath + System.IO.Path.DirectorySeparatorChar + APP_NAME;

                bool exists = System.IO.Directory.Exists(fullPath);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                }

                return fullPath;
            }
        }

        public static string IMAGES_FOLDER_LOCATION
        {
            get
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string fullPath = APP_DATA_LOCATION + System.IO.Path.DirectorySeparatorChar + IMAGES_FOLDER_NAME;

                bool exists = System.IO.Directory.Exists(fullPath);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                }

                return fullPath;
            }
        }
    }
}
