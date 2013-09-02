using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkCopyToTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [TestCase(0)]
        [TestCase(10)]
        public void ShouldCopyLayersToArray(int arrayIncrementation)
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var array = new INeuralLayer<ISmartNeuron>[neuralNetwork.Count + arrayIncrementation];
            neuralNetwork.CopyTo(array, 0);
            Assert.IsNotNull(array);
            Assert.AreEqual(neuralNetwork.Count + arrayIncrementation, array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                var layer1 = array[i];
                var layer2 = neuralNetwork[i];
                if (layer1 != layer2)
                {
                    Assert.IsTrue(layer1.Equals(layer2));
                }
            }

            for (var i = neuralNetwork.Count; i < array.Length; i++)
            {
                Assert.IsNull(array[i]);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionForArrayAsNull()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            neuralNetwork.CopyTo(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentNullExceptionForToShortArray()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var array = new INeuralLayer<ISmartNeuron>[neuralNetwork.Count - 1];
            neuralNetwork.CopyTo(array, 0);
        }
    }
}
