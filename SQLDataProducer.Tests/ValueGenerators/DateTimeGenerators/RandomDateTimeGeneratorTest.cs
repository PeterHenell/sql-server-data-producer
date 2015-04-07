
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DateTimeGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class RandomDateTimeGeneratorTest
    {
        public RandomDateTimeGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new RandomDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestStep()
        {
            var gen = new RandomDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = gen.GenerateValue(1);
            var secondValue = gen.GenerateValue(2);
            Assert.That(firstValue, Is.Not.EqualTo(secondValue));
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldTestMinAndMaxLimits()
        {
            var gen = new RandomDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var minDate = new DateTime(2010, 4, 1);
            var maxDate = new DateTime(2015, 12, 31);
            
            gen.GeneratorParameters.MinDate.Value = minDate;
            gen.GeneratorParameters.MaxDate.Value = maxDate;

            // Strange test since it is random
            for (long n = 0; n < 100; n++)
            {
                var val = gen.GenerateValue(n);
                Assert.That(val, Is.GreaterThan(minDate));
                Assert.That(val, Is.LessThan(maxDate));
            }
        }
    }
}