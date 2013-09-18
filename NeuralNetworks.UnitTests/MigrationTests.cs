using NeuralNetworks.Neurons;
using NeuralNetworks.Neurons.ResponseStrategies;
using NUnit.Framework;
using StrengthNorm = NeuralNetworks.Neurons.Enums.StrengthNorm;

namespace NeuralNetworks.UnitTests
{
    [TestFixture]
    public class MigrationTests
    {
        #region Neuron tests

        #region Response tests

        [TestCase(0, 0, 0, 0, 0.0)]
        [TestCase(5, 4, 5, 4, 41.0)]
        [TestCase(-1, -1, -1, -1, 2.0)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, 0.0)]
        [TestCase(double.Epsilon, double.MaxValue, double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.Epsilon, double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.MaxValue, double.Epsilon, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.Epsilon, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void SimpleResponseTest(double weight0, double weight1, double signal0, double signal1, double expectedResult)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var originalResult = expectedResult;
            var newNeuron = new SimpleNeuron(2, new SimpleResponse(), null);
            newNeuron.SetWeights(new[]
                                {
                                    weight0, weight1
                                });

            // TODO: newNeuron.ResponseStrategy.SetNeuron(newNeuron);
            var result = newNeuron.Response(signals);

            Assert.AreEqual(originalResult, result);
        }

        [TestCase(0, 0, 0, 0, 0.5)]
        [TestCase(5, 4, 5, 4, 1.0)]
        [TestCase(-1, -1, -1, -1, 0.88079707797788231)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, 0.5)]
        [TestCase(double.Epsilon, double.MaxValue, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.Epsilon, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.Epsilon, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.Epsilon, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, 1.0)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, 1.0)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.0)]
        public void SigmoidalResponseTest(double weight0, double weight1, double signal0, double signal1, double expectedResult)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var originalResult = expectedResult;
            var newNeuron = new SimpleNeuron(2, new SigmoidalResponse(), null);
            newNeuron.SetWeights(new[]
                                {
                                    weight0, weight1
                                });

            // TODO: newNeuron.ResponseStrategy.SetNeuron(newNeuron);
            var result = newNeuron.Response(signals);

            Assert.AreEqual(originalResult, result);
        }

        [TestCase(0, 0, 0, 0, 0.0)]
        [TestCase(5, 4, 5, 4, 1.0)]
        [TestCase(-1, -1, -1, -1, 0.9640275800758169)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, 0.0)]
        [TestCase(double.Epsilon, double.MaxValue, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.Epsilon, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.Epsilon, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.Epsilon, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, 1.0)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, 1.0)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.0)]
        public void TanhResponseTest(double weight0, double weight1, double signal0, double signal1, double expectedResult)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var newNeuron = new SimpleNeuron(2, new TanhResponse(), null);
            newNeuron.SetWeights(new[]
                                {
                                    weight0, weight1
                                });

            // TODO: newNeuron.ResponseStrategy.SetNeuron(newNeuron);
            var result = newNeuron.Response(signals);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0, 0, 0, 0.0)]
        [TestCase(5, 4, 5, 4, 1.0)]
        [TestCase(-1, -1, -1, -1, 1.0)]
        [TestCase(double.NaN, double.NaN, double.NaN, double.NaN, 0.0)]
        [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon, 0.0)]
        [TestCase(double.Epsilon, double.MaxValue, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.Epsilon, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.Epsilon, double.MaxValue, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.Epsilon, 1.0)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, 1.0)]
        [TestCase(double.MinValue, double.MinValue, double.MinValue, double.MinValue, 1.0)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, 1.0)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.0)]
        public void UnipolarResponseTest(double weight0, double weight1, double signal0, double signal1, double expectedResult)
        {
            var signals = new[]
            {
                signal0,
                signal1
            };

            var newNeuron = new SimpleNeuron(2, new UnipolarResponse(), null);
            newNeuron.SetWeights(new[]
                                {
                                    weight0, weight1
                                });

            // TODO: // TODO: newNeuron.ResponseStrategy.SetNeuron(newNeuron);
            var result = newNeuron.Response(signals);

            Assert.AreEqual(expectedResult, result);
        }

        #endregion

        #region Euclidean MemoryTraceStrength tests

        [TestCase(0, 0, 0.0)]
        [TestCase(5, 4, 6.4031242374328485)]
        [TestCase(-1, -1, 1.4142135623730951)]
        [TestCase(double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, 0.0)]
        [TestCase(double.Epsilon, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.Epsilon, double.MinValue, double.PositiveInfinity)]
        [TestCase(double.MinValue, double.Epsilon, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.Epsilon, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.MinValue, double.MinValue, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void EuclideanMemoryTraceStrengthTest(double weight0, double weight1, double expectedResult)
        {
            var newNeuron = new SimpleNeuron(2, new SimpleResponse(), null);
            newNeuron.SetWeights(new[]
                                {
                                    weight0, weight1
                                });

            // TODO: newNeuron.ResponseStrategy.SetNeuron(newNeuron);
            var result = newNeuron.MemoryTraceStrength(StrengthNorm.Euclidean);

            Assert.AreEqual(expectedResult, result);
        }

        #endregion

        #region Manhattan MemoryTraceStrength tests

        [TestCase(0, 0, 0.0)]
        [TestCase(5, 4, 9.0)]
        [TestCase(-1, -1, 2.0)]
        [TestCase(double.NaN, double.NaN, double.NaN)]
        [TestCase(double.Epsilon, double.Epsilon, 9.88131291682493E-324)]
        [TestCase(double.Epsilon, double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, double.Epsilon, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue, double.PositiveInfinity)]
        [TestCase(double.MinValue, double.MinValue, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void ManhattanMemoryTraceStrengthTest(double weight0, double weight1, double expectedResult)
        {
            var newNeuron = new SimpleNeuron(2, new SimpleResponse(), null);
            newNeuron.SetWeights(new[]
                                {
                                    weight0, weight1
                                });

            // TODO: newNeuron.ResponseStrategy.SetNeuron(newNeuron);
            var result = newNeuron.MemoryTraceStrength(StrengthNorm.Manhattan);

            Assert.AreEqual(expectedResult, result);
        }

        #endregion

        #endregion
    }
}
