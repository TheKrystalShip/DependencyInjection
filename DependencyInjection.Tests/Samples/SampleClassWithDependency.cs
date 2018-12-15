namespace DependencyInjection.Tests.Samples
{
    public class SampleClassWithDependency
    {
        private readonly ISampleInterface _sampleInterface;

        public SampleClassWithDependency(ISampleInterface sampleInterface)
        {
            _sampleInterface = sampleInterface;
        }

        public ISampleInterface GetSampleInterface()
        {
            return _sampleInterface;
        }
    }
}
