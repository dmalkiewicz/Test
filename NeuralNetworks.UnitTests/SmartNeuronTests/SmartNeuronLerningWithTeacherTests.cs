using System;
using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronLerningWithTeacherTests
    {
        private const int NeuronInputCount = 2;

        // 1) Weight0
        // 2) Weight1
        // 3) Signal0
        // 4) Signal0
        // 4) expectedOutput - what neuron should return
        // 5) ratio - teching ratio
        [TestCase(5.0, 4.0, 5.0, 4.0, 1.0, 0.5)]
        [TestCase(0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
        [TestCase(-1.0, -1.0, -1.0, -1.0, -1.0, -1.0)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void SmartNeuronSimpleResponseLearningTest(double weight0, double weight1, double signal0, double signal1, double expectedOutput, double ratio)
        {
            this.TestNeuronLearning(weight0, weight1, signal0, signal1, expectedOutput, ratio);
        }

        // 1) PreviousWeight0
        // 2) PreviousWeight1
        // 3) Signal0
        // 4) Signal0
        // 4) sigma
        // 5) techingRatio - teching ratio
        // 6) momentumRatio - momentum ratio
        [TestCase(5.0, 4.0, 5.0, 4.0, 1.0, 0.5, 0.5)]
        [TestCase(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
        [TestCase(-1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void SmartNeuronSimpleResponseLearningTest2(double previousWeight0, double previousWeight1, double signal0, double signal1, double sigma, double techingRatio, double momentum)
        {
            var previousWeights = new[] { previousWeight0, previousWeight1 };
            // Prepare weights and signals
            double[] signals;
            SimpleNeuron originalNeuron;
            double originalResultBeforeLerning;
            double resultBeforeLerning;
            ISmartNeuron smartNeuron;
            this.PrepareNeruonsForTeaching(previousWeights[0], previousWeights[1], signal0, signal1, out signals, out originalNeuron, out originalResultBeforeLerning, out resultBeforeLerning, out smartNeuron);

            // Teaching
            smartNeuron.Learn(signals, previousWeights, sigma, techingRatio, momentum);
            originalNeuron.Learn(signals, previousWeights, sigma, techingRatio, momentum);

            // Second responses
            var originalResultAfterLerning = originalNeuron.Response(signals);
            var resultAfterLerning = smartNeuron.Response(signals);

            Assert.AreEqual(originalResultBeforeLerning, resultBeforeLerning);
            Assert.AreEqual(originalResultAfterLerning, resultAfterLerning);

            CollectionAssert.AreEqual(originalNeuron.Weights, smartNeuron.Weights, new WeightsComparer());
        }

        // 1) Weight0
        // 2) Weight1
        // 3) Signal0
        // 4) Signal0
        // 4) expectedOutput - what neuron should return
        // 5) ratio - teching ratio
        [TestCase(5.0, 4.0, 5.0, 4.0, 1.0, 0.5)]
        [TestCase(0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
        [TestCase(-1.0, -1.0, -1.0, -1.0, -1.0, -1.0)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void SmartNeuronTanhResponseWidrowHoffLearningTest(double weight0, double weight1, double signal0, double signal1, double expectedOutput, double ratio)
        {
            this.TestNeuronLearning(weight0, weight1, signal0, signal1, expectedOutput, ratio, true);
        }

        [Test]
        public void SmartNeuronLearningWithSignalsAsNullShouldThrowArgumentException()
        {
            double previouseResponse;
            double previouseError;
            var smartNeuron = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            Assert.Throws<ArgumentException>(
                () => smartNeuron.Learn(null, 0, 0, out previouseResponse, out previouseError));
        }

        [Test]
        public void SmartNeuronLearningWithDifferentSignalsCountShouldThrowArgumentException()
        {
            double previouseResponse;
            double previouseError;
            var smartNeuron = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            Assert.Throws<ArgumentException>(
                () => smartNeuron.Learn(new double[0], 0, 0, out previouseResponse, out previouseError));
        }

        [Test]
        public void SmartNeuronLearningWithSignalsAsNullShouldThrowArgumentException2()
        {
            var smartNeuron = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            Assert.Throws<ArgumentException>(() => smartNeuron.Learn(null, null, 0, 0, 0));
        }

        [Test]
        public void SmartNeuronLearningWithDifferentSignalsCountShouldThrowArgumentException2()
        {
            var smartNeuron = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            Assert.Throws<ArgumentException>(() => smartNeuron.Learn(new double[0], null, 0, 0, 0));
        }

        [Test]
        public void SmartNeuronLearningWithPreviousWeightsAsNullShouldThrowArgumentException()
        {
            var smartNeuron = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            Assert.Throws<ArgumentException>(() => smartNeuron.Learn(new double[NeuronInputCount], null, 0, 0, 0));
        }

        [Test]
        public void SmartNeuronLearningWithDifferentPreviousWeightsCountShouldThrowArgumentException()
        {
            var smartNeuron = new SmartNeuron(NeuronInputCount, new SimpleResponse());
            Assert.Throws<ArgumentException>(
                () => smartNeuron.Learn(new double[NeuronInputCount], new double[0], 0, 0, 0));
        }

        #region Methods (private)

        private void VerifyNeuronsLearningResults(double[] signals, SimpleNeuron originalNeuron, double originalResultBeforeLerning, double resultBeforeLerning, double originalpreviousError, double smartPreviousError, double originalpreviousResponse, double smartPreviousResponse, ISmartNeuron smartNeuron)
        {
            // Second responses
            var originalResultAfterLerning = originalNeuron.Response(signals);
            var resultAfterLerning = smartNeuron.Response(signals);

            Assert.AreEqual(originalpreviousError, smartPreviousError);
            Assert.AreEqual(originalpreviousResponse, smartPreviousResponse);

            Assert.AreEqual(originalResultBeforeLerning, resultBeforeLerning);
            Assert.AreEqual(originalResultAfterLerning, resultAfterLerning);

            CollectionAssert.AreEqual(originalNeuron.Weights, smartNeuron.Weights, new WeightsComparer());
        }

        private void TestNeuronLearning(double weight0, double weight1, double signal0, double signal1, double expectedOutput, double ratio, bool isWidrowHoffLearning = false)
        {
            // Prepare weights and signals
            double[] signals;
            SimpleNeuron originalNeuron;
            double originalResultBeforeLerning;
            double resultBeforeLerning;
            ISmartNeuron smartNeuron;
            this.PrepareNeruonsForTeaching(weight0, weight1, signal0, signal1, out signals, out originalNeuron, out originalResultBeforeLerning, out resultBeforeLerning, out smartNeuron);

            // Teaching
            double originalpreviousError, smartPreviousError;
            double originalpreviousResponse, smartPreviousResponse;
            if (isWidrowHoffLearning)
            {
                smartNeuron.LearnWidrowHoff(signals, expectedOutput, ratio, out smartPreviousResponse, out smartPreviousError);
                originalNeuron.LearnWidrowHoff(signals, expectedOutput, ratio, out originalpreviousResponse, out originalpreviousError);
            }
            else
            {
                smartNeuron.Learn(signals, expectedOutput, ratio, out smartPreviousResponse, out smartPreviousError);
                originalNeuron.Learn(signals, expectedOutput, ratio, out originalpreviousResponse, out originalpreviousError);
            }

            this.VerifyNeuronsLearningResults(
                signals,
                originalNeuron,
                originalResultBeforeLerning,
                resultBeforeLerning,
                originalpreviousError,
                smartPreviousError,
                originalpreviousResponse,
                smartPreviousResponse,
                smartNeuron);
        }

        private void PrepareNeruonsForTeaching(double weight0, double weight1, double signal0, double signal1, out double[] signals, out SimpleNeuron originalNeuron, out double originalResultBeforeLerning, out double resultBeforeLerning, out ISmartNeuron smartNeuron)
        {
            signals = new[] { signal0, signal1 };
            var weights = new[] { weight0, weight1 };

            var response = new TanhResponse();
            originalNeuron = new SimpleNeuron(NeuronInputCount, response, null);

            // Prepare neurons
            originalNeuron.SetWeights(weights);

            smartNeuron = new SmartNeuron(NeuronInputCount, response);
            smartNeuron.SetWeights(weights);

            // First responses
            originalResultBeforeLerning = originalNeuron.Response(signals);
            resultBeforeLerning = smartNeuron.Response(signals);
        }

        #endregion
    }
}
