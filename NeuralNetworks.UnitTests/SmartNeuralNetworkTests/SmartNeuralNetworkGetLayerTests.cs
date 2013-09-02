using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests
{
    [TestFixture]
    public class SmartNeuralNetworkGetLayerTests : SmartNeuralNetworkTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [Test]
        public void ShouldGetLayerBasedOnLayerGuid()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer1 = CreateNeuralLayer();
            neuralNetwork.Add(layer1);

            var layer2 = neuralNetwork.GetLayer(layer1.LayerGuid);
            Assert.AreEqual(layer1, layer2);
        }

        [Test]
        public void ShouldGetNullForNotExistingLayerGuid()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork.GetLayer(Guid.NewGuid());
            Assert.IsNull(layer);
        }
    }
}
