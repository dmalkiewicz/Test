using System;
using System.Runtime.Serialization;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    [Serializable]
    public class UnipolarResponse : BasicResponse
    {
        #region Constructors
        
        public UnipolarResponse()
        {
        }

        protected UnipolarResponse(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Methods (public)

        #region Static

        public static new double Response(double[] inputSignals, double[] weights)
        {
            var baseResponse = BasicResponse.Response(inputSignals, weights);
            return baseResponse > 0 ? 1 : 0;
        }

        #endregion

        protected override double ActivationFunction(double argument)
        {
            return argument > 0 ? 1 : 0;
        }

        #endregion 
    }
}