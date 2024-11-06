using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuizMaker_labb3.Converter
{
    internal class AnswerButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (values != null && values.Length == 2)
            {
                string answer = values[0]?.ToString();
                string buttonNr = values[1]?.ToString();
                string[] tempBox = { answer, buttonNr };

                return tempBox;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
