using System;
using System.Linq;
using System.Runtime.Serialization;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    [Serializable]
    public abstract class BasicResponse : IResponse
    {
        #region Fields

        #region Error Messages

        private const string InputsAndWeightsCountAreNotEqual = "Inputs have to have the same count as the weights count.";

        #endregion

        private INeuron neuron;

        #endregion

        #region Constructors

        protected BasicResponse()
        {
        }

        protected BasicResponse(SerializationInfo info, StreamingContext context)
        {
        }

        #endregion

        #region Methods (public)

        #region Static

        public static double Response(double[] inputSignals, double[] weights)
        {
            return weights.Select((t, i) => t * inputSignals[i]).Sum();
        }

        #endregion

        public void SetNeuron(INeuron newNeuron)
        {
            this.neuron = newNeuron;
        }

        public double Response(double[] inputSignals)
        {
            // Validate the input signals and the check if number of input Equals the number of weights.
            if (inputSignals == null || inputSignals.Length != this.neuron.Weights.Length)
            {
                throw new ArgumentException(InputsAndWeightsCountAreNotEqual);
            }

            var result = this.neuron.Weights.Select((t, i) => t * inputSignals[i]).Sum();
            return this.ActivationFunction(result);
        }

        #endregion

        #region ISerializable Members

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        #endregion

        #region Methods (protected abstract)

        protected abstract double ActivationFunction(double argument);

        #endregion
    }
}