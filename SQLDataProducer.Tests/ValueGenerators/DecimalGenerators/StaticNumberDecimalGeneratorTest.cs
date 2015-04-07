
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DecimalGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class StaticNumberDecimalGeneratorTest
    {
        public StaticNumberDecimalGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new StaticNumberDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            gen.GeneratorParameters.Value.Value = 222m;
            var firstValue = (decimal)gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(222m));
        }
    }
}