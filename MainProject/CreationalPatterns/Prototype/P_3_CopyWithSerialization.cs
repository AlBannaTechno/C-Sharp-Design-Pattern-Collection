using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using static System.Console;
namespace MainProject.CreationalPatterns.Prototype
{
    namespace P_3_CopyWithSerialization
    {
        public static class CloneExtensions
        {
            /// <summary>
            /// Deep Cloning With Binary Serialization
            /// To Use This Method You Must Mark All Types With [Serializable]
            /// </summary>
            /// <param name="self"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T DeepBinaryCopy<T>(this T self)
            {
                var stream = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                object copy = formatter.Deserialize(stream);
                stream.Close();
                return (T) copy;
            }

            /// <summary>
            /// Create Deep Copy With Xml Serialization
            /// Tto Use This Method :
            ///  * All Types/Classes Must Have An Empty Constructor
            ///  * All Classes Must Be Public
            /// </summary>
            /// <param name="self"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T DeepXmlCopy<T>(this T self)
            {
                using var stream = new MemoryStream();
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T) ser.Deserialize(stream);
            }
        }
        [Serializable]
        public class Person
        {
            public Person()
            {

            }
            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public string[] Names { get; set; }
            public Address Address { get; set; }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }
        }
        [Serializable]
        public class Address
        {
            public Address()
            {

            }
            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName;
                HouseNumber = houseNumber;
            }

            public string StreetName { get; set; }
            public int HouseNumber { get; set; }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }

        public static class CopyWithSerialization
        {
            public static void Run()
            {
                var osama = new Person(new []{"Osama", "Al Banna"}, new Address("ASD", 14));
                // var ahmed = osama.DeepXmlCopy();
                var ahmed = osama.DeepXmlCopy();
                ahmed.Names[0] = "Ahmed";
                ahmed.Address.HouseNumber = 885;
                WriteLine(osama);
                WriteLine(ahmed);
            }
        }
    }
}
