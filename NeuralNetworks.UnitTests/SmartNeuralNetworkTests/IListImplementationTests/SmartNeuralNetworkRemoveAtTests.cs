using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.IListImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkRemoveAtTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [Test]
        public void ShouldRemoveLayerAtAppropriatePositionAndNeuralNetworkHasToBeChanged()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var index = neuralNetwork.Count - 1;
            var layer1 = neuralNetwork[index];
            neuralNetwork.RemoveAt(index);
            Assert.AreEqual(LayersCount - 1, neuralNetwork.Count);
            Assert.IsTrue(neuralNetwork.IsChanged);
            Assert.IsNull(neuralNetwork.GetLayer(layer1.LayerGuid));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowExceptionForToBigIndexWhileRemoveAtIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, Convert.ToUInt32(LayersCount), InputSignalCount);
            var index = Convert.ToInt32(LayersCount) + 5;
            neuralNetwork.RemoveAt(index);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForMinusIndexWhileRemoveAtIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, Convert.ToUInt32(LayersCount), InputSignalCount);
            const int Index = -10;
            neuralNetwork.RemoveAt(Index);
        }
    }
}
