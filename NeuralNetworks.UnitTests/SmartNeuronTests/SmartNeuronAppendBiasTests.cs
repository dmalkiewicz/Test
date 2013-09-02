using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronAppendBiasTests
    {
        private const uint InputCount = 2u;
         
        [Test]
        public void ShouldAppendBiasWithCreatingNewRandomWeight()
        {
            var smartNeuron = new SmartNeuron(InputCount);
            Assert.AreEqual(InputCount, smartNeuron.InputSignals.Count);
            Assert.AreEqual(InputCount, smartNeuron.InputSignalsWithWeights.Count);
            smartNeuron.AppendBias(true);
            Assert.AreEqual(InputCount + 1, smartNeuron.InputSignalsWithWeights.Count);
            Assert.IsFalse(smartNeuron.InputSignalsWithWeights.ContainsValue(0));
        }

        [Test]
        public void ShouldRemoveBias()
        {
            var smartNeuron = new SmartNeuron(InputCount);
            Assert.AreEqual(InputCount, smartNeuron.InputSignals.Count);
            Assert.AreEqual(InputCount, smartNeuron.InputSignalsWithWeights.Count);
            var inputSignalsWithWeights = smartNeuron.InputSignalsWithWeights.Count;
            smartNeuron.AppendBias(true);
            smartNeuron.AppendBias(false);
            Assert.AreEqual(InputCount, smartNeuron.InputSignalsWithWeights.Count);
            Assert.AreEqual(inputSignalsWithWeights, smartNeuron.InputSignalsWithWeights.Count);
            Assert.IsFalse(smartNeuron.InputSignalsWithWeights.ContainsValue(0));
        }

        [Test]
        public void ShouldDoNothindWhenBiasIsSetTwiceToTrue()
        {
            var smartNeuron = new SmartNeuron(InputCount);
            Assert.AreEqual(InputCount, smartNeuron.InputSignals.Count);
            Assert.AreEqual(InputCount, smartNeuron.InputSignalsWithWeights.Count);
            var inputSignalsWithWeights = smartNeuron.InputSignalsWithWeights.Count;
            smartNeuron.AppendBias(true);
            smartNeuron.AppendBias(true);
            Assert.AreEqual(InputCount + 1, smartNeuron.InputSignalsWithWeights.Count);
            Assert.AreEqual(inputSignalsWithWeights + 1, smartNeuron.InputSignalsWithWeights.Count);
            Assert.IsFalse(smartNeuron.InputSignalsWithWeights.ContainsValue(0));
        }

        [Test]
        public void ShouldDoNothindWhenBiasIsSetTwiceToFalse()
        {
            var smartNeuron = new SmartNeuron(InputCount);
            Assert.AreEqual(InputCount, smartNeuron.InputSignals.Count);
            Assert.AreEqual(InputCount, smartNeuron.InputSignalsWithWeights.Count);
            var inputSignalsWithWeights = smartNeuron.InputSignalsWithWeights.Count;
            smartNeuron.AppendBias(false);
            smartNeuron.AppendBias(false);
            Assert.AreEqual(InputCount, smartNeuron.InputSignalsWithWeights.Count);
            Assert.AreEqual(inputSignalsWithWeights, smartNeuron.InputSignalsWithWeights.Count);
            Assert.IsFalse(smartNeuron.InputSignalsWithWeights.ContainsValue(0));
        }
    }
}
