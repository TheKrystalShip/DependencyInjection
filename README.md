# TheKrystalShip.DependencyInjection

This is a dependency injection IoC container library.

It exposes one static `Container` class which is the main way to register and retrieve types into and from the IoC container.

Register all types at the program's entrypoint:

> All services register as singletons

```cs
public class Program
{
    public static void Main(string[] args)
    {
        // ...
        // Decoupled registration
        Container.Add<IMyInterface, MyImplementation>();

        // Single type registration
        Container.Add<MyImplementation>();

        // Instantiated registration
        Container.Add<MyInterface>(myImplementationInstance);
    }
}
```

Retrieve your services later on in the call stack:

```cs
public class MySampleClass
{
    private readonly IMyInterface _myInterface;

    public MySampleClass()
    {
        _myInterface = Container.Get<IMyInterface>(); // => MyImplementation instance registered before;
    }
}
```

If a service has depenencies, the will be resolved automatically:

```cs
public class MyDependency() { ... }

public class MyService
{
    private readonly MyDependency _myDependency;

    public MyService(MyDependency myDependency)
    {
        _myDependency = myDependency;
    }

    public int DoSomething()
    {
        return _myDependency.SomeMethod();
    }
}
```

Calling the service from the `Container` will trigger the dependency injection:

```cs
public class Program
{
    public static void Main(string[] args)
    {
        // Make sure both types are registered into the container
        Container.Add<MyDependency>();
        Container.Add<MyService>();

        // ...

        MyService myService = Container.Get<MyService>();

        // myService will have an instance of `MyDependency` injected into it
        int result = myService.DoSomething();
    }
}
```

> The library also offers two interfaces (`IServiceResolver` and `IDependencyInjector`) if you wish to make your own implementation.

# License

```plaintext
MIT License

Copyright (c) 2018 The Krystal Ship

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
