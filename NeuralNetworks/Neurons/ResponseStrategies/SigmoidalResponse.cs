using System;
using System.Runtime.Serialization;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    [Serializable]
    public class SigmoidalResponse : BasicResponse
    {
        #region Fields

        private double beta = 1.0;

        #endregion

        #region Constructors
        
        public SigmoidalResponse()
        {
        }

        protected SigmoidalResponse(SerializationInfo info, StreamingContext context)
            : base(info, context)
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

        #region Static

        public static new double Response(double[] inputSignals, double[] weights)
        {
            return BasicResponse.Response(inputSignals, weights);
        }

        public static double Response(double[] inputSignals, double[] weights, double beta)
        {
            var baseResponse = BasicResponse.Response(inputSignals, weights);
            return 1.0 / (1.0 + Math.Exp(-beta * baseResponse));
        }

        #endregion

        protected override double ActivationFunction(double argument)
        {
            return 1.0 / (1.0 + Math.Exp(-this.Beta * argument));
        }

        #endregion 
    }
}