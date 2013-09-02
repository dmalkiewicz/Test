using System.Collections;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.IEnumerableImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkIEnumerableTests
    {
        [Test]
        public void ShouldGetEnumerator()
        {
            const uint LayersCount = 3u;
            const uint NeuronsCount = 2u;
            const uint InputSignalCount = 4u;
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount) as IEnumerable;
            var enumerator = neuralNetwork.GetEnumerator();
            Assert.IsNotNull(enumerator);
        }
    }
}
