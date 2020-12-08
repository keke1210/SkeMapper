using SkeMapper.Extensions;
using SkeMapper.TypeContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SkeMapper.ResolveTypeLogic
{
    internal class CollectionTypeResolver : IResolver
    {
        private readonly IContainer container;

        public CollectionTypeResolver(IContainer container)
        {
            this.container = container;
        }

        public object ResolveTypeMap(object source)
        {
            var collection = source as IEnumerable<object>;
            var result = collection.Select(x => this.MapProperties(x)); 

            return result;
        }

        private object MapProperties(object source)
        {
            var sourceType = source.GetType();
            var destinationType = container.GetRegisteredMapping(sourceType);

            var sourceProperties = sourceType.GetProperties().ToDictionary(k => k.Name.ToLower(), v => v.GetValue(source, null));
            var destinationProperties = destinationType.GetProperties().Select(x => x.Name.ToLower());

            var destination = Activator.CreateInstance(destinationType);

            foreach (var propertyName in destinationProperties)
            {
                if (sourceProperties.TryGetValue(propertyName, out object propertyValue))
                {
                    var currentProperty = destination.GetType().GetProperty(
                        propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (!currentProperty.PropertyType.IsBuiltInType())
                    {
                        var child = this.MapProperties(propertyValue);

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
