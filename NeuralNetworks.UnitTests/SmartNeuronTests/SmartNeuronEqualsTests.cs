using System.Collections.Generic;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronEqualsTests
    {
        private const int NeuronInputCount = 2;

        [Test]
        public void NeuronsShouldNotBeEqualWhenSecondIsNull()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var result = smartNeuron1.Equals(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualForDifferentGuids()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualForDifferentInputCount()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount + 1, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier
                                   };
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualWhenSecondNeuronInputSignalsIsNull()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier, InputSignals = null
                                   };
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualWhenFirstNeuronInputSignalsIsNull()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier
                                   };
            smartNeuron1.InputSignals = null;
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualWhenSomeInputSignalIsDifferent()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier
                                   };
            // smartNeuron1.InputSignals[0].Value = 5; // use the reflection to assign the value
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualForDifferentWeightsCount()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier,
                                       InputSignals = smartNeuron1.InputSignals
                                   };
            smartNeuron2.SetWeights(new double[] { 5 });
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualWhenSecondNeuronWeightsIsNull()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier,
                                       InputSignals = smartNeuron1.InputSignals
                                   };
            smartNeuron2.SetWeights(null);
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }

        [Test]
        public void NeuronsShouldNotBeEqualWhenSomeWeightIsDifferent()
        {
            var smartNeuron1 = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            var smartNeuron2 = new SmartNeuron(NeuronInputCount, new SimpleResponse())
                                   {
                                       Identifier = smartNeuron1.Identifier,
                                       InputSignals = smartNeuron1.InputSignals
                                   };
            var newWeights = new List<double>(smartNeuron1.Weights).ToArray();
            newWeights[0] = 5;
            smartNeuron2.SetWeights(newWeights);
            var result = smartNeuron1.Equals(smartNeuron2);
            Assert.IsFalse(result);
        }
    }
}
