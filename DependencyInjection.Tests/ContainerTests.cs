using DependencyInjection.Tests.Samples;

using TheKrystalShip.DependencyInjection;

using Xunit;

namespace DependencyInjection.Tests
{
    public class ContainerTests
    {
        [Fact]
        public void CanRetrieveType()
        {
            Container.Add<SampleClass>();

            SampleClass sample = Container.Get<SampleClass>();

            Assert.NotNull(sample);
            Assert.Equal(1, sample.DoSomething());

            Container.Clear();
        }

        [Fact]
        public void CanRetrieveDecoupledType()
        {
            Container.Add<ISampleInterface, SampleClassWithInterface>();

            ISampleInterface sample = Container.Get<ISampleInterface>();

            Assert.NotNull(sample);
            Assert.IsType<SampleClassWithInterface>(sample);
            Assert.Equal(1, sample.DoSomething());

            Container.Clear();
        }

        [Fact]
        public void CanRetrieveTypeWithDependency()
        {
            Container.Add<ISampleInterface, SampleClassWithInterface>();
            Container.Add<SampleClassWithDependency>();

            SampleClassWithDependency sample = Container.Get<SampleClassWithDependency>();

            Assert.NotNull(sample);

            ISampleInterface sampleInterface = sample.GetSampleInterface();

            Assert.NotNull(sampleInterface);
            Assert.Equal(1, sampleInterface.DoSomething());

            Container.Clear();
        }

        [Fact]
        public void CanAddSingleInstance()
        {
            SampleClass sampleClass = new SampleClass();

            Container.Add(sampleClass);

            SampleClass retrievedSampleClass = Container.Get<SampleClass>();

            Assert.NotNull(retrievedSampleClass);
            Assert.Equal(1, retrievedSampleClass.DoSomething());

            Container.Clear();
        }
    }
}
