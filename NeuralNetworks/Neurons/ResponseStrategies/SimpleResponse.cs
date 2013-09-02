using System;
using System.Runtime.Serialization;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    [Serializable]
    public class SimpleResponse : BasicResponse
    {
        #region Constructors

        public SimpleResponse()
        {
        }

        protected SimpleResponse(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Methods (public)

        #region Static

        public static new double Response(double[] inputSignals, double[] weights)
        {
            return BasicResponse.Response(inputSignals, weights);
        }

        #endregion

        protected override double ActivationFunction(double argument)
        {
            return argument;
        }

        #endregion 
    }
}