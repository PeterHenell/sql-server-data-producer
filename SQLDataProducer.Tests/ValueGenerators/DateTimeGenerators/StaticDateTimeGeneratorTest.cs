
using System;
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.DateTimeGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class StaticDateTimeGeneratorTest
    {
        public StaticDateTimeGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new StaticDateTimeGenerator(new ColumnDataTypeDefinition("DateTime2(2)", false));
            var firstValue = (DateTime)gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(gen.GeneratorParameters.Date.Value));
        }
    }
}