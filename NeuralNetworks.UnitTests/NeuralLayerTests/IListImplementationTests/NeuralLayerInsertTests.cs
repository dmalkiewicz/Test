using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.IListImplementationTests
{
    [TestFixture]
    public class NeuralLayerInsertTests
    {
        [Test]
        public void ShouldInsertNeuronToAppropriatePositionAndLayerHasToBeChanged()
        {
            var neuronsCount = 5u;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var index = layer.Count - 1;
            var smartNeuron1 = new SmartNeuron(inputCount, new SimpleResponse());
            layer.Insert(index, smartNeuron1);
            var smartNeuron2 = layer[index];
            Assert.AreEqual(smartNeuron1, smartNeuron2);
            Assert.AreEqual(neuronsCount + 1, layer.Count);
            Assert.IsTrue(layer.IsChanged);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForToBigIndexWhileInsertingToIndex()
        {
            var neuronsCount = 5;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(Convert.ToUInt32(neuronsCount), inputCount);
            int index = neuronsCount + 5;
            var smartNeuron1 = new SmartNeuron(inputCount, new SimpleResponse());
            layer.Insert(index, smartNeuron1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowArgumentOutOfRangeExceptionForMinusIndexWhileInsertingToIndex()
        {
            var neuronsCount = 5;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(Convert.ToUInt32(neuronsCount), inputCount);
            int index = -10;
            var smartNeuron1 = new SmartNeuron(inputCount, new SimpleResponse());
            layer.Insert(index, smartNeuron1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotBePossibleToInsertSmartNeuronAsNullToLayer()
        {
            var neuronsCount = 5;
            var inputCount = 5u;
            var layer = new NeuralLayer<ISmartNeuron>(Convert.ToUInt32(neuronsCount), inputCount);
            int index = 1;
            layer.Insert(index, null);
        }
    }
}
