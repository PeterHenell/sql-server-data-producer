
using System;
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DateTimeGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class SecondsSeriesDateTimeGeneratorTest
    {
        public SecondsSeriesDateTimeGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new SecondsSeriesDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = (DateTime)gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestStep()
        {
            var gen = new SecondsSeriesDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = (DateTime)gen.GenerateValue(1);
            var secondValue = (DateTime)gen.GenerateValue(2);
            Assert.That(firstValue, Is.LessThan(secondValue));
            Assert.That(firstValue + TimeSpan.FromSeconds(1), Is.EqualTo(secondValue));
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestStartValue()
        {
            var gen = new SecondsSeriesDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = (DateTime)gen.GenerateValue(1);
            var repeatedFirstValue = (DateTime)gen.GenerateValue(1);
            Assert.That(firstValue, Is.EqualTo(repeatedFirstValue));
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestOffsets()
        {
            var gen = new SecondsSeriesDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var gen2 = new SecondsSeriesDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));

            gen.GeneratorParameters.ShiftDays.Value = 1;
            gen.GeneratorParameters.ShiftHours.Value = 1;
            gen.GeneratorParameters.ShiftMinutes.Value = 1;
            gen.GeneratorParameters.ShiftSeconds.Value = 1;
            gen.GeneratorParameters.ShiftMilliseconds.Value = 1;

            var valueFromOffsetGenerator = (DateTime)gen.GenerateValue(1);
            var valueFromNoOffset = (DateTime)gen2.GenerateValue(1);

            Assert.That(valueFromOffsetGenerator, Is.GreaterThan(valueFromNoOffset));
        }
    }
}