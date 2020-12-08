using System;
using System.Collections.Concurrent;

namespace SkeMapper.TypeContainer
{
    public interface IContainer
    {
        ConcurrentDictionary<Type, Type> Mappings { get; }
        void CreateMap(Type typeSource, Type typeDestination);
        void CreateCollectionMap(Type typeSource, Type typeDestination);
        Type GetRegisteredMapping(Type key);
    }
}
