using System;
using System.Globalization;

namespace BifrostSenderCtree.Domain.Utils
{
    public static class Types
    {
        public static DateTime StringToDateTime(object fieldValue, string format, bool required = false)
        {
            if (DateTime.TryParseExact(fieldValue.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dataFeriado))
                return dataFeriado;

            if (required)
                throw new Exception("Could not convert to string to DateTime");

            return new DateTime();
        }
    }
}