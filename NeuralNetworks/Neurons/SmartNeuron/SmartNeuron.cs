using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Neurons.SmartNeuron
{
    public partial class SmartNeuron : ISmartNeuron
    {
        #region Fields

        #region Error Messages

        private const string DifferentCounts = "Weight array lenght and Input count must have exactly the same elements.";

        #endregion

        private Dictionary<IReadonlySignal, double> inputSignalsWithWeights;

        private double[] weights;

        private Guid neuronGuid;

        #endregion
        
        #region Constructors

        /// <summary>Initialises a new instance of the <see cref="SmartNeuron"/> class.
        /// This class is a smart neuron with specified number of inputs,
        /// which can be used in network which will delete or add neurons.</summary>
        /// <param name="inputCount">The number of inputs for neuron.</param>
        /// <param name="responseStrategy">The neuron response strategy.</param>
        /// <remarks>Note: The array of weights will have exactly the same size as the input count.</remarks>
        public SmartNeuron(uint inputCount, IResponse responseStrategy = null)
        {
            this.InputSignals = new List<IReadonlySignal>();
            for (var i = 0; i < inputCount; i++)
            {
                this.InputSignals.Add(new Signal { Identifier = Guid.NewGuid() });
            }

            this.InitializeNeuron(ref responseStrategy);
            this.InputSignalsWithWeights = CreateInputSignalsWithWeights(this.InputSignals, CreateRandomWeights(inputCount));
            this.PreviousWeights = new double[this.Weights.Length];
        }

        public SmartNeuron(IEnumerable<IReadonlySignal> inputSignals, IResponse responseStrategy = null)
        {
            var readonlySignals = inputSignals as IList<IReadonlySignal> ?? inputSignals.ToList();
            ValidateNeededObjects(readonlySignals);

            this.InitializeNeuron(ref responseStrategy);
            this.InputSignals = readonlySignals.ToList();
            this.InputSignalsWithWeights = CreateInputSignalsWithWeights(
                this.InputSignals,
                CreateRandomWeights(Convert.ToUInt32(this.InputSignals.Count)));
            this.PreviousWeights = new double[this.Weights.Length];
        }

        public SmartNeuron(Guid neuronGuid, double[] weights, IEnumerable<IReadonlySignal> inputSignals, IResponse responseStrategy = null)
        {
            var readonlySignals = inputSignals as IList<IReadonlySignal> ?? inputSignals.ToList();
            ValidateNeededObjects(readonlySignals);
            if (weights == null || weights.Length != readonlySignals.Count())
            {
                throw new ArgumentNullException(DifferentCounts);
            }

            this.InputSignals = readonlySignals.ToList();
            this.InitializeNeuron(ref responseStrategy, neuronGuid);
            this.InputSignalsWithWeights = CreateInputSignalsWithWeights(this.InputSignals, weights);
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets a neuron Guid.</summary>
        [DataMember]
        public Guid Identifier
        {
            get
            {
                return this.neuronGuid;
            }

            set
            {
                this.neuronGuid = value;
            }
        }

        /// <summary>Gets an array which stores weights for each of neuron's inputs.</summary>
        [DataMember]
        public double[] Weights
        {
            get
            {
                return this.weights;
            }
        }

        /// <summary>Gets an dictionary which stores input signals with assigned to them weights.</summary>
        [DataMember]
        public Dictionary<IReadonlySignal, double> InputSignalsWithWeights
        {
            get
            {
                return this.inputSignalsWithWeights;
            }

            private set
            {
                this.inputSignalsWithWeights = value;
                if (this.inputSignalsWithWeights != null)
                {
                    this.weights = this.InputSignalsWithWeights.Select(i => i.Value).ToArray();
                }
            }
        }

        /// <summary>Gets an array which stores previous weights for each of neuron's inputs.</summary>
        [DataMember]
        public double[] PreviousWeights { get; private set; }

        /// <summary>Gets or sets the neuron response strategy.</summary>
        [DataMember]
        public IResponse ResponseStrategy { get; set; }

        /// <summary>Gets or sets the neuron input signals.</summary>
        [DataMember]
        public IList<IReadonlySignal> InputSignals { get; set; }

        /// <summary>Gets or sets the neuron description.</summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>Gets the neuron output signal.</summary>
        public double Value { get; private set; }

        #endregion

        private void InitializeNeuron(ref IResponse responseStrategy, Guid neuronIdentifier = default(Guid))
        {
            if (responseStrategy == null)
            {
                responseStrategy = new SimpleResponse();
            }

            this.ResponseStrategy = ResponseStrategyFactory.CreateResponseCopy(responseStrategy, this);
            this.Identifier = neuronIdentifier != default(Guid) ? neuronIdentifier : SequentialGuid.NewSequentialGuid();
        }
    }
}