using System.Drawing;
using Jeopardy.Model.Interfaces;
using Jeopardy.ViewModel;

namespace Jeopardy.Model
{
    public class JeopardyImage : NotifyPropertyChanged, IDisplayFullScreen
    {
        public DisplayType DisplayType
        {
            get { return DisplayType.Picture; }
        }

        private string _source;

        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        public int Value
        {
            get { return 0; }
        }

        public bool IsPlayed
        {
            get
            {
                return false;
            }
            set
            {
                return;
            }
        }

        public string Answer
        {
            get
            {
                return string.Empty;
            }
            set
            {
                return;
            }
        }

        public JeopardyImage(Bitmap source, string name)
        {
            var filePathAndName = Constants.IMAGES_FOLDER_LOCATION + System.IO.Path.DirectorySeparatorChar + name;
            source.Save(filePathAndName);
            Source = filePathAndName;
        }


        public System.Drawing.Color BackgroundColor
        {
            get
            {
                return Color.Blue;
            }
            set
            {

            }
        }
    }
}
