using System;
using System.Collections;
using System.Collections.Generic;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    internal class WeightsComparer : IComparer, IComparer<double>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as double?;
            var rhs = y as double?;
            if (!lhs.HasValue || !rhs.HasValue)
            {
                throw new InvalidOperationException();
            }

            return this.Compare(lhs.Value, rhs.Value);
        }

        public int Compare(double x, double y)
        {
            int temp;
            return (temp = x.CompareTo(y)) != 0 ? temp : x.CompareTo(y);
        }
    }
}
