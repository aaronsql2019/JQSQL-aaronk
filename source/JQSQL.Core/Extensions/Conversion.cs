﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JQSQL.Core.Extensions
{
    public static class Conversion
    {
        private static double? ConvertToDouble(this object valueToConvert)
        {
            if (valueToConvert == null)
                return null;

            double cValue;
            if (double.TryParse(valueToConvert.ToString(), out cValue))
                return cValue;

            return null;
        }

        private static DateTime? ConvertToDateTime(this object valueToConvert)
        {
            if (valueToConvert == null)
                return null;

            DateTime cValue;
            if (DateTime.TryParse(valueToConvert.ToString(), out cValue))
                return cValue;

            return null;
        }

        private static bool? ConvertToBoolean(this object valueToConvert)
        {
            if (valueToConvert == null)
                return null;

            bool cValue;
            if (bool.TryParse(valueToConvert.ToString(), out cValue))
                return cValue;

            return null;
        }

        public static Nullable<T> SafeCast<T>(this object valueToConvert) where T : struct
        {
            object val = null;
            if (typeof(T) == typeof(double))
            {
                val = valueToConvert.ConvertToDouble();
            }
            else if (typeof(T) == typeof(DateTime))
            {
                val = valueToConvert.ConvertToDateTime();
            }
            else if (typeof(T) == typeof(bool))
            {
                val = valueToConvert.ConvertToBoolean();
            }

            if (val != null)
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }

            return null;
        }

        public static bool IsTypeof<T>(this object value)
        {
            return (value != null && value is T);
        }

        public static SqlDbType GetDbType(this object value)
        {
            if (value.SafeCast<double>() != null)
            {
                return SqlDbType.Float;
            }
            else if (value.SafeCast<DateTime>() != null)
            {
                return SqlDbType.DateTime;
            }
            else if (value.SafeCast<bool>() != null)
            {
                return SqlDbType.Bit;
            }

            return SqlDbType.NVarChar;
        }

        /// <summary>
        /// Check if given value is a primitive type such as int, string in the context
        /// </summary>
        /// <param name="value">Value to be checked</param>
        /// <returns>Primitive status</returns>
        public static bool IsPrimitive(this object value)
        {
            return !(value is SimpleJson.JsonArray || value is SimpleJson.JsonObject);
        }
    }
}
