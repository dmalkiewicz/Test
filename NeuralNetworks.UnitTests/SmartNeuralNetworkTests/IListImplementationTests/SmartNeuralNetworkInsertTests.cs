using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.IListImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkInsertTests : SmartNeuralNetworkTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [Test]
        public void ShouldInsertLayerToAppropriatePositionAndNeuralNetworkHasToBeChanged()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var index = neuralNetwork.Count - 1;
            var layer1 = CreateNeuralLayer();
            neuralNetwork.Insert(index, layer1);
            var layer2 = neuralNetwork[index];
            Assert.AreEqual(layer1, layer2);
            Assert.AreEqual(LayersCount + 1, neuralNetwork.Count);
            Assert.IsTrue(neuralNetwork.IsChanged);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForToBigIndexWhileInsertingToIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, Convert.ToUInt32(LayersCount), InputSignalCount);
            const uint Index = LayersCount + 5;
            var layer1 = CreateNeuralLayer();
            neuralNetwork.Insert(Convert.ToInt32(Index), layer1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForMinusIndexWhileInsertingToIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, Convert.ToUInt32(LayersCount), InputSignalCount);
            const int Index = -10;
            var layer1 = CreateNeuralLayer();
            neuralNetwork.Insert(Index, layer1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotBePossibleToInsertSmartNeuronAsNullToLayer()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, Convert.ToUInt32(LayersCount), InputSignalCount);
            const int Index = 1;
            neuralNetwork.Insert(Index, null);
        }
    }
}
