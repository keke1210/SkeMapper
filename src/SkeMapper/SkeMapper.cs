using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace SkeMapper
{
    public class SkeMapper
    {
        public ConcurrentDictionary<Type, Type> Pairs { get; } = new ConcurrentDictionary<Type, Type>();

        public void CreateMap<TSource, TDestination>() 
            where TSource : class 
            where TDestination : class 
        {
            var typeIn = typeof(TSource);
            var typeOut = typeof(TDestination);

            if (IsBuiltInType(typeIn) || IsBuiltInType(typeOut))
                throw new Exception("C# built-in types or value types can't be mapped!");

            Pairs.TryAdd(typeIn, typeOut);
        }

        public TDestination Map<TDestination>(object source) 
            where TDestination : class
        {
            var sourceType = source.GetType();

            TDestination result = default;

            // If type exists (is registered)
            if (Pairs.ContainsKey(sourceType))
                result = ResolveTypeMap(source) as TDestination;

            if (result == default || result == null)
                throw new Exception("There is no Mapper configured for this object.");

            return result;
        }
         
        public object ResolveTypeMap(object source)
        {
            var sourceType = source.GetType();
            Pairs.TryGetValue(sourceType, out Type destinationType);

            var sourceProperties = sourceType.GetProperties().ToDictionary(k => k.Name.ToLower(), v => v.GetValue(source, null));
            var destinationProperties = destinationType.GetProperties().Select(x => x.Name.ToLower());

            var destination = Activator.CreateInstance(destinationType);

            foreach (var propertyName in destinationProperties)
            {
                if (sourceProperties.TryGetValue(propertyName, out object propertyValue))
                {
                    var currentProperty = destination.GetType().GetProperty(
                        propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (!IsBuiltInType(currentProperty.PropertyType))
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

        // TODO: 
        [Obsolete]
        public object ResolveCollectionTypeMap(object source)
        {
            if (!(source is IEnumerable))
                throw new ArgumentException("To do");

            var sourceType = source.GetType();

            Pairs.TryGetValue(sourceType, out Type destinationType);

            var sourceProperties = sourceType.GetProperties().ToDictionary(x => x.Name, y => y.GetValue(source, null));
            var destinationProperties = destinationType.GetProperties().Select(x => x.Name);

            var destination = Activator.CreateInstance(destinationType);

            foreach (var propertyName in destinationProperties)
            {
                if (sourceProperties.TryGetValue(propertyName, out object propertyValue))
                {
                    var currentProperty = destination.GetType().GetProperty(
                        propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (!IsBuiltInType(currentProperty.PropertyType))
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

        /// <summary>
        /// Is C# built-in type or struct
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsBuiltInType(Type type) 
        {
            return !(type.IsClass && string.IsNullOrEmpty(type.Namespace) ||
                   (!type.Namespace.Equals("System") && !type.Namespace.StartsWith("System.")));
        }
    }
}
