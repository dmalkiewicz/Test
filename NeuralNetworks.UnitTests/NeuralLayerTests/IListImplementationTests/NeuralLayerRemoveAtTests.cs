using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.IListImplementationTests
{
    [TestFixture]
    public class NeuralLayerRemoveAtTests
    {
        [Test]
        public void ShouldRemoveNeuronAtAppropriatePositionAndLayerHasToBeChanged()
        {
            var neuronsCount = 5u;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var index = layer.Count - 1;
            var smartNeuron1 = layer[index];
            layer.RemoveAt(index);
            Assert.AreEqual(neuronsCount - 1, layer.Count);
            Assert.IsTrue(layer.IsChanged);
            Assert.IsNull(layer.GetNeuron(smartNeuron1.Identifier));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowExceptionForToBigIndexWhileRemoveAtIndex()
        {
            var neuronsCount = 5;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(Convert.ToUInt32(neuronsCount), inputCount);
            int index = neuronsCount + 5;
            layer.RemoveAt(index);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForMinusIndexWhileRemoveAtIndex()
        {
            var neuronsCount = 5;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(Convert.ToUInt32(neuronsCount), inputCount);
            int index = -10;
            layer.RemoveAt(index);
        }
    }
}
