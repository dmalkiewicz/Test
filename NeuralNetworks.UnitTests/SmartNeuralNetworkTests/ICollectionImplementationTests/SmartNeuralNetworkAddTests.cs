using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkAddTests : SmartNeuralNetworkTests
    {
        [Test]
        public void ShouldAddAppropriateNeuralLayerToSmartNeuralNetwork()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            var layersCount = neuralNetwork.Count;
            Assert.IsFalse(neuralNetwork.IsChanged);
            neuralNetwork.Add(CreateNeuralLayer());
            Assert.IsTrue(neuralNetwork.IsChanged);
            Assert.AreEqual(layersCount + 1, neuralNetwork.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotAddNeuronAsNullToNeuralLayer()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            Assert.IsFalse(neuralNetwork.IsChanged);
            neuralNetwork.Add(null);
            Assert.IsFalse(neuralNetwork.IsChanged);
        }
    }
}
