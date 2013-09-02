using System;

namespace NeuralNetwork.Helpers.DynamicMemoryGuard
{
    public class MemoryLevelEventArgs : EventArgs
    {
        public float MemoryLevel { get; set; }
    }
}
