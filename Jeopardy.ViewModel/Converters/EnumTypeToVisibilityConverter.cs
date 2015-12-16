using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Jeopardy.Model.Interfaces;

namespace Jeopardy.ViewModel
{
    public class EnumTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DisplayType)
            {
                var displaytype = (DisplayType)value;
                if (displaytype == DisplayType.Picture)
                {
                    // hide text
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
