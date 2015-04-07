
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.XMLGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class StaticXmlGeneratorTest
    {
        public StaticXmlGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var expectedXML = "<generator does not really parse xml>";

            var gen = new StaticXmlGenerator(new ColumnDataTypeDefinition("Xml", false));
            gen.GeneratorParameters.Value.Value = expectedXML;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(expectedXML));
        }
    }
}