using SkeMapper.Extensions;
using SkeMapper.TypeContainer;
using System;
using System.Linq;
using System.Reflection;

namespace SkeMapper.ResolveTypeLogic
{
    internal class SingleTypeResolver : IResolver
    {
        private readonly IContainer container;

        public SingleTypeResolver(IContainer container)
        {
            this.container = container;
        }

        public object ResolveTypeMap(object source)
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
