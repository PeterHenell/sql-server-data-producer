
using NUnit.Framework;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.Generators.StringGenerators;

namespace SQLDataProducer.Tests.ValueGenerators
{
    [TestFixture]
    [MSTest.TestClass]
    public class CitiesStringGeneratorTest
    {
        public CitiesStringGeneratorTest()
        {
        }

        [Test]
        [MSTest.TestMethod]
        public void ShouldGenerateValue()
        {
            var gen = new CitiesStringGenerator(new ColumnDataTypeDefinition("VarChar(123)", false));
            var firstValue = gen.GenerateValue(1);
            var secondValue = gen.GenerateValue(2);
            Assert.That(firstValue, Is.Not.Null);
            Assert.That(firstValue, Is.Not.EqualTo(secondValue));
        }


        [Test]
        [MSTest.TestMethod]
        public void ShouldStartOverWhenAllValuesBeenSeen()
        {
            var gen = new CitiesStringGenerator(new ColumnDataTypeDefinition("VarChar(123)", false));
            for (int n = 0; n < 20000; n++)
            {
                var firstValue = gen.GenerateValue(n);    
                Assert.That(firstValue, Is.Not.Null);
            }
        }
    }
}