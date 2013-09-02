using System.Collections;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.IEnumerableImplementationTests
{
    [TestFixture]
    public class NeuralLayerIEnumerableTests
    {
        [Test]
        public void ShouldGetEnumerator()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount) as IEnumerable;
            var enumerator = layer.GetEnumerator();
            Assert.IsNotNull(enumerator);
        }
    }
}
