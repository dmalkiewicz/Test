using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NeuralNetworks.GraphicalClientCrap.Converters
{
    public class IntToUintConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var uint32 = value as uint?;
            if (uint32 == null)
            {
                return 0;
            }

            try
            {

                var simpleInt = (int)uint32;
                return simpleInt;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var int32 = value as int?;
            if (int32 == null)
            {
                var doubleValue = value as double?;
                if (doubleValue.HasValue)
                {
                    uint y = System.Convert.ToUInt32(doubleValue.Value);
                    return y;
                }

                return 0;
            }

            return int32.Value;
        }
    }
}
