
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DateTimeGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class ValueFromOtherColumnDateTimeGeneratorTest
    {
        public ValueFromOtherColumnDateTimeGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var otherColumn = new ColumnEntity();

            var gen = new ValueFromOtherColumnDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            gen.GeneratorParameters.ValueFromOtherColumn.Value = otherColumn;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(otherColumn.ColumnIdentity));
        }
    }
}