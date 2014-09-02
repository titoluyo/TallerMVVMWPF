using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SFChallenge.Model;

namespace SFChallenge.Storage
{
    /// <summary>
    /// Provides XML serialization for collections of SuperPerson instances.
    /// </summary>
    public class SuperXmlSerializer : ISuperXmlSerializer
    {
        /// <summary>
        /// Reads the collection of super people from the XML using the specified reader.
        /// </summary>
        /// <param name="reader">The reader to use for reading XML.</param>
        /// <returns>A SuperPerson collection (possibly empty) that was read.</returns>
        public IEnumerable<SuperPerson> Read(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            List<SuperPerson> superPeople = new List<SuperPerson>();

            reader.ReadStartElement("SuperPeople");

            while (reader.IsStartElement("SuperPerson"))
            {
                superPeople.Add(this.ReadSuperPerson(reader));
            }

            return superPeople.AsEnumerable();
        }

        /// <summary>
        /// Writes the specified super people to XML using the specified writer.
        /// </summary>
        /// <param name="writer">The writer to use for writing XML.</param>
        /// <param name="superPeople">The SuperPerson collection to write.</param>
        public void Write(XmlWriter writer, IEnumerable<SuperPerson> superPeople)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (superPeople == null)
            {
                throw new ArgumentNullException("superPeople");
            }

            writer.WriteStartElement("SuperPeople");

            foreach (var superPerson in superPeople)
            {
                this.WriteSuperPerson(writer, superPerson);
            }

            writer.WriteFullEndElement();
        }

        private SuperPerson ReadSuperPerson(XmlReader reader)
        {
            SuperPerson superPerson = new SuperPerson();

            superPerson.Id = XmlConvert.ToInt32(reader.GetAttribute("Id"));
            superPerson.Name = reader.GetAttribute("Name");            
            superPerson.Allegiance = reader.GetAttribute("Allegiance");
            superPerson.Rank = XmlConvert.ToInt32(reader.GetAttribute("Rank"));            
            superPerson.Health = XmlConvert.ToInt32(reader.GetAttribute("Health"));
            superPerson.Strength = XmlConvert.ToInt32(reader.GetAttribute("Strength"));
            superPerson.Resistance = XmlConvert.ToInt32(reader.GetAttribute("Resistance"));
            superPerson.Intellect = XmlConvert.ToInt32(reader.GetAttribute("Intellect"));
            superPerson.Speed = XmlConvert.ToInt32(reader.GetAttribute("Speed"));

            reader.ReadStartElement("SuperPerson");
            reader.ReadEndElement();

            return superPerson;
        }

        private void WriteSuperPerson(XmlWriter writer, SuperPerson superPerson)
        {
            writer.WriteStartElement("SuperPerson");

            writer.WriteAttributeString("Id", XmlConvert.ToString(superPerson.Id));            
            writer.WriteAttributeString("Name", superPerson.Name);            
            writer.WriteAttributeString("Allegiance", superPerson.Allegiance);
            writer.WriteAttributeString("Rank", XmlConvert.ToString(superPerson.Rank));
            writer.WriteAttributeString("Health", XmlConvert.ToString(superPerson.Health));
            writer.WriteAttributeString("Strength", XmlConvert.ToString(superPerson.Strength));
            writer.WriteAttributeString("Speed", XmlConvert.ToString(superPerson.Speed));            
            writer.WriteAttributeString("Resistance", XmlConvert.ToString(superPerson.Resistance));
            writer.WriteAttributeString("Intellect", XmlConvert.ToString(superPerson.Intellect));

            writer.WriteFullEndElement();
        }
    }
}
