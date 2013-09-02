using System;
using System.Collections.Generic;
using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.Enums;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronMemoryTraceStrengthTests
    {
        private const int NeuronInputCount = 2;

        #region Euclidean

        [TestCase(0, 0)]
        [TestCase(5, 4)]
        [TestCase(-1, -1)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon)]
        [TestCase(double.Epsilon, double.MaxValue)]
        [TestCase(double.MaxValue, double.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MinValue, double.MinValue)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void EuclideanMemoryTraceStrengthTest(double weight0, double weight1)
        {
            var response = new SimpleResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response);

            #region Prepare neurons

            var weights = new[] { weight0, weight1 };
            var inputSignals = new List<ISignal>
            {
                new Signal(),
                new Signal()
            };

            originalNeuron.SetWeights(new[] { weight0, weight1 });
            // TODO: originalNeuron.ResponseStrategy.SetNeuron(originalNeuron);
            var smartNeuron = new SmartNeuron(Guid.NewGuid(), weights, inputSignals, response);

            #endregion

            var originalResult = originalNeuron.MemoryTraceStrength(StrengthNorm.Euclidean);
            var result = smartNeuron.MemoryTraceStrength(StrengthNorm.Euclidean);

            Assert.AreEqual(originalResult, result);
        }

        #endregion

        #region Manhattan

        [TestCase(0, 0)]
        [TestCase(5, 4)]
        [TestCase(-1, -1)]
        [TestCase(double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon)]
        [TestCase(double.Epsilon, double.MaxValue)]
        [TestCase(double.MaxValue, double.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue)]
        [TestCase(double.MinValue, double.MinValue)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        public void ManhattanMemoryTraceStrengthTest(double weight0, double weight1)
        {
            var response = new SimpleResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response);

            #region Prepare neurons

            var weights = new[] { weight0, weight1 };
            var inputSignals = new List<ISignal>
            {
                new Signal(),
                new Signal()
            };

            originalNeuron.SetWeights(new[] { weight0, weight1 });

            var smartNeuron = new SmartNeuron(Guid.NewGuid(), weights, inputSignals, response);

            #endregion

            var originalResult = originalNeuron.MemoryTraceStrength(StrengthNorm.Manhattan);
            var result = smartNeuron.MemoryTraceStrength(StrengthNorm.Manhattan);

            Assert.AreEqual(originalResult, result);
        }

        #endregion

        [Test]
        public void StrengthWithNullInputSignalsTest()
        {
            Assert.Throws<ArgumentNullException>(() => SmartNeuron.Strength(null, StrengthNorm.Euclidean));
        }
    }
}
