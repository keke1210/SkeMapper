using SkeMapper.Extensions;
using System;
using System.Collections.Concurrent;

namespace SkeMapper.TypeContainer
{
    internal sealed class Container : IContainer
    {
        static Container() { }
        private Container() { }

        private static Lazy<Container> lazyContainer = new Lazy<Container>(() => new Container(), true);

        public static Container Instance => lazyContainer.Value;

        public ConcurrentDictionary<Type, Type> Pairs { get; } = new ConcurrentDictionary<Type, Type>();

        public void CreateMap(Type typeSource, Type typeDestination)
        {
            if (typeSource.IsBuiltInType() || typeDestination.IsBuiltInType())
                throw new Exception("C# built-in types or value types can't be mapped!");

            Pairs.TryAdd(typeSource, typeDestination);
        }
    }
}
