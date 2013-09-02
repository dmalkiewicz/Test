using System;

using NeuralNetworks.UnitTests.Neurons.Abstracts;

namespace NeuralNetworks.UnitTests.Neurons.Concretes
{
    public class SigmoidalNeuron : NonlinearNeuron
    {
        #region Fields

        private double beta = 1.0; 

        #endregion

        #region Constructors

        public SigmoidalNeuron(int inputCount)
            : base(inputCount)
        {
        }

        #endregion

        #region Properties

        public double Beta
        {
            get
            {
                return this.beta;
            }

            set
            {
                this.beta = value;
            }
        }

        #endregion

        #region Methods (public)

        public override double ActivationFunction(double arg)
        {
            return 1.0 / (1.0 + Math.Exp(-this.beta * arg));
        } 
        #endregion
    }
}
