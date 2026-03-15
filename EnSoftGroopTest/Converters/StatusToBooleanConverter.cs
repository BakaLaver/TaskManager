using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TaskStatus = EnSoftGroopTest.Models.TaskStatus;

namespace EnSoftGroopTest.Converters
{
    public class StatusToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskStatus status)
            {
                return status == TaskStatus.Completed;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCompleted)
            {
                return isCompleted ? TaskStatus.Completed : TaskStatus.Active;
            }
            return TaskStatus.Active;
        }
    }
}
