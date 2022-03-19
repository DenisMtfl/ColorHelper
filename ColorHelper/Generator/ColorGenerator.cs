using System;

namespace ColorHelper
{
    public static partial class ColorGenerator
    {
        public static T GetRandomColor<T>() where T: IColor
        {
            return GetRandomColor<T>(GetColorFilter(ColorThemesEnum.Default));
        }

        public static T GetLightRandomColor<T>() where T : IColor
        {
            return GetRandomColor<T>(GetColorFilter(ColorThemesEnum.Light));
        }

        public static T GetDarkRandomColor<T>() where T : IColor
        {
            return GetRandomColor<T>(GetColorFilter(ColorThemesEnum.Dark));
        }

        public static T GetRandomColorExact<T>(int hashCode, ColorThemesEnum colorTheme = ColorThemesEnum.Default) where T : IColor
        {
            return GetRandomColorExact<T>(hashCode, GetColorFilter(colorTheme));
        }

        private static RgbRandomColorFilter GetColorFilter(ColorThemesEnum colorTheme)
        {
            const byte maxRangeValue = 80;
            const byte minRangeValue = 170;

            RgbRandomColorFilter filter = new RgbRandomColorFilter();

            switch (colorTheme)
            {
                case ColorThemesEnum.Default:
                    break;
                case ColorThemesEnum.Light:
                    filter.minR = minRangeValue;
                    filter.minG = minRangeValue;
                    filter.minB = minRangeValue;
                    break;
                case ColorThemesEnum.Dark:
                    filter.maxR = maxRangeValue;
                    filter.maxG = maxRangeValue;
                    filter.maxB = maxRangeValue;
                    break;
            }

            return filter;
        }

        private static T GetRandomColor<T>(RgbRandomColorFilter filter) where T : IColor
        {
            Random random = new Random(DateTime.Now.Millisecond);
            return GetRandomColor<T>(filter, random);
        }

        private static T GetRandomColorExact<T>(int hashCode, RgbRandomColorFilter filter) where T : IColor
        {
            Random random = new Random(DateTime.Now.Millisecond + hashCode);
            return GetRandomColor<T>(filter, random);
        }

        private static T GetRandomColor<T>(RgbRandomColorFilter filter, Random random) where T : IColor
        {
            RGB rgb = new RGB(
                (byte)random.Next(filter.minR, filter.maxR),
                (byte)random.Next(filter.minG, filter.maxG),
                (byte)random.Next(filter.minB, filter.maxB));

            return ConvertRgbToNecessaryColorType<T>(rgb);
        }

        private static T ConvertRgbToNecessaryColorType<T>(RGB rgb) where T: IColor
        {
            if (typeof(T) == typeof(RGB))
            {
                return (T)(object)rgb;
            }
            else if (typeof(T) == typeof(HEX))
            {
                return (T)(object)ColorConverter.RgbToHex(rgb);
            }
            else if (typeof(T) == typeof(CMYK))
            {
                return (T)(object)ColorConverter.RgbToCmyk(rgb);
            }
            else if (typeof(T) == typeof(HSV))
            {
                return (T)(object)ColorConverter.RgbToHsv(rgb);
            }
            else if (typeof(T) == typeof(HSL))
            {
                return (T)(object)ColorConverter.RgbToHsl(rgb);
            }
            else if (typeof(T) == typeof(XYZ))
            {
                return (T)(object)ColorConverter.RgbToXyz(rgb);
            }
            else
            {
                throw new ArgumentException("Incorrect class type");
            }
        }
    }
}
