using System;
using System.Collections.Concurrent;

namespace SkeMapper.TypeContainer
{
    public interface IContainer
    {
        ConcurrentDictionary<Type, Type> Pairs { get; }
        void CreateMap(Type typeSource, Type typeDestination);
    }
}
