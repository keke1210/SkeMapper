using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SkeMapper
{
    public class SkeMapper
    {
        public ConcurrentDictionary<Type, Type> Pairs { get; } = new ConcurrentDictionary<Type, Type>();

        public void CreateMap<TIn, TOut>()
        {
            Pairs.TryAdd(typeof(TIn), typeof(TOut));
        }

        public TDestination Map<TDestination>(object source)
        {
            var sourceType = source.GetType();

            // If type exists (is registered)
            if (Pairs.TryGetValue(sourceType, out Type type))
            {
                // return destination instance
                return ResolveTypeMap<TDestination>(source);
            }

            return default;
        }

        public TDestination ResolveTypeMap<TDestination>(object source)
        {
            var sourceType = source.GetType();
            Pairs.TryGetValue(sourceType, out Type destinationType);

            var sourceProperties = sourceType.GetProperties().ToDictionary(x => x.Name, y => y.GetValue(source, null));
            var destinationProperties = destinationType.GetProperties().Select(x => x.Name);

            var destination = (TDestination) Activator.CreateInstance(destinationType);

            foreach (var propertyName in destinationProperties)
            {
                if (sourceProperties.TryGetValue(propertyName, out object propertyValue))
                {
                    destination.GetType().GetProperty(propertyName).SetValue(destination, propertyValue);
                }
            }

            return destination;
        }
    }
}
