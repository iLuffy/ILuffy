using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ILuffy.IOP
{
    /// <summary>
    /// Serializer Helper
    /// </summary>
    internal class SerializerHelper
    {
        /// <summary>
        /// Serializes an object using the <see cref="XmlSerializer" />
        /// into an XML string.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>An XML string containing serialized
        /// data.
        /// </returns>
        public static String SerializeToXmlString<TData>(TData data)
        {
            StringBuilder serializedData = new StringBuilder();
            using (StringWriter writer = new EncodingStringWriter(Encoding.UTF8, serializedData, CultureInfo.InvariantCulture))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TData));
                serializer.Serialize(writer, data);
            }
            return serializedData.ToString();
        }

        /// <summary>
        /// Serializes an object using the <see cref="XmlSerializer" />
        /// into an XML string.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>An XML string containing serialized
        /// data.
        /// </returns>
        public static String SerializeToXmlString(Object data)
        {
            StringBuilder serializedData = new StringBuilder();
            using (StringWriter writer = new EncodingStringWriter(Encoding.UTF8, serializedData, CultureInfo.InvariantCulture))
            {
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(writer, data);
            }
            return serializedData.ToString();
        }

        /// <summary>
        /// Deserializes an object from an XML string
        /// using the <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="data">The XML data to Deserializes.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TData DeserializeFromXmlString<TData>(String data)
        {
            using (StringReader reader = new StringReader(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TData));
                return (TData)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Deserializes an object from an XML string
        /// using the <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="data">The XML data to Deserializes.</param>
        /// <param name="Type">The type of data to process.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static Object DeserializeFromXmlString(String data, Type type)
        {
            using (StringReader reader = new StringReader(data))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Serializes an object using the <see cref="XmlSerializer" />
        /// into an XML string.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>An XML string containing serialized
        /// data.
        /// </returns>
        public static String SerializeToXmlStringWithoutDecalaring<TData>(TData data)
        {
            String result = SerializerHelper.SerializeToXmlString<TData>(data);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            return doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// Serializes an object using the <see cref="XmlSerializer" />
        /// into an XML string.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>An XML string containing serialized
        /// data.
        /// </returns>
        public static String SerializeToXmlStringWithoutDecalaring(Object data)
        {
            String result = SerializerHelper.SerializeToXmlString(data);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            return doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// Serializes an object using the <see cref="XmlSerializer" />
        /// into an XML string.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>An XML string containing serialized
        /// data.
        /// </returns>
        public static Object DeserializeToXmlStringWithoutDecalaring(String data, Type type)
        {
            return DeserializeFromXmlString(data, type);
        }

        /// <summary>
        /// Deserializes an object from an XML string
        /// using the <see cref="XmlSerializer" />.
        /// </summary>
        /// <param name="data">The XML data to Deserializes.</param>
        /// <typeparam name="TData">The type of data to process.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TData DeserializeFromXmlStringWithoutDecalaring<TData>(String data)
        {
            return SerializerHelper.DeserializeFromXmlString<TData>(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String SerializeToBase64StringByDataContractSerializer(Object data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var dataContractSerializer = new DataContractSerializer(data.GetType());
                dataContractSerializer.WriteObject(ms, data);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// make sure the correct type of the data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String SerializeToBase64StringByDataContractSerializer(Object data, Type type)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var dataContractSerializer = new DataContractSerializer(type);
                dataContractSerializer.WriteObject(ms, data);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static Byte[] SerializeToStreamByDataContractSerializer(Object data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var dataContractSerializer = new DataContractSerializer(data.GetType());
                dataContractSerializer.WriteObject(ms, data);

                return ms.ToArray();
            }
        }

        public static Object DeserializeFromStreamByDataContractSerializer(Type type, Byte[] data, int offset, int length)
        {
            using (MemoryStream ms = new MemoryStream(data, offset, length))
            {
                ms.Position = 0;
                var dataContractSerializer = new DataContractSerializer(type);
                return dataContractSerializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 利用DataContractSerializer进行序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String SerializeByDataContractSerializer(Object data)
        {
            var serializedStringBuilder = new StringBuilder();
            using (var encodingStringWriter = new EncodingStringWriter(Encoding.UTF8, serializedStringBuilder, CultureInfo.InvariantCulture))
            {
                using (XmlTextWriter writer = new XmlTextWriter(encodingStringWriter))
                {
                    var dataContractSerializer = new DataContractSerializer(data.GetType());
                    dataContractSerializer.WriteObject(writer, data);
                    return serializedStringBuilder.ToString();
                }
            }
        }

        /// <summary>
        /// make the correct type of the data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String SerializeByDataContractSerializer(Object data, Type type)
        {
            var serializedStringBuilder = new StringBuilder();
            using (var encodingStringWriter = new EncodingStringWriter(Encoding.UTF8, serializedStringBuilder, CultureInfo.InvariantCulture))
            {
                using (XmlTextWriter writer = new XmlTextWriter(encodingStringWriter))
                {
                    var dataContractSerializer = new DataContractSerializer(type);
                    dataContractSerializer.WriteObject(writer, data);
                    return serializedStringBuilder.ToString();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object DeserializeFromBase64StringByDataContractSerializer(String data, Type type)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] content = Convert.FromBase64String(data);
                ms.Write(content, 0, content.Length);
                ms.Position = 0;
                var dataContractSerializer = new DataContractSerializer(type);
                return dataContractSerializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 利用DataContractSerializer进行反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeByDataContractSerializer<T>(string data)
        {
            using (var stringReader = new StringReader(data))
            {
                using (XmlTextReader reader = new XmlTextReader(stringReader))
                {
                    var dataContractSerializer = new DataContractSerializer(typeof(T));
                    return (T)dataContractSerializer.ReadObject(reader);
                }
            }
        }

        /// <summary>
        /// DeserializeByJavaScript
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeByJavaScript<T>(string data)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Deserialize<T>(data);
        }

        public static string SerializeByJavaScript(Object data)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(data);
        }

        /// <summary>
        /// 利用DataContractSerializer进行反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object DeserializeByDataContractSerializer(string data, Type type)
        {
            using (var stringReader = new StringReader(data))
            {
                using (var xmlTextReader = new XmlTextReader(stringReader))
                {
                    var dataContractSerializer = new DataContractSerializer(type);
                    return dataContractSerializer.ReadObject(xmlTextReader);
                }
            }
        }
    }
}