using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkIsRemoveTests : SmartNeuralNetworkTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [Test]
        public void ShouldRemoveExistingLayerFromNeuralNetwork()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork[0];

            Assert.IsFalse(neuralNetwork.IsChanged);
            var result = neuralNetwork.Remove(layer);
            Assert.IsTrue(neuralNetwork.IsChanged);
            Assert.IsTrue(result);
            Assert.AreEqual(LayersCount - 1, neuralNetwork.Count);
            Assert.IsNull(neuralNetwork.GetLayer(layer.LayerGuid));
        }

        [Test]
        public void ShouldDoNothingForNotExistingLayerWhileRemovingFromNeuralNetwork()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = CreateNeuralLayer();

            Assert.IsFalse(neuralNetwork.IsChanged);
            var result = neuralNetwork.Remove(layer);
            Assert.IsFalse(neuralNetwork.IsChanged);
            Assert.IsFalse(result);
            Assert.AreEqual(LayersCount, neuralNetwork.Count);
            Assert.IsNull(neuralNetwork.GetLayer(layer.LayerGuid));
        }

        [Test]
        public void ShouldDoNothingForLayerAsNullWhileRemovingFromNeuralNetwork()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);

            Assert.IsFalse(neuralNetwork.IsChanged);
            var result = neuralNetwork.Remove(null);
            Assert.IsFalse(neuralNetwork.IsChanged);
            Assert.IsFalse(result);
            Assert.AreEqual(LayersCount, neuralNetwork.Count);
            Assert.IsFalse(neuralNetwork.Contains(null));
        }
    }
}
