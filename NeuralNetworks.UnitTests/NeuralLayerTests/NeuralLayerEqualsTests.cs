using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests
{
    [TestFixture]
    public class NeuralLayerEqualsTests
    {
        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneIsNull()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsFalse(layer.Equals(null));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneIsSomethingElseThanNeuralLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer1 = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var layer2 = "something else";
            Assert.IsFalse(layer1.Equals(layer2));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneGuidIsDifferent()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer1 = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var layer2 = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsFalse(layer1.Equals(layer2));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenSecondOneHasDifferentNeuronsCount()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer1 = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var layer2 = new NeuralLayer<ISmartNeuron>(neuronsCount + 1, inputCount)
            {
                LayerGuid = layer1.LayerGuid
            };

            Assert.IsFalse(layer1.Equals(layer2));
        }

        [Test]
        public void NeuralLayersShouldNotBeEqualWhenAnyNeuronIsDifferent()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer1 = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var layer2 = new NeuralLayer<ISmartNeuron>(0, inputCount)
            {
                LayerGuid = layer1.LayerGuid
            };

            foreach (var neuron in layer1)
            {
                layer2.Add(neuron);
            }

            layer2[0] = new SmartNeuron(inputCount);

            Assert.IsFalse(layer1.Equals(layer2));
        }

        [Test]
        public void NeuralLayersShouldBeEqual()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer1 = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var layer2 = new NeuralLayer<ISmartNeuron>(0, inputCount)
            {
                LayerGuid = layer1.LayerGuid
            };

            foreach (var neuron in layer1)
            {
                layer2.Add(neuron);
            }

            Assert.IsTrue(layer1.Equals(layer2));
        }

        [Test]
        public void ShouldHaveAppropriateHashCode()
        {
            var layer = new NeuralLayer<ISmartNeuron>(0u, 0u);
            var hashCode = layer.GetHashCode();
            Assert.AreNotEqual(0, hashCode);
            Assert.AreNotEqual(int.MinValue, hashCode);
            Assert.AreNotEqual(int.MaxValue, hashCode);
        }
    }
}
