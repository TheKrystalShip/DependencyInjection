using System;

namespace TheKrystalShip.DependencyInjection
{
    public interface IServiceResolver : IServiceProvider
    {
        IDependencyInjector DependencyInjector { get; set; }

        void Register<TFrom, TTo>();
        void Register<TType>();
        void Register<TType>(object implementation);
        void Register<TType>(TType instance);
        T Resolve<T>();
        void Clear();
    }
}
