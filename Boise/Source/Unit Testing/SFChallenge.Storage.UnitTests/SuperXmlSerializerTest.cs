using SFChallenge.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Xml;
using System.IO;
using SFChallenge.Model;
using System.Collections.Generic;
using BellaCode.UnitTesting;
using System.Xml.XPath;

namespace SFChallenge.Storage.UnitTests
{
    // Note: Example of testing a data in/out class
    [TestClass()]
    public class SuperXmlSerializerTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInstantiated()
        {
            // Arrange

            // Act
            SuperXmlSerializer actual = new SuperXmlSerializer();

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenReadWithReaderNull_ThenThrows()
        {
            // Arrange

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlReader reader = null;

            // Act            
            target.Read(reader);

            // Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(XmlException))]
        public void WhenReadWithEmptyXml_ThenThrows()
        {
            // Arrange
            string inputXml = "";

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            // Act
            using (StringReader stringReader = new StringReader(inputXml))
            {
                using (XmlReader reader = XmlReader.Create(stringReader, settings))
                {
                    target.Read(reader);
                }
            }

            // Assert
        }

        [TestMethod()]
        public void WhenReadWithEmptySet_ThenReturnsEmptyCollection()
        {
            // Note: Example of using strings for XML

            // Arrange
            string inputXml = "<SuperPeople></SuperPeople>";

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            // Act
            IEnumerable<SuperPerson> actual;
            using (StringReader stringReader = new StringReader(inputXml))
            {
                using (XmlReader reader = XmlReader.Create(stringReader, settings))
                {
                    actual = target.Read(reader);
                }
            }

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod()]
        public void WhenReadWithValidXml_ThenReturnsCollection()
        {
            // Note: Example of using embedded resource XML with TestDataLoader for input

            // Arrange
            string inputXml = TestDataLoader.LoadText("TestData.SuperPeople.Valid.xml");

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            // Act
            IEnumerable<SuperPerson> actual;
            using (StringReader stringReader = new StringReader(inputXml))
            {
                using (XmlReader reader = XmlReader.Create(stringReader, settings))
                {
                    actual = target.Read(reader);
                }
            }

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(5, actual.Count());

            int i = 1;
            foreach (var actualItem in actual)
            {
                Assert.AreEqual(i, actualItem.Id);
                Assert.AreEqual("Name" + i.ToString(), actualItem.Name);
                Assert.AreEqual("Allegiance" + i.ToString(), actualItem.Allegiance);
                Assert.AreEqual(i, actualItem.Rank);
                Assert.AreEqual(i, actualItem.Health);
                Assert.AreEqual(i, actualItem.Strength);
                Assert.AreEqual(i, actualItem.Speed);
                Assert.AreEqual(i, actualItem.Resistance);
                Assert.AreEqual(i, actualItem.Intellect);

                i++;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenWriteWithWriterNull_ThenThrows()
        {
            // Arrange
            List<SuperPerson> superPeople = new List<SuperPerson>()
            {
                new SuperPerson() {
                     Id = 1,
                     Name = "Superman",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 1,
                     Health = 1000,
                     Strength = 10,                     
                     Resistance = 80,
                     Intellect = 20,
                     Speed = 60
                },
            };

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlWriter writer = null;

            // Act

            target.Write(writer, superPeople);

            // Assert            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenWriteWithSuperPeopleNull_ThenThrows()
        {
            // Arrange
            List<SuperPerson> superPeople = null;

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.CloseOutput = false;
            writerSettings.Encoding = System.Text.Encoding.UTF8;
            writerSettings.Indent = false;
            writerSettings.OmitXmlDeclaration = false;

            using (StringWriter stringWriter = new StringWriter())
            {
                // Act
                using (XmlWriter writer = XmlWriter.Create(stringWriter, writerSettings))
                {
                    target.Write(writer, superPeople);
                }
            }

            // Assert
        }

        [TestMethod()]
        public void WhenWriteWithEmptyCollection_ThenCreatesXml()
        {
            // Arrange
            List<SuperPerson> superPeople = new List<SuperPerson>();

            string expected = "<SuperPeople></SuperPeople>";

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.CloseOutput = false;
            writerSettings.Encoding = System.Text.Encoding.UTF8;
            writerSettings.Indent = false;
            writerSettings.OmitXmlDeclaration = false;

            string actual;
            using (StringWriter stringWriter = new StringWriter())
            {
                // Act
                using (XmlWriter writer = XmlWriter.Create(stringWriter, writerSettings))
                {
                    target.Write(writer, superPeople);
                    writer.Flush();
                    stringWriter.Flush();

                    actual = stringWriter.ToString();
                }

                // Note: Example of using AssertXml for comparison

                // Assert
                AssertXml.AreEqual(expected, actual);
            }
        }

        // Note: Example of extraneous method.  How much different is this than the multiple-item write test case.

        [TestMethod()]
        public void WhenWriteWithOneSuperPerson_ThenWritesXml()
        {
            // Arrange
            List<SuperPerson> superPeople = new List<SuperPerson>()
            {
                new SuperPerson() {
                     Id = 1,
                     Name = "Superman",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 1,
                     Health = 1000,
                     Strength = 10,                     
                     Resistance = 80,
                     Intellect = 20,
                     Speed = 60
                },
            };

            // Note: Example of using complex XML string with TestDataLoader for expected
            // Note: Would skip in favor of TestDataLoader

            string expected =
                "<SuperPeople>" +
                    "<SuperPerson Id='1' Name='Superman' Allegiance='Super Friends' Rank='1' Health='1000' Strength='10' Speed='60' Resistance='80' Intellect='20'></SuperPerson>" +
                "</SuperPeople>";


            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.CloseOutput = false;
            writerSettings.Encoding = System.Text.Encoding.UTF8;
            writerSettings.Indent = false;
            writerSettings.OmitXmlDeclaration = false;

            // Act
            string actual;
            using (StringWriter stringWriter = new StringWriter())
            {                
                using (XmlWriter writer = XmlWriter.Create(stringWriter, writerSettings))
                {
                    target.Write(writer, superPeople);
                    writer.Flush();
                    stringWriter.Flush();

                    actual = stringWriter.ToString();
                }                
            }

            // Assert
            AssertXml.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WhenWrite_ThenWritesXml()
        {
            // Arrange
            List<SuperPerson> superPeople = new List<SuperPerson>()
            {
                new SuperPerson() {
                     Id = 1,
                     Name = "Name1",
                     Allegiance = "Allegiance1",
                     Rank = 1,
                     Health = 1,
                     Strength = 1,                     
                     Resistance = 1,
                     Intellect = 1,
                     Speed = 1
                },
                new SuperPerson() {
                     Id = 2,
                     Name = "Name2",
                     Allegiance = "Allegiance2",
                     Rank = 2,
                     Health = 2,
                     Strength = 2,                     
                     Resistance = 2,
                     Intellect = 2,
                     Speed = 2
                },
                new SuperPerson() {
                     Id = 3,
                     Name = "Name3",
                     Allegiance = "Allegiance3",
                     Rank = 3,
                     Health = 3,
                     Strength = 3,                     
                     Resistance = 3,
                     Intellect = 3,
                     Speed = 3
                },
                new SuperPerson() {
                     Id = 4,
                     Name = "Name4",
                     Allegiance = "Allegiance4",
                     Rank = 4,
                     Health = 4,
                     Strength = 4,                     
                     Resistance = 4,
                     Intellect = 4,
                     Speed = 4
                },
                new SuperPerson() {
                     Id = 5,
                     Name = "Name5",
                     Allegiance = "Allegiance5",
                     Rank = 5,
                     Health = 5,
                     Strength = 5,                     
                     Resistance = 5,
                     Intellect = 5,
                     Speed = 5
                },
            };

            // Note: Example of using embedded resource XML with TestDataLoader for expected results

            string expected = TestDataLoader.LoadText("TestData.SuperPeople.Valid.xml");

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.CloseOutput = false;
            writerSettings.Encoding = System.Text.Encoding.UTF8;
            writerSettings.Indent = false;
            writerSettings.OmitXmlDeclaration = false;

            // Act
            string actual;
            using (StringWriter stringWriter = new StringWriter())
            {
                
                using (XmlWriter writer = XmlWriter.Create(stringWriter, writerSettings))
                {
                    target.Write(writer, superPeople);
                    writer.Flush();
                    stringWriter.Flush();

                    actual = stringWriter.ToString();
                }                
            }            

            // Assert
            AssertXml.AreEqual(expected, actual);
        }

        // Note: Round-trip test not enough on its own (could be null/null), but very useful for serialization correctness.

        [TestMethod()]
        public void WhenReadAndWrite_ThenRoundtripsXml()
        {
            // Arrange
            string inputXml = TestDataLoader.LoadText("TestData.SuperPeople.Valid.xml");
            string expected = inputXml;

            SuperXmlSerializer target = new SuperXmlSerializer();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.CloseOutput = false;
            writerSettings.Encoding = System.Text.Encoding.UTF8;
            writerSettings.Indent = false;
            writerSettings.OmitXmlDeclaration = false;

            // Act
            IEnumerable<SuperPerson> superPeople;
            using (StringReader stringReader = new StringReader(inputXml))
            {
                using (XmlReader reader = XmlReader.Create(stringReader, settings))
                {
                    superPeople = target.Read(reader);
                }
            }
            
            string actual;
            using (StringWriter stringWriter = new StringWriter())
            {

                using (XmlWriter writer = XmlWriter.Create(stringWriter, writerSettings))
                {
                    target.Write(writer, superPeople);
                    writer.Flush();
                    stringWriter.Flush();

                    actual = stringWriter.ToString();
                }
            }

            // Assert
            AssertXml.AreEqual(expected, actual);
        }
    }
}
