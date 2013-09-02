using System;
using System.Linq;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests
{
    [TestFixture]
    public class SmartNeuralNetworkIteratorTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        #region Get

        [Test]
        public void ShouldReturnNullForMinusIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork[-1];
            Assert.IsNull(layer);
        }

        [Test]
        public void ShouldReturnNullForTooBigIndexIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork[100];
            Assert.IsNull(layer);
        }

        [Test]
        public void ShouldReturnAppropriateNeuralLayerForExistingIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork[0];
            Assert.IsNotNull(layer);
        }

        #endregion

        #region Set

        [Test]
        public void ShouldSetAppropriateNeuralLayerForExistingIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork.First();
            Assert.IsNotNull(layer);

            neuralNetwork[1] = layer;
            Assert.IsTrue(neuralNetwork[0].Equals(neuralNetwork[1]));
            Assert.IsFalse(neuralNetwork.Contains(null));
        }

        [Test]
        public void ShouldNotSetNullForExistingIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork.First();
            Assert.IsNotNull(layer);

            neuralNetwork[1] = null;
            Assert.IsFalse(neuralNetwork[0].Equals(neuralNetwork[1]));
            Assert.IsFalse(neuralNetwork.Contains(null));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForAppropriateNeuralLayerAndTooBigIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork.First();
            Assert.IsNotNull(layer);

            neuralNetwork[10] = layer;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForAppropriateNeuralLayerAndMinusIndex()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = neuralNetwork.First();
            Assert.IsNotNull(layer);

            neuralNetwork[-1] = layer;
        }

        #endregion
    }
}
