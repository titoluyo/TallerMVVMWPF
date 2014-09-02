using System;
using System.Collections.Generic;
using SFChallenge.Model;
using System.Xml;

namespace SFChallenge.Storage
{
    /// <summary>
    /// Provides XML serialization for collections of SuperPerson instances.
    /// </summary>
    public interface ISuperXmlSerializer
    {
        /// <summary>
        /// Reads the collection of super people from the XML using the specified reader.
        /// </summary>
        /// <param name="reader">The reader to use for reading XML.</param>
        /// <returns>A SuperPerson collection (possibly empty) that was read.</returns>
        IEnumerable<SuperPerson> Read(XmlReader reader);

        /// <summary>
        /// Writes the specified super people to XML using the specified writer.
        /// </summary>
        /// <param name="writer">The writer to use for writing XML.</param>
        /// <param name="superPeople">The SuperPerson collection to write.</param>
        void Write(XmlWriter writer, IEnumerable<SuperPerson> superPeople);
    }
}
