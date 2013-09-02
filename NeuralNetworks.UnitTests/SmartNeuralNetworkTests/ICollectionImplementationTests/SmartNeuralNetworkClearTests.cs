using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkClearTests : SmartNeuralNetworkTests
    {
        [Test]
        public void ShouldClearNeuralNetworkWithNeuronsAndLayersShouldBeChanged()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            Assert.IsFalse(neuralNetwork.IsChanged);
            neuralNetwork.Clear();
            Assert.AreEqual(0, neuralNetwork.Count);
            Assert.IsTrue(neuralNetwork.IsChanged);
        }
        
        [Test]
        public void ShouldClearNeuralNetworkWithoutNeuronsAndLayersShouldNotBeChanged()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0u, 0u, 0u);
            Assert.AreEqual(0, neuralNetwork.Count);
            Assert.IsFalse(neuralNetwork.IsChanged);
            neuralNetwork.Clear();
            Assert.AreEqual(0, neuralNetwork.Count);
            Assert.IsFalse(neuralNetwork.IsChanged);
        }
    }
}
