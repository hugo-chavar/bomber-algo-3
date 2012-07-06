using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BombermanModel.Mapa
{
    /// <summary>
    /// Serializable dictionary
    /// </summary>
    /// <typeparam name="TKey">Key template</typeparam>
    /// <typeparam name="TValue">Value template</typeparam>
    [Serializable]
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members

        /// <summary>
        /// Noop IXmlSerializable
        /// </summary>
        /// <returns>Always null</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }


        /// <summary>
        /// Read xml from stream reader
        /// </summary>
        /// <param name="reader">Xml reader</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof (TKey));

            XmlSerializer valueSerializer = new XmlSerializer(typeof (TValue));


            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();


            if (wasEmpty)
            {
                return;
            }


            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");


                reader.ReadStartElement("key");

                TKey key = (TKey) keySerializer.Deserialize(reader);

                reader.ReadEndElement();


                reader.ReadStartElement("value");

                TValue value = (TValue) valueSerializer.Deserialize(reader);

                reader.ReadEndElement();


                Add(key, value);


                reader.ReadEndElement();

                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }


        /// <summary>
        /// Write xml data to xml writer
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof (TKey));

            XmlSerializer valueSerializer = new XmlSerializer(typeof (TValue));


            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");


                writer.WriteStartElement("key");

                keySerializer.Serialize(writer, key);

                writer.WriteEndElement();


                writer.WriteStartElement("value");

                TValue value = this[key];

                valueSerializer.Serialize(writer, value);

                writer.WriteEndElement();


                writer.WriteEndElement();
            }
        }

        #endregion

        /// <summary>
        /// Overload constructor
        /// </summary>
        /// <param name="dictionary">Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
        ///    class that contains elements copied from the specified System.Collections.Generic.IDictionary<TKey,TValue>
        ///    and uses the default equality comparer for the key type.</param>
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {}

        /// <summary>
        ///     Initializes a new instance of the System.Collections.Generic.Dictionary<TKey,TValue>
        ///     class that is empty, has the default initial capacity, and uses the default
        ///     equality comparer for the key type.
        /// </summary>
        public SerializableDictionary()
            : base()
        {}


        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"></see> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        public SerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}

        /// <summary>
        /// Returns array of extra types used to serialize/deserialize config from/to xml.
        /// </summary>
        /// <returns></returns>
        public Type[] GetInnerTypes()
        {
            return new Type[] {typeof (TKey), typeof (TValue)};
        }
    }
}