using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkContainsTests : SmartNeuralNetworkTests
    {
        [Test]
        public void ShouldReturnTrueForLayerThatExistsInNetwork()
        {
            var network = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            var layer = network[0];
            var result = network.Contains(layer);
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseForLayerThatNotExistsInNetwork()
        {
            var network = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            var layer = CreateNeuralLayer();
            var result = network.Contains(layer);
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnFalseForLayerAsNull()
        {
            var network = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            var result = network.Contains(null);
            Assert.IsFalse(result);
        }
    }
}
