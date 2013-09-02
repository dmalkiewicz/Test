using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests
{
    [TestFixture]
    public class SmartNeuralNetworkEqualsTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneIsNull()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            Assert.IsFalse(neuralNetwork.Equals(null));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneIsSomethingElseThanNeuralLayer()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            const string Sth = "something else";
            Assert.IsFalse(neuralNetwork.Equals(Sth));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneGuidIsDifferent()
        {
            var neuralNetwork1 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var neuralNetwork2 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            Assert.IsFalse(neuralNetwork1.Equals(neuralNetwork2));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneHasDifferentNeuronsCount()
        {
            var neuralNetwork1 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var neuralNetwork2 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount + 1, LayersCount, InputSignalCount)
                {
                    NeuralNetworkGuid = neuralNetwork1.NeuralNetworkGuid
                };

            Assert.IsFalse(neuralNetwork1.Equals(neuralNetwork2));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneHasDifferentLayersCount()
        {
            var neuralNetwork1 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var neuralNetwork2 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount + 1, InputSignalCount)
            {
                NeuralNetworkGuid = neuralNetwork1.NeuralNetworkGuid
            };

            Assert.IsFalse(neuralNetwork1.Equals(neuralNetwork2));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenAnyNeuronIsDifferent()
        {
            var neuralNetwork1 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var neuralNetwork2 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0, 0, InputSignalCount)
            {
                NeuralNetworkGuid = neuralNetwork1.NeuralNetworkGuid
            };

            foreach (var layer in neuralNetwork1)
            {
                neuralNetwork2.Add(layer);
            }

            neuralNetwork2[0] = new NeuralLayer<ISmartNeuron>(NeuronsCount, InputSignalCount);

            Assert.IsFalse(neuralNetwork1.Equals(neuralNetwork2));
        }

        [Test]
        public void NeuralLayersShouldBeEqual()
        {
            var neuralNetwork1 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var neuralNetwork2 = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0, 0, InputSignalCount)
            {
                NeuralNetworkGuid = neuralNetwork1.NeuralNetworkGuid
            };

            foreach (var layer in neuralNetwork1)
            {
                neuralNetwork2.Add(layer);
            }

            Assert.IsTrue(neuralNetwork1.Equals(neuralNetwork2));
        }

        [Test]
        public void ShouldHaveAppropriateHashCode()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var hashCode = neuralNetwork.GetHashCode();
            Assert.AreNotEqual(0, hashCode);
            Assert.AreNotEqual(int.MinValue, hashCode);
            Assert.AreNotEqual(int.MaxValue, hashCode);
        }
    }
}
