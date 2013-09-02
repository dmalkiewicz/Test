using System;
using System.Runtime.Serialization;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    [Serializable]
    public class TanhResponse : BasicResponse
    {
        #region Constructors
        
        public TanhResponse()
        {
        }

        protected TanhResponse(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Methods (public)

        #region Static

        public static new double Response(double[] inputSignals, double[] weights)
        {
            var baseResponse = BasicResponse.Response(inputSignals, weights);
            return Math.Tanh(baseResponse);
        }

        #endregion

        protected override double ActivationFunction(double argument)
        {
            return Math.Tanh(argument);
        }

        #endregion 
    }
}