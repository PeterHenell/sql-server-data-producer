
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DecimalGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class CountingUpDecimalGeneratorTest
    {
        public CountingUpDecimalGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new CountingUpDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestStep()
        {
            var gen = new CountingUpDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            gen.GeneratorParameters.Step.Value = 1;
            gen.GeneratorParameters.StartValue.Value = 1;

            var firstValue = gen.GenerateValue(1);
            var secondValue = gen.GenerateValue(2);
            Assert.That(firstValue, Is.EqualTo(1m));
            Assert.That(secondValue, Is.EqualTo(2m));
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldStepDownwards()
        {
            var gen = new CountingUpDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            gen.GeneratorParameters.StartValue.Value = 1;
            gen.GeneratorParameters.Step.Value = -1;

            var firstValue = gen.GenerateValue(1);
            var secondValue = gen.GenerateValue(2);
            var thirdValue = gen.GenerateValue(3);
            Assert.That(firstValue, Is.EqualTo(1m));
            Assert.That(secondValue, Is.EqualTo(0m));
            Assert.That(thirdValue, Is.EqualTo(-1m));
        }
    }
}