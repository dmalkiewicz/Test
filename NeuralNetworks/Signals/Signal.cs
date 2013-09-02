using System;
using System.Runtime.Serialization;
using NeuralNetwork.Helpers;

namespace NeuralNetworks.Signals
{
    [DataContract]
    [Serializable]
    public class Signal : ISignal
    {
        #region Constructors

        public Signal()
        {
            this.Identifier = SequentialGuid.NewSequentialGuid();
        }

        protected Signal(SerializationInfo info, StreamingContext context)
        {
            this.Identifier = (Guid)info.GetValue(PropertyHelper.GetName<Signal>(p => p.Identifier), typeof(Guid));
            this.Value = (double)info.GetValue(PropertyHelper.GetName<Signal>(p => p.Value), typeof(double));
        }

        #endregion

        #region Properties

        [DataMember]
        public Guid Identifier { get; set; }

        [DataMember]
        public double Value { get; set; }

        #endregion

        #region Methods (public)

        public override bool Equals(object obj)
        {
            var signal = obj as ISignal;
            if (signal == null)
            {
                return false;
            }

            if (this.Identifier != signal.Identifier)
            {
                return false;
            }

            return !(Math.Abs(this.Value - signal.Value) > double.Epsilon);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region ISerializable Members

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(PropertyHelper.GetName<Signal>(p => p.Identifier), this.Identifier);
            info.AddValue(PropertyHelper.GetName<Signal>(p => p.Value), this.Value);
        }

        #endregion
    }
}