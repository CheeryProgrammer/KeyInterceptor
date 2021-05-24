using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KeyInterceptor.WPF.Settings
{
    public static class BrushStringExtensions
    {
        internal static string ToCsvString(this Brush fontColor)
        {
            Color color = (fontColor as SolidColorBrush).Color;
            return $"{color.R},{color.G},{color.B},{color.A}";
        }

        internal static Brush ToBrush(this string colorString)
        {
            string[] rgbaStr = colorString.Split(',');
            if (rgbaStr.Length != 4)
                throw new ArgumentException("String must be a four byte comma separated values (\"255,255,255,255\")");
            return new SolidColorBrush(Color.FromArgb(
                byte.Parse(rgbaStr[3]),
                byte.Parse(rgbaStr[0]),
                byte.Parse(rgbaStr[1]),
                byte.Parse(rgbaStr[2])
                ));
        }
    }
}
