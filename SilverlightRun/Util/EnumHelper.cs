using System;

namespace SilverlightRun.Util
{
    /// <summary>
    /// Allows to convert a string value to a Enum value.
    /// </summary>
    public class EnumHelper
    {
        public static T Get<T>(string enumValue)
        {
            return (T)Enum.Parse(typeof(T), enumValue, false);
        }
    }
}
