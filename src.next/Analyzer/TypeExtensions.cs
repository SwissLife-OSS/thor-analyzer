﻿using System;
using System.ComponentModel;

namespace ChilliCream.Logging.Analyzer
{
    internal static class TypeExtensions
    {
        public static object Default(this Type type)
        {
            if (type == typeof(string))
            {
                return string.Empty;
            }

            if (type == typeof(byte[]))
            {
                return new byte[] { };
            }

            return Activator.CreateInstance(type);
        }

        public static object NotDefault(this Type type)
        {
            if (type == typeof(string))
            {
                return "An arbitrary string value";
            }

            if (type == typeof(Guid))
            {
                return Guid.NewGuid();
            }

            if (type == typeof(bool))
            {
                return true;
            }

            if (type == typeof(DateTime))
            {
                return DateTime.UtcNow;
            }

            if (type == typeof(byte[]))
            {
                return new byte[] { 1, 2, 3 };
            }

            TypeConverter converter = TypeDescriptor.GetConverter(type);

            if (converter != null && converter.CanConvertFrom(typeof(string)))
            {
                return converter.ConvertFromInvariantString("1");
            }

            return Activator.CreateInstance(type);
        }
    }
}