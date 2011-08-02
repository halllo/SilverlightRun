using System.Runtime.Serialization;

namespace SilverlightRun.Util
{
    /// <summary>
    /// Allows to create a Tuple2 with inferred types.
    /// </summary>
    public static class Tuple2
    {
        public static Tuple2<T, U> With<T, U>(T t, U u)
        {
            return new Tuple2<T, U>(t, u);
        }        

        public static Tuple2<T, U> And<T, U>(this T t, U u)
        {
            return new Tuple2<T, U>(t, u);
        }
    }

    /// <summary>
    /// Generic set of two values of any type.
    /// </summary>
    /// <typeparam name="T">Type of first value.</typeparam>
    /// <typeparam name="U">Type of second value.</typeparam>
    [DataContract]
    public class Tuple2<T, U>
    {
        [DataMember]
        public T First { get; set; }
        [DataMember]
        public U Second { get; set; }

        public Tuple2(T t, U u)
        {
            First = t;
            Second = u;
        }
    }
}
