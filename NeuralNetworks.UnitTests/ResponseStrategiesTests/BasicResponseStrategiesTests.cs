using System;
using System.Collections.Generic;
using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.ResponseStrategiesTests
{
    [TestFixture]
    public class BasicResponseStrategiesTests
    {
        private const int NeuronInputCount = 2;

        [Test]
        public void BasicResponseWithDifferentInputSignalCountAndNullInputSignalsTest()
        {
            var response = new SimpleResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response);

            #region Prepare neurons

            var weights = new[] { 0.0, 0.0 };
            var inputSignals = new List<ISignal> { new Signal(), new Signal { Identifier = Guid.NewGuid() } };

            originalNeuron.SetWeights(weights);
            // TODO: originalNeuron.ResponseStrategy.SetNeuron(originalNeuron);
            var smartNeuron = new SmartNeuron(Guid.NewGuid(), weights, inputSignals, response);

            #endregion

            Assert.Throws<ArgumentException>(() => smartNeuron.Response(new double[0]));
            Assert.Throws<ArgumentException>(() => smartNeuron.Response(null));
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(5, 4, 5, 4)]
        [TestCase(-1, -1, -1, -1)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon)]
        [TestCase(double.Epsilon, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.Epsilon, double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue, double.Epsilon, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void BasicResponseWithDifferentInputSignalCountAndNullInputSignalsTest(double weight0, double weight1, double signal0, double signal1)
        {
            var response = new SimpleResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response);

            var weights = new[] { weight0, weight1 };
            var inputSignals = new[]
            {
                signal0,
                signal1
            };
            originalNeuron.SetWeights(weights);

            var originalResult = originalNeuron.Response(inputSignals);
            var result = BasicResponse.Response(inputSignals, weights);
            Assert.AreEqual(originalResult, result);
        }
    }
}
