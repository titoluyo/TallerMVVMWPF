using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ocean.Wpf.Infrastructure {

    /// <summary>
    /// Represents Cloner
    /// </summary>
    public static class Cloner {

        /// <summary>
        /// Initializes the <see cref="Cloner"/> class.
        /// </summary>
        static Cloner() { }

        /// <summary>
        /// Deep copy to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toClone">The obj to clone.</param>
        /// <returns></returns>
        public static T DeepCopy<T>(Object toClone) {
            using(var ms = new MemoryStream()) {

                var bf = new BinaryFormatter();
                bf.Serialize(ms, toClone);
                ms.Position = 0;
                return (T)(bf.Deserialize(ms));
            }
        }
    }
}
