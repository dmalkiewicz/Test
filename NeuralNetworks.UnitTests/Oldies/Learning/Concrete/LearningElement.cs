using NeuralNetworks.UnitTests.Learning.Interfaces;

using ILearningElement = NeuralNetworks.Layers.ILearningElement;

namespace NeuralNetworks.UnitTests.Learning.Concrete
{
    public class LearningElement : ILearningElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LearningElement"/> class
        /// class which is a exact copy of specified <see cref="LearningElement"/>" as a parameter.
        /// </summary>
        /// <param name="element">The element used as the original.</param>
        public LearningElement(ILearningElement element)
        {
            this.Inputs = (double[])element.Inputs.Clone();
            this.ExpectedOutputs = (double[])element.ExpectedOutputs.Clone();
            this.Comment = element.Comment;
        }

        /// <summary>Gets or sets the values ​​that should be given to the neural network input. May require normalization.</summary>
        public double[] Inputs { get; set; }

        /// <summary>Gets or sets values which are expected on the output of the neural network.</summary>
        public double[] ExpectedOutputs { get; set; }

        /// <summary>Gets or sets comment of the learning element.</summary>
        public string Comment { get; set; }

    }
}
