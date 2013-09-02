using System;
using System.Linq;
using NeuralNetworks.UnitTests.Neurons.Concretes;
using NeuralNetworks.UnitTests.Neurons.Interfaces;

namespace NeuralNetworks.UnitTests.Neurons.Abstracts
{
    /// <summary>This class represents a non linear neuron.</summary>
    public abstract class NonlinearNeuron : BasicNeuron, INonlinearNeuron
    {
        #region Fields

        private const int Min = -10;

        private const double Length = 0.00001; // or use min - max

        private bool hasBias;

        #endregion

        #region Constructors

        protected NonlinearNeuron(int inputCount)
            : base(inputCount)
        {
        }

        #endregion

        #region Methods (public)

        public abstract double ActivationFunction(double arg);

        public override double Response(double[] inputSignals)
        {
            return this.ActivationFunction(base.Response(inputSignals));
        }

        public void LearnWidrowHoff(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError)
        {
            previousResponse = base.Response(signals);
            previousError = expectedOutput - previousResponse;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += ratio * previousError * signals[i];
            }
        }

        public void AppendBias(bool hasBias)
        {
            if (this.hasBias == hasBias)
            {
                return;
            }

            if (hasBias)
            {
                var weights = this.Weights.ToList();
                weights.Add(Min + (Length * new Random().NextDouble()));
                this.Weights = weights.ToArray();
            }
            else
            {
                var weights = this.Weights.ToList();
                weights.RemoveAt(weights.Count - 1);
                this.Weights = weights.ToArray();
            }

            this.hasBias = hasBias;
        }

        #endregion
    }
}
