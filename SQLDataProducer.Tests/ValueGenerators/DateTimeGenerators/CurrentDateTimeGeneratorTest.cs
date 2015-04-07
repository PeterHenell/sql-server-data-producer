
using System;
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DateTimeGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class CurrentDateTimeGeneratorTest
    {
        public CurrentDateTimeGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new CurrentDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = (DateTime)gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldOffsetValueUsingParameters()
        {
            var gen = new CurrentDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            gen.GeneratorParameters.ShiftDays.Value = 1;
            gen.GeneratorParameters.ShiftHours.Value = 1;
            gen.GeneratorParameters.ShiftMinutes.Value = 10;
            gen.GeneratorParameters.ShiftSeconds.Value = 10;
            gen.GeneratorParameters.ShiftMilliseconds.Value = 20;
            
            var firstValue = (DateTime)gen.GenerateValue(1);
            Assert.That(firstValue, Is.GreaterThan(DateTime.Now));

        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldOffsetNegativeValueUsingParameters()
        {
            var gen = new CurrentDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            gen.GeneratorParameters.ShiftDays.Value = -1;
            gen.GeneratorParameters.ShiftHours.Value = -1;
            gen.GeneratorParameters.ShiftMinutes.Value = -10;
            gen.GeneratorParameters.ShiftSeconds.Value = -10;
            gen.GeneratorParameters.ShiftMilliseconds.Value = -20;

            var firstValue = (DateTime)gen.GenerateValue(1);
            Assert.That(firstValue, Is.LessThan(DateTime.Now));

        }


    }
}