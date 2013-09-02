namespace NeuralNetworks.Neurons.Enums
{
    /// <summary>Types of signal strength measurements.</summary>
    public enum StrengthNorm
    {
        /// <summary>Power is calculated as the square root of the sum of squares of the individual signal components.</summary>
        Euclidean,

        /// <summary>Power is calculated as the sum of the absolute values ​​of the individual signal components.</summary>
        Manhattan
    }
}
