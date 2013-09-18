using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronResponseTests
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
        public void SmartNeuronSimpleResponseTest(double weight0, double weight1, double signal0, double signal1)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var response = new SimpleResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response, null);

            #region Prepare neurons

            originalNeuron.SetWeights(new[] { weight0, weight1 });

            var smartNeuron = new SmartNeuron(NeuronInputCount, response);
            smartNeuron.SetWeights(new[] { weight0, weight1 });

            #endregion

            var originalResult = originalNeuron.Response(signals);
            var result = smartNeuron.Response(signals);

            Assert.AreEqual(originalResult, result);
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
        public void SmartNeuronSigmoidalResponseTest(double weight0, double weight1, double signal0, double signal1)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var response = new SigmoidalResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response, null);

            #region Prepare neurons

            originalNeuron.SetWeights(new[] { weight0, weight1 });

            var smartNeuron = new SmartNeuron(NeuronInputCount, response);
            smartNeuron.SetWeights(new[] { weight0, weight1 });

            #endregion

            var originalResult = originalNeuron.Response(signals);
            var result = smartNeuron.Response(signals);

            Assert.AreEqual(originalResult, result);
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
        public void SmartNeuronTanhResponseTest(double weight0, double weight1, double signal0, double signal1)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var response = new TanhResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response, null);

            #region Prepare neurons

            originalNeuron.SetWeights(new[] { weight0, weight1 });

            var smartNeuron = new SmartNeuron(NeuronInputCount, response);
            smartNeuron.SetWeights(new[] { weight0, weight1 });

            #endregion

            var originalResult = originalNeuron.Response(signals);
            var result = smartNeuron.Response(signals);

            Assert.AreEqual(originalResult, result);
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
        public void SmartNeuronUnipolarResponseTest(double weight0, double weight1, double signal0, double signal1)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var response = new UnipolarResponse();
            var originalNeuron = new SimpleNeuron(NeuronInputCount, response, null);

            #region Prepare neurons

            originalNeuron.SetWeights(new[] { weight0, weight1 });

            var smartNeuron = new SmartNeuron(NeuronInputCount, response);
            smartNeuron.SetWeights(new[] { weight0, weight1 });

            #endregion

            var originalResult = originalNeuron.Response(signals);
            var result = smartNeuron.Response(signals);

            Assert.AreEqual(originalResult, result);
        }
    }
}
