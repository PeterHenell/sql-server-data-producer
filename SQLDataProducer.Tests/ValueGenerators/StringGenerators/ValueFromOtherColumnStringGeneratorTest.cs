
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.StringGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class ValueFromOtherColumnStringGeneratorTest
    {
        public ValueFromOtherColumnStringGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var otherColumn = new ColumnEntity();

            var gen = new ValueFromOtherColumnStringGenerator(new ColumnDataTypeDefinition("VarChar(123)", false));
            gen.GeneratorParameters.ValueFromOtherColumn.Value = otherColumn;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(otherColumn.ColumnIdentity));
        }

       
    }
}