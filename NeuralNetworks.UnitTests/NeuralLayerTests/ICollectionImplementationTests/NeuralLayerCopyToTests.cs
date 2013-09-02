using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerCopyToTests
    {
        [TestCase(0)]
        [TestCase(10)]
        public void ShouldCopyNeuronsToArray(int arrayIncrementation)
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var array = new ISmartNeuron[layer.Count + arrayIncrementation];
            layer.CopyTo(array, 0);
            Assert.IsNotNull(array);
            Assert.AreEqual(layer.Count + arrayIncrementation, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                var smartNeuron1 = array[i];
                var smartNeuron2 = layer[i];
                if (smartNeuron1 != smartNeuron2)
                {
                    Assert.IsTrue(smartNeuron1.Equals(smartNeuron2));
                }
            }

            for (int i = layer.Count; i < array.Length; i++)
            {
                Assert.IsNull(array[i]);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionForArrayAsNull()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            layer.CopyTo(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentNullExceptionForToShortArray()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var array = new ISmartNeuron[layer.Count - 1];
            layer.CopyTo(array, 0);
        }
    }
}
