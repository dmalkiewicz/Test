using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork.Helpers.MemoryGuard
{
    public class DescendedFloatComparer : IComparer<float>
    {
        public int Compare(float x, float y)
        {
            var ascendingResult = Comparer<float>.Default.Compare(x, y);
            return 0 - ascendingResult;
        }
    }
}
