using NeuralNetwork.Helpers;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.HelpersTests
{
    [TestFixture]
    public class PropertyHelperTest
    {
        [Test]
        public void ShouldGetApropriatePropertyName()
        {
            const string ExpectedResult = "Property1";
            var result = PropertyHelper.GetName<TestClass>(p => p.Property1);
            Assert.AreEqual(ExpectedResult, result);
        }

        private abstract class TestClass
        {
            protected TestClass(string property1)
            {
                this.Property1 = property1;
            }

            public string Property1 { get; private set; }
        }
    }
}
