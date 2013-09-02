using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SignalTests
{
    [TestFixture]
    public class SignalEqualsTests
    {
        [Test]
        public void SignalsShouldNotBeEqualWhenSecondIsNull()
        {
            var signal1 = new Signal();
            var result = signal1.Equals(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void SignalsShouldNotBeEqualForDifferentGuids()
        {
            var signal1 = new Signal();
            var signal2 = new Signal();
            var result = signal1.Equals(signal2);
            Assert.IsFalse(result);
        }

        [Test]
        public void SignalsShouldNotBeEqualForDifferentValues()
        {
            var signal1 = new Signal();
            var signal2 = new Signal { Identifier = signal1.Identifier, Value = 1 };
            var result = signal1.Equals(signal2);
            Assert.IsFalse(result);
        }

        [Test]
        public void SignalsShouldBeEqual()
        {
            var signal1 = new Signal();
            var signal2 = new Signal { Identifier = signal1.Identifier, Value = signal1.Value };
            var result = signal1.Equals(signal2);
            Assert.IsTrue(result);
        }
    }
}
