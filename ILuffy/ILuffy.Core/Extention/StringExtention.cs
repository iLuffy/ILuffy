using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILuffy.Halo
{
    public static class StringExtension
    {
        private static class Cache<T>
        {
            public static Func<string, T> Get;
            public static Func<T, string> GetString;
        }

        static StringExtension()
        {
            Cache<string>.Get = GetString;
            Cache<string>.GetString = GetString;
            Cache<bool>.Get = GetBoolean;
            Cache<bool>.GetString = GetBooleanAsString;
            Cache<double>.Get = GetDouble;
            Cache<double>.GetString = GetDoubleAsString;
            Cache<int>.Get = GetInt;
            Cache<int>.GetString = GetIntAsString;
            //Cache<WeightCalculateMethod>.Get = typeof(StringExtension).GetMethod("GetBoolean").CreateDelegate<Func<string, bool>>();
        }

        static string GetString(string value)
        {
            return value;
        }

        static bool GetBoolean(string value)
        {
            return bool.Parse(value);
        }

        static string GetBooleanAsString(bool value)
        {
            return value.ToString();
        }

        static double GetDouble(string value)
        {
            return double.Parse(value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        static string GetDoubleAsString(double value)
        {
            return value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        static int GetInt(string value)
        {
            return int.Parse(value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        static string GetIntAsString(int value)
        {
            return value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        static T GetEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        static string GetEnumAsString<T>(T value)
        {
            return Enum.Format(typeof(T), value, "F");
        }

        public static Nullable<T> Get<T>(this string value)
        {
            if (value == null)
            {
                return new Nullable<T>();
            }

            if (typeof(T).IsEnum)
            {
                return new Nullable<T>(GetEnum<T>(value));
            }

            return new Nullable<T>(Cache<T>.Get(value));
        }

        public static string Get<T>(T value)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(T).IsEnum)
            {
                return GetEnumAsString<T>(value);
            }

            return Cache<T>.GetString(value);
        }
    }
}
