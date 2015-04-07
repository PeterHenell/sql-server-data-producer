
using System;
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.UniqueIdentifierGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class StaticGuidGeneratorTest
    {
        public StaticGuidGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var expected = Guid.NewGuid();

            var gen = new StaticGuidGenerator(new ColumnDataTypeDefinition("UniqueIdentifier", false));
            gen.GeneratorParameters.Value.Value = expected;
            var firstValue = gen.GenerateValue(1);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.EqualTo(expected));
        }
    }
}