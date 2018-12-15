using System;
using System.Collections.Generic;

namespace TheKrystalShip.DependencyInjection
{
    internal class ServiceResolver : IServiceResolver
    {
        public IDependencyInjector DependencyInjector { get; set; }

        private readonly Dictionary<Type, object> _store;
        private readonly Dictionary<Type, Type> _bindings;

        public ServiceResolver()
        {
            DependencyInjector = new DependencyInjector(this);
            _store = new Dictionary<Type, object>();
            _bindings = new Dictionary<Type, Type>();
        }

        public ServiceResolver(IDependencyInjector injector)
        {
            DependencyInjector = injector;
            _store = new Dictionary<Type, object>();
            _bindings = new Dictionary<Type, Type>();
        }

        public T Resolve<T>()
        {
            return (T)GetService(typeof(T));
        }
        
        public object GetService(Type serviceType)
        {
            if (_store.ContainsKey(serviceType))
            {
                return _store[serviceType];
            }

            if (_bindings.ContainsKey(serviceType))
            {
                Type type = _bindings[serviceType];

                object implementation = DependencyInjector.GetInjectedInstance(type);

                _store.Add(serviceType, implementation);

                return implementation;
            }

            throw new InvalidOperationException($"No service of type {serviceType} has been registered to the container");
        }

        public void Register<TType>()
        {
            Type type = typeof(TType);

            if (_bindings.ContainsKey(type))
            {
                throw new InvalidOperationException($"A service of type {type} has already been registered");
            }

            _bindings.Add(type, type);
        }

        public void Register<TType, TImplementation>()
        {
            Type keyType = typeof(TType);
            Type valueType = typeof(TImplementation);

            if (_bindings.ContainsKey(keyType))
            {
                throw new InvalidOperationException($"A service of type {keyType} has already been registered");
            }

            _bindings.Add(keyType, valueType);
        }
        
        public void Register<TType>(object implementation)
        {
            Type type = typeof(TType);

            if (_store.ContainsKey(type))
            {
                throw new InvalidOperationException($"A service of type {implementation} has already been registered");
            }

            _store.Add(type, implementation);
        }
    }
}
