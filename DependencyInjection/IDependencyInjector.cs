using System;

namespace TheKrystalShip.DependencyInjection
{
    public interface IDependencyInjector
    {
        IServiceResolver ServiceResolver { get; set; }

        object GetInjectedInstance(Type type);
        T GetInjectedInstance<T>() where T : class;
    }
}