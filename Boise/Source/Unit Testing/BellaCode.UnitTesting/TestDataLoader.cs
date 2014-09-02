using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.IO;

namespace BellaCode.UnitTesting
{
    public static class TestDataLoader
    {
        /// <summary>
        /// Loads the XML in an embedded resource in the calling assembly.
        /// </summary>
        /// <param name="resourcePath">The resource path (e.g. TestData.MyFile.xml)</param>
        /// <returns>An IXPathNavigable instance.</returns>
        public static IXPathNavigable LoadXml(string resourcePath)
        {
            Assert.IsNotNull(resourcePath);
            Assert.AreNotEqual(string.Empty, resourcePath);

            Assembly assembly = Assembly.GetCallingAssembly();
            return LoadXml(assembly, resourcePath);
        }

        /// <summary>
        /// Loads the XML in an embedded resource in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <param name="resourcePath">The resource path (e.g. TestData.MyFile.xml)</param>
        /// <returns>An IXPathNavigable instance.</returns>
        public static IXPathNavigable LoadXml(Assembly assembly, string resourcePath)
        {
            Assert.IsNotNull(resourcePath);
            Assert.AreNotEqual(string.Empty, resourcePath);

            string resourceName = assembly.GetName().Name + "." + resourcePath;

            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader stream = new StreamReader(assembly.GetManifestResourceStream(resourceName), Encoding.UTF8))
            {
                xmlDocument.Load(stream);
            }

            return xmlDocument;
        }

        /// <summary>
        /// Loads the text in an embedded resource in the calling assembly.
        /// </summary>
        /// <param name="resourcePath">The resource path (e.g. TestData.MyFile.txt)</param>
        /// <returns>A string.</returns>
        public static string LoadText(string resourcePath)
        {
            Assert.IsNotNull(resourcePath);
            Assert.AreNotEqual(string.Empty, resourcePath);

            Assembly assembly = Assembly.GetCallingAssembly();
            return LoadText(assembly, resourcePath);
        }

        /// <summary>
        /// Loads the text in an embedded resource in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// /// <param name="resourcePath">The resource path (e.g. TestData.MyFile.txt)</param>
        /// <returns>A string.</returns>
        public static string LoadText(Assembly assembly, string resourcePath)
        {
            Assert.IsNotNull(resourcePath);
            Assert.AreNotEqual(string.Empty, resourcePath);

            string resourceName = assembly.GetName().Name + "." + resourcePath;

            string content = null;

            using (TextReader textReader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
            {
                content = textReader.ReadToEnd();
            }

            return content;
        }
    }
}
