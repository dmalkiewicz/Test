using System;
using System.Linq;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests
{
    [TestFixture]
    public class NeuralLayerIteratorTests
    {
        private const uint InputCount = 2u;

        private const uint NeuronsCount = 2u;
        
        #region Get

        [Test]
        public void ShouldReturnNullForMinusIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer[-1];
            Assert.IsNull(smartNeuron);
        }

        [Test]
        public void ShouldReturnNullForTooBigIndexIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer[100];
            Assert.IsNull(smartNeuron);
        }

        [Test]
        public void ShouldReturnAppropriateNeuronForExistingIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer[0];
            Assert.IsNotNull(smartNeuron);
        }

        #endregion

        #region Set

        [Test]
        public void ShouldSetAppropriateNeuronForExistingIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer.First();
            Assert.IsNotNull(smartNeuron);

            layer[1] = smartNeuron;
            Assert.IsTrue(layer[0].Equals(layer[1]));
            Assert.IsFalse(layer.Contains(null));
        }

        [Test]
        public void ShouldNotSetNullForExistingIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer.First();
            Assert.IsNotNull(smartNeuron);

            layer[1] = null;
            Assert.IsFalse(layer[0].Equals(layer[1]));
            Assert.IsFalse(layer.Contains(null));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForAppropriateNeuronAndTooBigIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer.First();
            Assert.IsNotNull(smartNeuron);

            layer[10] = smartNeuron;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForAppropriateNeuronAndMinusIndex()
        {
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
            var smartNeuron = layer.First();
            Assert.IsNotNull(smartNeuron);

            layer[-1] = smartNeuron;
        }

        #endregion
    }
}
