using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SkeMapper
{
    public class SkeMapper
    {
        public ConcurrentDictionary<Type, Type> Pairs { get; } = new ConcurrentDictionary<Type, Type>();

        public void CreateMap<TIn, TOut>() 
            where TIn : class 
            where TOut : class 
        {
            Pairs.TryAdd(typeof(TIn), typeof(TOut));
        }

        public TDestination Map<TDestination>(object source) where TDestination : class
        {
            var sourceType = source.GetType();

            TDestination result = default;

            // If type exists (is registered)
            if (Pairs.TryGetValue(sourceType, out Type type))
                result = ResolveTypeMap(source) as TDestination;

            if(result == default || result == null)
                throw new Exception("There is no Mapper configured for this object.");

            return result;
        }
         
        public object ResolveTypeMap(object source)
        {
            var sourceType = source.GetType();
            Pairs.TryGetValue(sourceType, out Type destinationType);

            var sourceProperties = sourceType.GetProperties().ToDictionary(x => x.Name, y => y.GetValue(source, null));
            var destinationProperties = destinationType.GetProperties().Select(x => x.Name);

            var destination = Activator.CreateInstance(destinationType);

            foreach (var propertyName in destinationProperties)
            {
                if (sourceProperties.TryGetValue(propertyName, out object propertyValue))
                {
                    var currentProperty = destination.GetType().GetProperty(propertyName);
                    var currentPropertyType = currentProperty.PropertyType;

                    if (currentPropertyType.IsClass && string.IsNullOrEmpty(currentPropertyType.Namespace) ||
                       (!currentPropertyType.Namespace.Equals("System") && !currentPropertyType.Namespace.StartsWith("System.")))
                    { 
                        var child = this.ResolveTypeMap(propertyValue);

                        currentProperty.SetValue(destination, child, null);
                    }
                    else
                    {
                        currentProperty.SetValue(destination, propertyValue, null);
                    }
                }
            }

            return destination;
        }
    }
}
