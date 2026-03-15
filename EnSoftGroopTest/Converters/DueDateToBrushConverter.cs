using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace EnSoftGroopTest.Converters
{
    public class DueDateToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dueDate)
            {
                // Если срок прошел и задача еще активна - выделяем красным
                if (dueDate.Date < DateTime.Now.Date)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 200, 200)); // Светло-красный
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
