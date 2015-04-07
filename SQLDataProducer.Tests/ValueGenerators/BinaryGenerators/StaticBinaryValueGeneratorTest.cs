
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.BinaryGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class StaticBinaryValueGeneratorTest
    {
        public StaticBinaryValueGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var expectedValue = "0x0010";

            var gen = new StaticBinaryValueGenerator(new ColumnDataTypeDefinition("Binary(123)", false));
            gen.GeneratorParameters.Value.Value = expectedValue;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(expectedValue));
        }
    }
}