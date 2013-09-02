namespace NeuralNetworks.UnitTests.Learning.Interfaces
{
    public interface ILearningSet
    {
        int InputCount { get; set; }

        int OutputCount { get; set; }

        void Normalize();
    }
}