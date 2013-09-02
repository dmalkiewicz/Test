using System.Collections.Generic;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronOutputSignalTests
    {
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
        public void SmartNeuronSimpleOutputSignalTest(double weight0, double weight1, double signal0, double signal1)
        {
            var response = new SimpleResponse();
            this.VerifyNeuronOutputSignal(response, weight0, weight1, signal0, signal1);
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
        public void SmartNeuronSigmoidalOutputSignalTest(double weight0, double weight1, double signal0, double signal1)
        {
            var response = new SigmoidalResponse();
            this.VerifyNeuronOutputSignal(response, weight0, weight1, signal0, signal1);
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
        public void SmartNeuronTanhOutputSignalTest(double weight0, double weight1, double signal0, double signal1)
        {
            var response = new TanhResponse();
            this.VerifyNeuronOutputSignal(response, weight0, weight1, signal0, signal1);
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
        public void SmartNeuronUnipolarOutputSignalTest(double weight0, double weight1, double signal0, double signal1)
        {
            var response = new UnipolarResponse();
            this.VerifyNeuronOutputSignal(response, weight0, weight1, signal0, signal1);
        }

        private void VerifyNeuronOutputSignal(IResponse response, double weight0, double weight1, double signal0, double signal1)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var listOfSignals = new List<Signal>
                                    {
                                        new Signal { Value = signal0 },
                                        new Signal { Value = signal1 }
                                    };

            #region Prepare neurons

            var smartNeuron = new SmartNeuron(listOfSignals, response);
            smartNeuron.SetWeights(new[] { weight0, weight1 });

            #endregion

            var originalResult = smartNeuron.Response(signals);
            var result = smartNeuron.Response(signals);
            var outputSignal = smartNeuron.CalculateOutputSignal();

            Assert.AreEqual(originalResult, result);
            Assert.AreEqual(result, outputSignal.Value);
            Assert.AreEqual(smartNeuron.Identifier, outputSignal.Identifier);
        }
    }
}
