﻿using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.ResponseStrategies;

using NUnit.Framework;

namespace NeuralNetworks.UnitTests.ResponseStrategiesTests
{
    [TestFixture]
    public class UnipolarResponseStrategiesTests
    {
        private const int NeuronInputCount = 2;

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
        public void UnipolarResponseWithDifferentInputSignalCountAndNullInputSignalsTest(double weight0, double weight1, double signal0, double signal1)
        {
            var response = new UnipolarResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response, null);

            var weights = new[] { weight0, weight1 };
            var inputSignals = new[]
            {
                signal0,
                signal1
            };
            originalNeuron.SetWeights(weights);

            var originalResult = originalNeuron.Response(inputSignals);
            var result = UnipolarResponse.Response(inputSignals, weights);
            Assert.AreEqual(originalResult, result);
        }
    }
}
