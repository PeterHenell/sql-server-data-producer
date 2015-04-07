
using System.Runtime.InteropServices;
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.UniqueIdentifierGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class RandomGuidGeneratorTest
    {
        public RandomGuidGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new RandomGuidGenerator(new ColumnDataTypeDefinition("UniqueIdentifier", false));
            var firstValue = gen.GenerateValue(1);
            var secondValue = gen.GenerateValue(2);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.Not.EqualTo(secondValue));
        }

      
    }
}