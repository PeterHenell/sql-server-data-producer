
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DecimalGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class RandomNumberDecimalGeneratorTest
    {
        public RandomNumberDecimalGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new RandomNumberDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            gen.GeneratorParameters.MinValue.Value = - 20m;
            gen.GeneratorParameters.MaxValue.Value = 20m;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.GreaterThan(-20m));
            Assert.That(firstValue, Is.LessThan(20m));
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateNegativeValues()
        {
            var gen = new RandomNumberDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            gen.GeneratorParameters.MinValue.Value = -20m;
            gen.GeneratorParameters.MaxValue.Value = 0m;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.GreaterThan(-20m));
            Assert.That(firstValue, Is.LessThan(0m));
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestStep()
        {
            var gen = new RandomNumberDecimalGenerator(new ColumnDataTypeDefinition("Decimal(12)", false));
            var firstValue = gen.GenerateValue(1);
            var secondValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.EqualTo(secondValue));
        }
      
    }
}