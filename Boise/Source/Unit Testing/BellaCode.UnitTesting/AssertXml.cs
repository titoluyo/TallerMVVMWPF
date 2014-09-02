namespace BellaCode.UnitTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Xml.XPath;
    using System.Xml;
    using System.IO;

    /// <summary>
    /// Verifies XML content conditions in unit tests using true/false propositions
    /// </summary>
    public static class AssertXml
    {
        /// <summary>
        /// Determines if two XML content items are equivalent by comparison using a standarized formatting.
        /// </summary>
        /// <param name="expected">The expected XML.</param>
        /// <param name="actual">The actual XML.</param>
        /// <remarks>
        /// A rich descriptive comparison message included if the assertion fails.
        /// </remarks>
        public static void AreEqual(IXPathNavigable expected, IXPathNavigable actual)
        {
            string expectedXml = StandardizeXml(expected);
            string actualXml = StandardizeXml(actual);

            AreStandardizedXmlEqual(expectedXml, actualXml);
        }

        /// <summary>
        /// Determines if two XML content items are equivalent by comparison using a standarized formatting.
        /// </summary>
        /// <param name="expected">The expected XML.</param>
        /// <param name="actual">The actual XML.</param>
        /// <remarks>
        /// A rich descriptive comparison message included if the assertion fails.
        /// </remarks>
        public static void AreEqual(IXPathNavigable expected, string actual)
        {
            string expectedXml = StandardizeXml(expected);
            string actualXml = StandardizeXml(actual);

            AreStandardizedXmlEqual(expectedXml, actualXml);
        }

        /// <summary>
        /// Determines if two XML content items are equivalent by comparison using a standarized formatting.
        /// </summary>
        /// <param name="expected">The expected XML.</param>
        /// <param name="actual">The actual XML.</param>
        /// <remarks>
        /// A rich descriptive comparison message included if the assertion fails.
        /// </remarks>
        public static void AreEqual(string expected, IXPathNavigable actual)
        {
            string expectedXml = StandardizeXml(expected);
            string actualXml = StandardizeXml(actual);

            AreStandardizedXmlEqual(expectedXml, actualXml);
        }

        /// <summary>
        /// Determines if two XML content items are equivalent by comparison using a standarized formatting.
        /// </summary>
        /// <param name="expected">The expected XML.</param>
        /// <param name="actual">The actual XML.</param>
        /// <remarks>
        /// A rich descriptive comparison message included if the assertion fails.
        /// </remarks>
        public static void AreEqual(string expected, string actual)
        {
            string expectedXml = StandardizeXml(expected);
            string actualXml = StandardizeXml(actual);

            AreStandardizedXmlEqual(expectedXml, actualXml);
        }

        private static void AreStandardizedXmlEqual(string expected, string actual)
        {
            if (string.Compare(expected, actual, StringComparison.InvariantCulture) != 0)
            {
                string message = BuildDifferenceMessage(expected, actual);
                Assert.Fail(message);
            }
        }

        private static string StandardizeXml(IXPathNavigable navigable)
        {
            if (navigable == null)
            {
                return null;
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(navigable.CreateNavigator().OuterXml);

                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
                    ms.Flush();
                    ms.Position = 0;
                    document.Load(ms);
                }

                return document.CreateNavigator().OuterXml;
            }
            catch
            {
                return null;
            }
        }

        private static string StandardizeXml(string xml)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                return StandardizeXml(document.CreateNavigator());
            }
            catch
            {
                return null;
            }
        }

        private static string BuildDifferenceMessage(string expected, string actual)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int expectedLength = (expected != null) ? expected.Length : 0;
            int actualLength = (actual != null) ? actual.Length : 0;
            int maxLength = Math.Max(expectedLength, actualLength);

            for (int i = 0; i < maxLength; i++)
            {
                if (i == actualLength)
                {
                    stringBuilder.Append("Actual terminated prematurely at position ");
                    stringBuilder.Append(i);
                }
                else if (i == expectedLength)
                {
                    stringBuilder.Append("Expected terminated prematurely at position ");
                    stringBuilder.Append(i);
                }
                else if (actual[i] != expected[i])
                {
                    stringBuilder.Append("Actual deviates from expected at position ");
                    stringBuilder.Append(i);
                }
                else
                {
                    continue;
                }

                stringBuilder.Append("\r\n");
                stringBuilder.Append("\r\n");
                stringBuilder.Append("Expected:      ");
                if (!string.IsNullOrEmpty(expected))
                {
                    stringBuilder.Append(expected);
                }
                else
                {
                    stringBuilder.Append("<<null>>");
                }

                stringBuilder.Append("\r\n");
                stringBuilder.Append("\r\n");
                stringBuilder.Append("Actual:        ");
                if (!string.IsNullOrEmpty(actual))
                {
                    stringBuilder.Append(actual);
                }
                else
                {
                    stringBuilder.Append("<<null>>");
                }

                stringBuilder.Append("\r\n");
                stringBuilder.Append("\r\n");
                stringBuilder.Append("Partial Match: ");
                if (!string.IsNullOrEmpty(expected))
                {
                    stringBuilder.Append(expected.Substring(0, i));
                }
                else
                {
                    stringBuilder.Append("<<null>>");
                }

                return stringBuilder.ToString();
            }

            return "BuildDifferenceMessage() did not detect any differences.";
        }       
    }
}
