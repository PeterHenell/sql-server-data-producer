
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.IntGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class ValueFromOtherColumnIntGeneratorTest
    {
        public ValueFromOtherColumnIntGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var otherColumn = new ColumnEntity();

            var gen = new ValueFromOtherColumnIntGenerator(new ColumnDataTypeDefinition("TinyInt", false));
            gen.GeneratorParameters.ValueFromOtherColumn.Value = otherColumn;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(otherColumn.ColumnIdentity));
        }
    }
}