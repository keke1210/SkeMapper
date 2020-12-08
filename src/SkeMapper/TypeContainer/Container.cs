using SkeMapper.Exceptions;
using SkeMapper.Extensions;
using System;
using System.Collections.Concurrent;

namespace SkeMapper.TypeContainer
{
    public sealed class Container : IContainer
    {
        private Container() { }
        public static IContainer Instance => new Container();

        public ConcurrentDictionary<Type, Type> Mappings { get; } = new ConcurrentDictionary<Type, Type>();

        public void CreateMap(Type typeSource, Type typeDestination)
        {
            if (typeSource.IsBuiltInType() || typeDestination.IsBuiltInType())
                throw new RegisterBuiltInTypesException("C# built-in types or value types can't be mapped!");

            var typeWasAddedToContainer = Mappings.TryAdd(typeSource, typeDestination);
            if(!typeWasAddedToContainer) 
                throw new DuplicateRegisteredTypeException("Duplicate types could not be registered as source!");
        }

        public void CreateCollectionMap(Type typeSource, Type typeDestination)
        {
            var typeWasAddedToContainer = Mappings.TryAdd(typeSource, typeDestination);
            if (!typeWasAddedToContainer)
                throw new DuplicateRegisteredTypeException("Duplicate types could not be registered as source!");
        }

        public Type GetRegisteredMapping(Type key)
        {
            if(Mappings.TryGetValue(key, out Type result) == false)
                throw new MappingNotExistsException("This mapping is not registered in settings.");

            return result;
        }
    }
}
