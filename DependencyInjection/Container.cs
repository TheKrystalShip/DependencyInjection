using System;

namespace TheKrystalShip.DependencyInjection
{
    /// <summary>
    /// IoC Container with Dependency Injection built in
    /// </summary>
    public static class Container
    {
        private static IServiceResolver _serviceResolver;

        static Container()
        {
            _serviceResolver = new ServiceResolver();
        }

        /// <summary>
        /// Add a Type to the IoC Container
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void Add<T>() where T : class
        {
            _serviceResolver.Register<T>();
        }

        /// <summary>
        /// Add an instantiated Type to the IoC Container
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type"></param>
        public static void Add<T>(T type) where T : class
        {
            _serviceResolver.Register(type);
        }

        /// <summary>
        /// Add a decoupled implementation of a Type to the IoC Container
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="I">Implementation</typeparam>
        public static void Add<T, I>() where T : class where I : class, T
        {
            _serviceResolver.Register<T, I>();
        }

        /// <summary>
        /// Get a stored instance of a given Type from the IoC Container
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        private static object Get(Type type)
        {
            return _serviceResolver.GetService(type);
        }

        /// <summary>
        /// Access to the Microsoft.DependencyInjection.IServiceProvider
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider()
        {
            return _serviceResolver;
        }

        /// <summary>
        /// Access to the configured IServiceResolver
        /// </summary>
        /// <returns></returns>
        public static IServiceResolver GetServiceResolver()
        {
            return _serviceResolver;
        }

        /// <summary>
        /// Used to set the internal IServiceResolver if you want to use
        /// a custom implementation of it.
        /// </summary>
        /// <param name="serviceResolver">Implementation of IServiceResolver</param>
        public static void SetServiceResolver(IServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        /// <summary>
        /// Remove all registered Types and/or implementations
        /// </summary>
        public static void Clear()
        {
            _serviceResolver.Clear();
        }
    }
}
