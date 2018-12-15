using DependencyInjection.Tests.Samples;

using System;

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

            var sample = Container.Get<SampleClass>();

            Assert.NotNull(sample);
            Assert.Equal(1, sample.DoSomething());
        }

        [Fact]
        public void CanRetrieveDecoupledType()
        {
            try
            {
                Container.Add<ISampleInterface, SampleClassWithInterface>();
            }
            catch (InvalidOperationException)
            {
                // Exception because other method might have registered
                // the service before this test was run
            }

            var sample = Container.Get<ISampleInterface>();

            Assert.NotNull(sample);
            Assert.IsType<SampleClassWithInterface>(sample);
            Assert.Equal(1, sample.DoSomething());
        }

        [Fact]
        public void CanRetrieveTypeWithDependency()
        {
            try
            {
                Container.Add<ISampleInterface, SampleClassWithInterface>();
            }
            catch (InvalidOperationException)
            {
                // Exception because other method might have registered
                // the service before this test was run
            }

            Container.Add<SampleClassWithDependency>();

            var sample = Container.Get<SampleClassWithDependency>();

            Assert.NotNull(sample);

            var sampleInterface = sample.GetSampleInterface();

            Assert.NotNull(sampleInterface);
            Assert.Equal(1, sampleInterface.DoSomething());
        }
    }
}
