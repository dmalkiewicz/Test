namespace NeuralNetworks.UnitTests.Learning.Interfaces
{
    public interface ILearningElement
    {
        /// <summary>Gets or sets the values ​​that should be given to the neural network input. May require normalization.</summary>
        double[] Inputs { get; set; }

        /// <summary>Gets or sets values which are expected on the output of the neural network.</summary>
        double[] ExpectedOutputs { get; set; }

        /// <summary>Gets or sets comment of the learning element.</summary>
        string Comment { get; set; }
    }
}
