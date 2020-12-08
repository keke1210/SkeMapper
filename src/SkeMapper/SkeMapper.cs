using SkeMapper.ResolveTypeLogic;
using SkeMapper.TypeContainer;
using System;
using System.Collections;

namespace SkeMapper
{
    public sealed class SkeMapper : IMapper
    {
        public Lazy<IContainer> MapperContainer { get; } = new Lazy<IContainer>(() => Container.Instance);
        private SkeMapper() { }
        public static SkeMapper Instance => new SkeMapper();


        public TDestination Map<TDestination>(object source)
        {
            if (source is TDestination sourceRes)
            {
                return sourceRes;
            }

            var resolveType = source is IEnumerable ? ResolveType.Collection : ResolveType.Single;

            var factory = new ResolverFactory(MapperContainer.Value);
            IResolver resolver = factory.Create(resolveType);

            var destination = resolver.ResolveTypeMap(source);

            return (TDestination)destination;
        }
    }
}
